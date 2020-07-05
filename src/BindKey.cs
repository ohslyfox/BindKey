using Gma.System.MouseKeyHook;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using BindKey.KeyActions;

namespace BindKey
{
    public partial class BindKey : Form
    {
        private IKeyboardMouseEvents m_GlobalHook = Hook.GlobalEvents();
        private Dictionary<string, List<IKeyAction>> ProfileKeyActions = new Dictionary<string, List<IKeyAction>>();
        private SecretIO Crypto = new SecretIO();

        private List<IKeyAction> CurrentKeyActionList { get; set; }

        private string _filePath = string.Empty;
        private string FilePath { get => string.IsNullOrEmpty(_filePath) ? "save.bk" : _filePath; }

        private Add AddForm { get; set; }
        private Profile ProfileForm { get; set; }
        private bool GlobalDisable { get; set; } = false;

        public BindKey(string filePath)
        {
            InitializeComponent();
            _filePath = filePath;
            LoadSavedData();
            this.FormClosing += new FormClosingEventHandler(CloseButtonClicked);
        }

        private void LoadSavedData()
        {
            var selectedProfileName = string.Empty;
            var currentProfileName = string.Empty;
            var currentProfileList = new List<IKeyAction>();
            List<string> loadedData = new List<string>();
            string rawText = string.Empty;

            try
            {
                using (StreamReader sr = new StreamReader(FilePath))
                {
                    while (sr.Peek() != -1)
                    {
                        rawText += sr.ReadLine();
                    }
                    sr.Close();
                }

                Crypto.Decrypt(rawText).Split(new[] { Environment.NewLine }, StringSplitOptions.None).ToList().ForEach(s =>
                {
                    loadedData.Add(s);
                });
            }
            catch (Exception)
            {
                EnableDisableNonProfileControls(false);
            }

            // first get selected profile name
            if (loadedData.Count > 0 && string.IsNullOrWhiteSpace(loadedData[0]) == false)
            {
                selectedProfileName = loadedData[0];
                selectedProfileName = selectedProfileName.Substring(3, selectedProfileName.Length - 3);
                loadedData.RemoveAt(0);
            }

            string line = string.Empty;
            while (loadedData.Count > 0 && string.IsNullOrWhiteSpace(loadedData[0]) == false)
            {
                line = loadedData[0];
                if (line.Substring(0, 3) == "***")
                {
                    if (string.IsNullOrEmpty(currentProfileName) == false)
                    {
                        // add previous profile
                        AddProfile(currentProfileName, currentProfileList);
                        currentProfileList = new List<IKeyAction>();
                    }
                    currentProfileName = line.Substring(3, line.Length - 3);
                }
                else
                {
                    CreateKeyActionFromSaveString(currentProfileList, line);
                }
                loadedData.RemoveAt(0);
            }

            // add final profile
            if (string.IsNullOrEmpty(currentProfileName) == false)
            {
                AddProfile(currentProfileName, currentProfileList);
            }

            var selectedKeyActionList = ProfileKeyActions.Where(kvp => kvp.Key == selectedProfileName).FirstOrDefault().Value;
            if (ProfileKeyActions.Any() && selectedKeyActionList != null)
            {
                CurrentKeyActionList = selectedKeyActionList;
                foreach (string profile in ProfileKeyActions.Keys)
                {
                    ProfileComboBox.Items.Add(profile);
                }
                ProfileComboBox.Text = selectedProfileName;
                UpdateNextActions();
                DrawListView();
                AddListeners();
            }
            else
            {
                EnableDisableNonProfileControls(false);
            }
        }

        private void UpdateNextActions()
        {
            foreach (var keyActionList in ProfileKeyActions.Values)
            {
                UpdateNextActionsInGivenList(keyActionList);
            }
        }

        private void UpdateNextActionsInGivenList(List<IKeyAction> keyActions)
        {
            foreach (var keyAction in keyActions)
            {
                var nextAction = keyActions.FirstOrDefault(ka => ka.GUID == keyAction.NextKeyActionGUID);
                if (nextAction != null)
                {
                    keyAction.SetNextAction(nextAction);
                }
                else
                {
                    keyAction.SetNextAction(null);
                }
            }
        }

        private void AddProfile(string currentProfileName, List<IKeyAction> currentProfileList)
        {
            ProfileKeyActions[currentProfileName] = currentProfileList;
        }

        private void EnableDisableNonProfileControls(bool enable)
        {
            listView1.Enabled = enable;
            ButtonAdd.Enabled = enable;
            button1.Enabled = enable;
            button2.Enabled = enable;
        }

        private void CreateKeyActionFromSaveString(List<IKeyAction> profileList, string line)
        {
            try
            {
                string[] parts = DefaultKeyAction.DELIMITER_REGEX.Split(line);
                profileList.Add(KeyActionFactory.GetNewKeyActionOfType(parts));
            }
            catch (Exception)
            {
                // do nothing
            }
        }

        private void SaveItems()
        {
            string rawText = string.Empty;
            rawText += ($"***{ProfileComboBox.Text}" + Environment.NewLine);
            foreach (var kvp in ProfileKeyActions)
            {
                rawText += ($"***{kvp.Key}" + Environment.NewLine);
                foreach (IKeyAction keyAction in kvp.Value)
                {
                    rawText += (keyAction.SaveString + Environment.NewLine);
                }
            }
            rawText = Crypto.Encrypt(rawText);

            try
            {
                using (StreamWriter sw = File.CreateText(FilePath))
                {
                    sw.Write(rawText);
                    sw.Close();
                }
            }
            catch (Exception)
            {
                MessageBox.Show($"Could not save to file {FilePath}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CloseButtonClicked(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            this.Visible = false;
            this.ShowInTaskbar = false;
            e.Cancel = true;
        }

        private void AddListeners()
        {
            if (GlobalDisable) return;
            try
            {
                RecreateKeyboardHook();

                // bind key-combos to actions
                Dictionary<Combination, Action> combos = new Dictionary<Combination, Action>();
                foreach (IKeyAction keyAction in CurrentKeyActionList.Where(ka => ka.Enabled && string.IsNullOrWhiteSpace(ka.KeyCombo) == false))
                {
                    Combination combination = Combination.FromString(keyAction.KeyCombo);
                    Action action = keyAction.ExecuteActions;
                    combos[combination] = action;
                }
                m_GlobalHook.OnCombination(combos);
            }
            catch (Exception)
            {
                // do nothing
            }
        }

        private void RecreateKeyboardHook()
        {
            m_GlobalHook.Dispose();
            m_GlobalHook = Hook.GlobalEvents();
        }

        private void DrawListView()
        {
            listView1.Items.Clear();
            foreach (IKeyAction keyAction in CurrentKeyActionList)
            {
                var keyCombo = DefaultKeyAction.GetKeyCombo(keyAction.Keys, true);
                keyCombo = string.IsNullOrWhiteSpace(keyCombo) ? "None" : keyCombo;
                ListViewItem newItem = listView1.Items.Add(keyCombo);
                if (keyAction.Enabled == false)
                {
                    Font fnt = new Font(newItem.Font, FontStyle.Strikeout);
                    newItem.ForeColor = Color.FromArgb(100, 100, 100);
                    newItem.Font = fnt;
                }

                newItem.SubItems.Add(keyAction.ToString());
            }
            listView1.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.ColumnContent);
            listView1.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.HeaderSize);
            listView1.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.ColumnContent);
            if (listView1.Columns[1].Width < (400 - listView1.Columns[0].Width - 2))
            {
                listView1.Columns[1].Width = (400 - listView1.Columns[0].Width - 2);
            }
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            CreateAddForm(null);
        }

        private void AddFormClosed(object sender, FormClosedEventArgs e)
        {
            this.BringToFront();
            UpdateNextActionsInGivenList(this.CurrentKeyActionList);
            AddListeners();
            DrawListView();
            AddForm = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count != 0)
            {
                CreateAddForm(CurrentKeyActionList[listView1.SelectedItems[0].Index]);
            }
        }

        private void CreateAddForm(IKeyAction selectedAction)
        {
            if (AddForm == null && ProfileForm == null)
            {
                RecreateKeyboardHook();
                AddForm = new Add(CurrentKeyActionList, selectedAction);
                AddForm.StartPosition = FormStartPosition.Manual;
                AddForm.Location = new Point((this.Location.X + this.Width / 2) - Add.DIMENSIONS_DEFAULT.X / 2, (this.Location.Y + this.Height / 2));
                AddForm.FormClosed += new FormClosedEventHandler(AddFormClosed);
                AddForm.Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (AddForm == null && ProfileForm == null && listView1.SelectedItems.Count != 0)
            {
                RecreateKeyboardHook();
                CurrentKeyActionList.Remove(CurrentKeyActionList[listView1.SelectedItems[0].Index]);
                UpdateNextActionsInGivenList(this.CurrentKeyActionList);
                AddListeners();
                DrawListView();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveItems();
            this.Close();
            this.Dispose();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Visible = true;
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }

        private void disableAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (disableAllToolStripMenuItem.Text == "Disable All")
            {
                GlobalDisable = true;
                RecreateKeyboardHook();
                disableAllToolStripMenuItem.Text = "Enable All";
            }
            else
            {
                GlobalDisable = false;
                AddListeners();
                DrawListView();
                disableAllToolStripMenuItem.Text = "Disable All";
            }
        }

        private void ProfileAdd_Click(object sender, EventArgs e)
        {
            CreateProfileForm();
        }

        private void CreateProfileForm()
        {
            if (ProfileForm == null && AddForm == null)
            {
                ProfileForm = new Profile(ProfileKeyActions);
                ProfileForm.StartPosition = FormStartPosition.Manual;
                ProfileForm.Location = new Point((this.Location.X + this.Width / 2) - ProfileForm.Width / 2, (this.Location.Y + this.Height / 2) - ProfileForm.Height / 2);
                ProfileForm.FormClosed += new FormClosedEventHandler(ProfileFormClosed);
                ProfileForm.Show();
            }
        }

        private void ProfileFormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                this.BringToFront();
                ProfileForm = null;
                var addedItem = ProfileKeyActions.Where(kvp => ProfileComboBox.Items.Contains(kvp.Key) == false).FirstOrDefault().Key;
                if (addedItem != null)
                {
                    ProfileComboBox.Items.Add(addedItem);
                    ProfileComboBox.SelectedIndex = ProfileComboBox.Items.Count - 1;
                }
                EnableDisableNonProfileControls(ProfileComboBox.Items.Count > 0);
            }
            catch (Exception)
            {
                // do nothing
            }
        }

        private void ProfileRemove_Click(object sender, EventArgs e)
        {
            if (ProfileForm == null && AddForm == null)
            {
                var selectedItem = ProfileKeyActions.Where(kvp => kvp.Key == ProfileComboBox.SelectedItem.ToString()).FirstOrDefault();
                if (string.IsNullOrEmpty(selectedItem.Key) == false)
                {
                    ProfileKeyActions.Remove(selectedItem.Key);
                    ProfileComboBox.Items.RemoveAt(ProfileComboBox.SelectedIndex);
                    if (ProfileComboBox.Items.Count == 0)
                    {
                        EnableDisableNonProfileControls(false);
                        // this is handled by the selected index changed event 
                        // if a profile exists after deletion
                        CurrentKeyActionList = null;
                        listView1.Items.Clear();
                        RecreateKeyboardHook();
                    }
                    else
                    {
                        ProfileComboBox.SelectedIndex = 0;
                    }
                }
            }
        }

        private void ProfileComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var nextActionList = ProfileKeyActions.Where(kvp => kvp.Key == ProfileComboBox.Text).FirstOrDefault().Value;
            if (CurrentKeyActionList != nextActionList)
            {
                CurrentKeyActionList = nextActionList;
                DrawListView();
                AddListeners();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveItems();
        }
    }
}
