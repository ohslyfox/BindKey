﻿using Gma.System.MouseKeyHook;
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
        private KeyActionData Data { get; }
        private Add AddForm { get; set; }
        private Profile ProfileForm { get; set; }
        private Queue<NotificationMessage> MessageQueue { get; }
        private bool GlobalDisable { get; set; }

        private static BindKey SingletonInstance = null;
        private static BindKey GetInstance()
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
            this.MessageQueue = new Queue<NotificationMessage>();
            this.RefreshListAndKeyHooks();
            this.RefreshProfileList();
            this.FormClosing += new FormClosingEventHandler(CloseButtonClicked);
            this.Data.ProfileChanged += ProfileUpdated;
            SingletonInstance = this;
        }

        private readonly struct NotificationMessage
        {
            public string Title { get; }
            public string Message { get; }
            public ToolTipIcon Icon { get; }

            public NotificationMessage(string title, string message, ToolTipIcon icon)
            {
                this.Title = title;
                this.Message = message;
                this.Icon = icon;
            }
        }
        public static void ShowBalloonTip(string title, string message, ToolTipIcon icon)
        {
            GetInstance().notifyIcon1.ShowBalloonTip(1250, title, message, icon);
        }

        private static void ShowBalloonTip(NotificationMessage message)
        {
            ShowBalloonTip(message.Title, message.Message, message.Icon);
        }

        public static void AddMessage(string title, string message, ToolTipIcon icon)
        {
            GetInstance().MessageQueue.Enqueue(new NotificationMessage(title, message, icon));
        }

        private void NotificationClosedHandler(object sender, EventArgs e)
        {
            if (this.MessageQueue.Any())
            {
                var message = this.MessageQueue.Dequeue();
                ShowBalloonTip(message);
            }
            else
            {
                this.notifyIcon1.BalloonTipClosed -= NotificationClosedHandler;
            }
        }

        public static void NotifyAllMessages()
        {
            var instance = GetInstance();
            if (instance.MessageQueue.Any())
            {
                instance.notifyIcon1.BalloonTipClosed += instance.NotificationClosedHandler;
                var message = instance.MessageQueue.Dequeue();
                ShowBalloonTip(message);
            }
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
            EnableDisableProfileRelatedControls(ProfileComboBox.Items.Count > 0);
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

        private void EnableDisableProfileRelatedControls(bool enable)
        {
            listView1.Enabled = enable;
            ButtonAdd.Enabled = enable;
            ProfileRemove.Enabled = enable;
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
            if (AddForm == null && ProfileForm == null && Data.ProfileNames.Any())
            {
                EnableDisableControls(false);
                HookManager.CleanHook();
                AddForm = new Add(Data, selectedAction);
                AddForm.StartPosition = FormStartPosition.Manual;
                AddForm.Location = new Point((this.Location.X + this.Width / 2) - Add.DIMENSIONS_DEFAULT.X / 2, this.Location.Y);
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
                AddListeners();
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
            EnableDisableProfileRelatedControls(ProfileComboBox.Items.Count > 0);
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

        private void ProfileUpdated(object sender, ProfileNameChangedEventArgs e)
        {
            for (int i = 0; i < ProfileComboBox.Items.Count; i++)
            {
                if (ProfileComboBox.GetItemText(ProfileComboBox.Items[i]) == e.NewProfileName)
                {
                    this.ProfileComboBox.SelectedIndex = i;
                    return;
                }
            }
        }

        private void ProfileComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Data.SelectedProfile = ProfileComboBox.Text;
            notifyIcon1.Text = $"BindKey{(string.IsNullOrWhiteSpace(ProfileComboBox.Text) ? string.Empty : $" - {ProfileComboBox.Text}")}";
            RefreshListAndKeyHooks();
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
                        disableToolStripMenuItem.Text = selectedKeyAction.Enabled ? "Disable" : "Enable";
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

        private void disableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AddForm == null && ProfileForm == null)
            {
                var selectedKeyAction = ResolveSelectedKeyAction();
                if (selectedKeyAction != null)
                {
                    Data.EnableDisableKeyAction(selectedKeyAction);
                    RefreshListAndKeyHooks();
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
            try
            {
                System.Diagnostics.Process.Start("https://github.com/ohslyfox/BindKey");
            }
            catch (Exception ex)
            {
                ShowBalloonTip("BindKey", ex.Message, ToolTipIcon.Error);
            }
        }
    }
}
