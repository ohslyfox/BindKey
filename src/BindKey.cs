using Gma.System.MouseKeyHook;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using BindKey.KeyActions;
using BindKey.Util;

namespace BindKey
{
    public partial class BindKey : Form
    {
        private KeyActionData Data { get; set; }
        private Add AddForm { get; set; }
        private Profile ProfileForm { get; set; }
        private bool GlobalDisable { get; set; }

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
            this.GlobalDisable = false;
            this.Data = new KeyActionData(filePath);
            this.RefreshListAndKeyHooks();
            this.RefreshProfileList();
            this.FormClosing += new FormClosingEventHandler(CloseButtonClicked);
            SingletonInstance = this;
        }

        public void ShowBalloonTip(string title, string message, ToolTipIcon icon)
        {
            notifyIcon1.ShowBalloonTip(1250, title, message, icon);
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
            ProfileComboBox.DropDownHeight = ProfileComboBox.Items.Count > 0 ? 150 : 17;
            EnableDisableNonProfileControls(ProfileComboBox.Items.Count > 0);
        }

        private IKeyAction ResolveSelectedKeyAction()
        {
            IKeyAction res = null;
            try
            {
                if (listView1.SelectedItems.Count > 0)
                {
                    var selectedGUID = listView1.SelectedItems[0].Tag.ToString();
                    if (Data.SelectedActionMap != null && Data.SelectedActionMap.ContainsKey(selectedGUID))
                    {
                        res = Data.SelectedActionMap[selectedGUID];
                    }
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

        private void EnableDisableControls(bool enable)
        {
            listView1.Enabled = enable;
            ButtonAdd.Enabled = enable;
            ProfileComboBox.Enabled = enable;
            ProfileAdd.Enabled = enable;
            ProfileRemove.Enabled = enable;
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
                HookManager.CleanHook();

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
                    HookManager.SetCombinationHook(combos);
                }
            }
            catch
            {
                HookManager.CleanHook();
            }
        }

        private void DrawListView()
        {
            listView1.Items.Clear();
            if (Data.SelectedActionList != null)
            {
                foreach (IKeyAction keyAction in Data.SelectedActionList.OrderBy(ka => ka.Pinned == false).ThenBy(ka => ka.KeyCombo))
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
                        newItem.BackColor = Color.FromArgb(230, 230, 230);
                    }

                    newItem.SubItems.Add($"{(keyAction.Pinned ? ">  " : string.Empty)}{keyAction}");
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
            BringToFront();
            EnableDisableControls(true);
            Data.RefreshNextKeyActions();
            RefreshListAndKeyHooks();
            AddForm = null;
        }

        private void CreateAddForm(IKeyAction selectedAction)
        {
            if (AddForm == null && ProfileForm == null)
            {
                EnableDisableControls(false);
                HookManager.CleanHook();
                AddForm = new Add(Data, selectedAction);
                AddForm.StartPosition = FormStartPosition.Manual;
                AddForm.Location = new Point((this.Location.X + this.Width / 2) - Add.DIMENSIONS_DEFAULT.X / 2, (this.Location.Y + this.Height / 2));
                AddForm.FormClosed += new FormClosedEventHandler(AddFormClosed);
                AddForm.Show();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Data.SaveData();
            Close();
            Dispose();
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
                HookManager.CleanHook();
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
            HookManager.CleanHook();
            CreateProfileForm();
        }

        private void CreateProfileForm()
        {
            if (ProfileForm == null && AddForm == null)
            {
                EnableDisableControls(false);
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
            EnableDisableControls(true);
            EnableDisableNonProfileControls(ProfileComboBox.Items.Count > 0);
        }

        private void ProfileRemove_Click(object sender, EventArgs e)
        {
            if (ProfileForm == null && AddForm == null)
            {
                if (Data.RemoveProfile(ProfileComboBox.SelectedItem?.ToString()))
                {
                    ProfileComboBox.Items.RemoveAt(ProfileComboBox.SelectedIndex);
                    RefreshProfileList();
                    if (ProfileComboBox.Items.Count > 0)
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

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && AddForm == null && ProfileForm == null)
            {
                if (listView1.FocusedItem.Bounds.Contains(e.Location))
                {
                    var selectedKeyAction = ResolveSelectedKeyAction();
                    if (selectedKeyAction != null)
                    {
                        pinToolStripMenuItem.Text = selectedKeyAction.Pinned ? "Unpin" : "Pin";
                        ListItemMenuStrip.Show(new Point(Cursor.Position.X - 12, Cursor.Position.Y - 4));
                    }
                }
            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && AddForm == null && ProfileForm == null)
            {
                if (listView1.FocusedItem.Bounds.Contains(e.Location))
                {
                    var selectedKeyAction = ResolveSelectedKeyAction();
                    if (selectedKeyAction != null)
                    {
                        CreateAddForm(selectedKeyAction);
                    }
                }
            }
        }

        private void listView1_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.NewWidth = this.listView1.Columns[e.ColumnIndex].Width;
            e.Cancel = true;
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

        private void githubToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/ohslyfox/BindKey");
        }
    }
}
