using Gma.System.MouseKeyHook;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BindKey.KeyActions;
using BindKey.Util;
using System.Linq;

namespace BindKey
{
    public partial class BindKey : Form
    {
        private IKeyboardMouseEvents m_GlobalHook = Hook.GlobalEvents();
        private KeyActionData Data { get; set; }
        private Add AddForm { get; set; }
        private Profile ProfileForm { get; set; }
        private bool GlobalDisable { get; set; } = false;

        private static BindKey SingletonInstance = null;
        public static BindKey GetInstance()
        {
            if (SingletonInstance == null)
            {
                throw new Exception("Attempted to access instance of BindKey before it was instantiated.");
            }
            return SingletonInstance;
        }

        public BindKey(string filePath)
        {
            InitializeComponent();
            this.Data = new KeyActionData(filePath);
            this.RefreshListAndKeyHooks();
            this.RefreshProfileList();
            this.FormClosing += new FormClosingEventHandler(CloseButtonClicked);
            SingletonInstance = this;
        }

        public void ShowBalloonTip(string title, string message)
        {
            notifyIcon1.ShowBalloonTip(1250, title, message, ToolTipIcon.Info);
        }

        private void RefreshListAndKeyHooks()
        {
            DrawListView();
            AddListeners();
        }

        private void RefreshProfileList()
        {
            ProfileComboBox.Items.Clear();
            if (Data.ProfileNames != null)
            {
                foreach (string profileName in Data.ProfileNames)
                {
                    ProfileComboBox.Items.Add(profileName);
                    if (profileName == Data.SelectedProfile)
                    {
                        ProfileComboBox.SelectedItem = profileName;
                    }
                }
            }
            EnableDisableNonProfileControls(ProfileComboBox.Items.Count > 0);
        }

        private IKeyAction ResolveSelectedKeyAction()
        {
            IKeyAction? res = null;
            try
            {
                if (listView1.SelectedItems.Count > 0)
                {
                    var selectedGUID = listView1.SelectedItems[0].Tag.ToString();
                    res = Data.SelectedActionMap[selectedGUID];
                }
            }
            catch
            {
                res = null;
            }
            return res;
        }

        private void EnableDisableNonProfileControls(bool enable)
        {
            listView1.Enabled = enable;
            ButtonAdd.Enabled = enable;
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

                var actionsToHook = Data.ActionsToHook;
                if (actionsToHook != null)
                {
                    // bind key-combos to actions
                    Dictionary<Combination, Action> combos = new Dictionary<Combination, Action>();
                    foreach (IKeyAction keyAction in actionsToHook)
                    {
                        Combination combination = Combination.FromString(keyAction.KeyCombo);
                        Action action = keyAction.ExecuteActions;
                        combos[combination] = action;
                    }
                    m_GlobalHook.OnCombination(combos);
                }
            }
            catch
            {
                RecreateKeyboardHook();
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
            if (Data.SelectedActionList != null)
            {
                foreach (IKeyAction keyAction in Data.SelectedActionList.OrderBy(ka => ka.Pinned == false))
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
                    if (keyAction.Pinned)
                    {
                        newItem.BackColor = Color.FromArgb(200, 200, 200);
                    }

                    newItem.SubItems.Add(keyAction.ToString());
                    newItem.Tag = keyAction.GUID;
                }
                listView1.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.ColumnContent);
                listView1.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.HeaderSize);
                listView1.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.ColumnContent);
                if (listView1.Columns[1].Width < (400 - listView1.Columns[0].Width - 2))
                {
                    listView1.Columns[1].Width = (400 - listView1.Columns[0].Width - 2);
                }
            }
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            CreateAddForm(null);
        }

        private void AddFormClosed(object sender, FormClosedEventArgs e)
        {
            this.BringToFront();
            this.Data.RefreshNextKeyActions();
            this.RefreshListAndKeyHooks();
            this.AddForm = null;
        }

        private void CreateAddForm(IKeyAction selectedAction)
        {
            if (AddForm == null && ProfileForm == null)
            {
                RecreateKeyboardHook();
                AddForm = new Add(Data, selectedAction);
                AddForm.StartPosition = FormStartPosition.Manual;
                AddForm.Location = new Point((this.Location.X + this.Width / 2) - Add.DIMENSIONS_DEFAULT.X / 2, (this.Location.Y + this.Height / 2));
                AddForm.FormClosed += new FormClosedEventHandler(AddFormClosed);
                AddForm.Show();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Data.SaveData();
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
                RefreshListAndKeyHooks();
                disableAllToolStripMenuItem.Text = "Disable All";
            }
        }

        private void ProfileAdd_Click(object sender, EventArgs e)
        {
            RecreateKeyboardHook();
            CreateProfileForm();
        }

        private void CreateProfileForm()
        {
            if (ProfileForm == null && AddForm == null)
            {
                ProfileForm = new Profile(Data);
                ProfileForm.StartPosition = FormStartPosition.Manual;
                ProfileForm.Location = new Point((this.Location.X + this.Width / 2) - ProfileForm.Width / 2, (this.Location.Y + this.Height / 2) - ProfileForm.Height / 2);
                ProfileForm.FormClosed += new FormClosedEventHandler(ProfileFormClosed);
                ProfileForm.Show();
            }
        }

        private void ProfileFormClosed(object sender, FormClosedEventArgs e)
        {
            this.BringToFront();
            ProfileForm = null;
            RefreshProfileList();
            RefreshListAndKeyHooks();
            EnableDisableNonProfileControls(ProfileComboBox.Items.Count > 0);
        }

        private void ProfileRemove_Click(object sender, EventArgs e)
        {
            if (ProfileForm == null && AddForm == null)
            {
                if (Data.RemoveProfile(ProfileComboBox.SelectedItem.ToString()))
                {
                    ProfileComboBox.Items.RemoveAt(ProfileComboBox.SelectedIndex);
                    if (ProfileComboBox.Items.Count == 0)
                    {
                        EnableDisableNonProfileControls(false);
                        Data.SelectedProfile = string.Empty;
                        listView1.Items.Clear();
                    }
                    else
                    {
                        Data.SelectedProfile = ProfileComboBox.Items[0].ToString();
                        ProfileComboBox.SelectedIndex = 0;
                    }
                }

                RefreshListAndKeyHooks();
            }
        }

        public string CycleProfile(bool forward = true)
        {
            string res = string.Empty;
            if (this.ProfileComboBox.Items.Count > 0)
            {
                int newIndex = forward ? this.ProfileComboBox.SelectedIndex + 1 : this.ProfileComboBox.SelectedIndex - 1;
                if (newIndex >= this.ProfileComboBox.Items.Count)
                {
                    newIndex = 0;
                }
                else if (newIndex < 0)
                {
                    newIndex = this.ProfileComboBox.Items.Count - 1;
                }
                res = ProfileComboBox.Items[newIndex].ToString();
                this.ProfileComboBox.SelectedIndex = newIndex;
            }
            return res;
        }

        private void ProfileComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ProfileComboBox.Text != Data.SelectedProfile)
            {
                Data.SelectedProfile = ProfileComboBox.Text;
                RefreshListAndKeyHooks();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Data.SaveData();
        }

        public void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (listView1.FocusedItem.Bounds.Contains(e.Location))
                {
                    var selectedKeyAction = ResolveSelectedKeyAction();
                    if (selectedKeyAction != null)
                    {
                        pinToolStripMenuItem.Text = selectedKeyAction.Pinned ? "Unpin" : "Pin";
                        ListItemMenuStrip.Show(Cursor.Position);
                    }
                }
            }
        }

        private void pinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AddForm == null && ProfileForm == null)
            {
                var selectedKeyAction = ResolveSelectedKeyAction();
                if (selectedKeyAction != null)
                {
                    Data.PinUnpinKeyAction(selectedKeyAction);
                    DrawListView();
                }
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedKeyAction = ResolveSelectedKeyAction();
            if (selectedKeyAction != null)
            {
                CreateAddForm(selectedKeyAction);
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AddForm == null && ProfileForm == null)
            {
                var selectedKeyAction = ResolveSelectedKeyAction();
                if (selectedKeyAction != null)
                {
                    if (Data.RemoveKeyAction(selectedKeyAction))
                    {
                        RefreshListAndKeyHooks();
                    }
                }
            }
        }
    }
}
