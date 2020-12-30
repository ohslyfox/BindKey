using BindKey.AddOptions;
using BindKey.KeyActions;
using BindKey.Util;
using Gma.System.MouseKeyHook;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace BindKey
{
    internal partial class Add : Form
    {
        #region dimension constants
        public const int DEFAULT_BUTTON_X = 11;
        public const int DEFAULT_PANEL_X = 12;
        public const int DEFAULT_PANEL_WIDTH = 248;
        public const int DEFAULT_FORM_WIDTH = 288;
        public const int DEFAULT_BOTTOM_OF_FORM = TOP_OF_FORM_MARGIN +
                                                  KEY_COMBO_GROUPBOX_HEIGHT +
                                                  CONTROL_MARGIN +
                                                  ACTION_GROUPBOX_HEIGHT +
                                                  CONTROL_MARGIN;
        public const int HEADER_HEIGHT = 38;
        public const int CONTROL_MARGIN = 10;
        public const int TOP_OF_FORM_MARGIN = 3;
        public const int BOTTOM_OF_FORM_MARGIN = 10;
        public const int KEY_COMBO_GROUPBOX_HEIGHT = 75;
        public const int ACTION_GROUPBOX_HEIGHT = 174;
        public const int OPEN_PROCESS_PANEL_HEIGHT = 80;
        public const int SCREENSHOT_PROCESS_PANEL_HEIGHT = 190;
        public const int KILL_PROCESS_PANEL_HEIGHT = 190;
        public const int DELETE_FILES_PANEL_HEIGHT = 180;
        public const int CYCLE_PROFILE_PANEL_HEIGHT = 62;
        public const int SAVE_BUTTON_HEIGHT = 23;
        public static readonly Dictionary<ActionTypes, int> HEIGHT_DICT = new Dictionary<ActionTypes, int>
        {
            { ActionTypes.OpenProcess, OPEN_PROCESS_PANEL_HEIGHT },
            { ActionTypes.ScreenCapture, SCREENSHOT_PROCESS_PANEL_HEIGHT },
            { ActionTypes.KillProcess, KILL_PROCESS_PANEL_HEIGHT },
            { ActionTypes.DeleteFiles, DELETE_FILES_PANEL_HEIGHT },
            { ActionTypes.CycleProfile, CYCLE_PROFILE_PANEL_HEIGHT }
        };
        #endregion

        #region dimension point constants
        public static readonly Point DIMENSIONS_DEFAULT = new Point(DEFAULT_FORM_WIDTH, HEADER_HEIGHT +
                                                                                        DEFAULT_BOTTOM_OF_FORM);
        public static readonly Point DIMENSIONS_OPENPROCESS = new Point(DEFAULT_FORM_WIDTH, DIMENSIONS_DEFAULT.Y +
                                                                                        OPEN_PROCESS_PANEL_HEIGHT + CONTROL_MARGIN +
                                                                                        SAVE_BUTTON_HEIGHT +
                                                                                        BOTTOM_OF_FORM_MARGIN);
        public static readonly Point DIMENSIONS_SCREENCAPTURE = new Point(DEFAULT_FORM_WIDTH, DIMENSIONS_DEFAULT.Y +
                                                                                              SCREENSHOT_PROCESS_PANEL_HEIGHT +
                                                                                              CONTROL_MARGIN +
                                                                                              SAVE_BUTTON_HEIGHT +
                                                                                              BOTTOM_OF_FORM_MARGIN);
        public static readonly Point DIMENSIONS_KILLPROCESS = new Point(DEFAULT_FORM_WIDTH, DIMENSIONS_DEFAULT.Y +
                                                                                            KILL_PROCESS_PANEL_HEIGHT +
                                                                                            CONTROL_MARGIN +
                                                                                            SAVE_BUTTON_HEIGHT +
                                                                                            BOTTOM_OF_FORM_MARGIN);
        public static readonly Point DIMENSIONS_DELETEFILES = new Point(DEFAULT_FORM_WIDTH, DIMENSIONS_DEFAULT.Y +
                                                                                            DELETE_FILES_PANEL_HEIGHT +
                                                                                            CONTROL_MARGIN +
                                                                                            SAVE_BUTTON_HEIGHT +
                                                                                            BOTTOM_OF_FORM_MARGIN);
        public static readonly Point DIMENSIONS_CYCLE_PROFILE = new Point(DEFAULT_FORM_WIDTH, DIMENSIONS_DEFAULT.Y +
                                                                                              CYCLE_PROFILE_PANEL_HEIGHT +
                                                                                              CONTROL_MARGIN +
                                                                                              SAVE_BUTTON_HEIGHT +
                                                                                              BOTTOM_OF_FORM_MARGIN);
        public static readonly Dictionary<ActionTypes, Point> DIMENSIONS_DICT = new Dictionary<ActionTypes, Point>
        {
            { ActionTypes.OpenProcess, DIMENSIONS_OPENPROCESS },
            { ActionTypes.ScreenCapture, DIMENSIONS_SCREENCAPTURE},
            { ActionTypes.KillProcess, DIMENSIONS_KILLPROCESS },
            { ActionTypes.DeleteFiles, DIMENSIONS_DELETEFILES },
            { ActionTypes.CycleProfile, DIMENSIONS_CYCLE_PROFILE }
        };
        #endregion

        #region location point constants
        public static readonly Point LOCATION_PANELS = new Point(DEFAULT_PANEL_X, DEFAULT_BOTTOM_OF_FORM);
        #endregion

        public KeyActionData Data { get; }
        private PickKeyCombo KeyPicker { get; set; }
        private ScreenGrabber Grabber { get; set; }
        private string LocalActionGUID { get => LocalAction == null ? string.Empty : LocalAction.GUID; }
        public IKeyAction LocalAction { get; private set; }
        public Keys[] Keys { get => KeyPicker.Keys; }
        public Rectangle? SelectedRegion { get => Grabber.CalculateRegion(); set => Grabber.Region = value; }
        public IKeyAction NextAction { get => NextActionCombo.SelectedItem as IKeyAction; }

        public Add(KeyActionData data, IKeyAction selectedAction)
        {
            this.Data = data;
            this.LocalAction = selectedAction;
            this.KeyPicker = new PickKeyCombo(this);
            this.Grabber = new ScreenGrabber(this);
            var availableNextActions = selectedAction == null ? 
                                       Data.SelectedActionList.Where(ka => !ka.Pinned) : 
                                       Data.SelectedActionList.Where(ka => KeyActionMeetsDisplayCriteria(ka, selectedAction));

            InitializeComponent();
            SetDefaultDimensionsAndLocations();
            PopulateNextActions(availableNextActions);
            PopulateSelectedAction(selectedAction, availableNextActions);
            RefreshProcessListView();
        }

        private bool KeyActionMeetsDisplayCriteria(IKeyAction ka, IKeyAction selectedAction)
        {
            var temp = ka;

            if (selectedAction.Pinned && !ka.Pinned) return false;
            while (temp != null)
            {
                if (temp.GUID == LocalActionGUID || temp.NextKeyActionGUID == LocalActionGUID)
                {
                    return false;
                }
                temp = temp.NextKeyAction;
            }

            return true;
        }

        private void PopulateNextActions(IEnumerable<IKeyAction> availableNextActions)
        {
            NextActionCombo.Items.Add(string.Empty);
            foreach (var action in availableNextActions)
            {
                NextActionCombo.Items.Add(action);
            }
        }

        private void SetDefaultDimensionsAndLocations()
        {
            foreach (Panel panel in this.Controls.OfType<Panel>())
            {
                panel.Location = LOCATION_PANELS;
                panel.Width = DEFAULT_PANEL_WIDTH;
            }
            foreach (GroupBox groupBox in this.Controls.OfType<GroupBox>())
            {
                groupBox.Location = new Point(DEFAULT_PANEL_X, groupBox.Location.Y);
                groupBox.Width = DEFAULT_PANEL_WIDTH;
            }

            this.KeyComboGroupBox.Height = KEY_COMBO_GROUPBOX_HEIGHT;
            this.ActionGroupBox.Height = ACTION_GROUPBOX_HEIGHT;
        }

        private void PopulateSelectedAction(IKeyAction selectedAction, IEnumerable<IKeyAction> availableNextActions)
        {
            if (selectedAction != null)
            {
                LocalAction = selectedAction;
                NextActionCombo.SelectedItem = availableNextActions.FirstOrDefault(ka => ka.GUID == selectedAction.NextKeyActionGUID);
                IAddOptions addOptions = AddOptionsFactory.GetAddOptionsFromActionType(selectedAction.Type, this);
                addOptions.FillForm(selectedAction);
            }
            DrawKeyDisplay();
        }

        private void AdjustFormSizeOnLoad()
        {
            if (this.LocalAction != null)
            {
                this.Text = "Edit Event";
                FormAdjustOfType(this.LocalAction.Type);
            }
            else
            {
                this.Width = DIMENSIONS_DEFAULT.X;
                this.Height = DIMENSIONS_DEFAULT.Y;
            }
        }

        private void FormAdjustOfType(ActionTypes type)
        {
            if (DIMENSIONS_DICT.TryGetValue(type, out var point) && HEIGHT_DICT.TryGetValue(type, out var height))
            {
                ShowHidePanels(type);
                this.Width = point.X;
                this.Height = point.Y;
                ButtonSave.Location = new Point(DEFAULT_BUTTON_X, DEFAULT_BOTTOM_OF_FORM + height + CONTROL_MARGIN);
            }
        }

        private void Add_Load(object sender, EventArgs e)
        {
            AdjustFormSizeOnLoad();
        }

        private ActionTypes ResolveSelectedAction()
        {
            return (ActionTypes)Enum.Parse(typeof(ActionTypes), ActionGroupBox.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Name, true);
        }

        public void DrawKeyDisplay()
        {
            TextBoxKeyCombo.Text = DefaultKeyAction.GetKeyCombo(this.Keys, true);
        }

        private void KeyComboTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (button6.Enabled && e.KeyCode == System.Windows.Forms.Keys.Back)
            {
                this.KeyPicker.Keys = new Keys[DefaultKeyAction.KEY_COUNT];
                DrawKeyDisplay();
            }
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            ActionTypes type = ResolveSelectedAction();
            IAddOptions addOptions = AddOptionsFactory.GetAddOptionsFromActionType(type, this);
            if (addOptions.Validate())
            {
                IKeyAction newAction = KeyActionFactory.GetKeyActionFromActionType(type, addOptions, LocalActionGUID);

                if (LocalAction == null)
                {
                    Data.SelectedActionMap[newAction.GUID] = newAction;
                }
                else
                {
                    foreach (var dict in Data.ProfileMap.Values)
                    {
                        if (dict.ContainsKey(LocalAction.GUID))
                        {
                            dict[LocalAction.GUID] = newAction;
                        }
                    }
                }

                this.Close();
                this.Dispose();
            }
        }

        private void ShowHidePanels(ActionTypes type)
        {
            if (type == ActionTypes.None) return;
            panelProcess.Visible = type == ActionTypes.OpenProcess;
            PanelScreenCapture.Visible = type == ActionTypes.ScreenCapture;
            PanelKillRestartProcess.Visible = type == ActionTypes.KillProcess;
            PanelDeleteFiles.Visible = type == ActionTypes.DeleteFiles;
            PanelCycleProfile.Visible = type == ActionTypes.CycleProfile;
        }

        private void ActionRadio_CheckedChanged(object sender, EventArgs e)
        {
            FormAdjustOfType(ResolveSelectedAction());
        }

        private void RefreshProcessListView()
        {
            RefreshProcessButton.Enabled = false;
            KillProcessListView.Items.Clear();
            List<string> processes = Process.GetProcesses().Select(p => p.ProcessName).Distinct().ToList();
            processes.Sort();
            foreach (var process in processes)
            {
                KillProcessListView.Items.Add(process);
            }
            KillProcessListView.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.ColumnContent);
            RefreshProcessButton.Enabled = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            foreach (Control c in this.Controls)
            {
                c.Enabled = false;
            }
            this.Grabber.StartPicking();
        }

        private void ButtonOpenProcessFileDialog_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = OpenFilePathTextBox.Text == string.Empty ? Directory.GetCurrentDirectory() : OpenFilePathTextBox.Text.Contains(".") ? OpenFilePathTextBox.Text.Substring(0, OpenFilePathTextBox.Text.LastIndexOf("\\")) : OpenFilePathTextBox.Text;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    OpenFilePathTextBox.Text = openFileDialog.FileName;
                }
            }
        }

        private void ButtonTakeScreenshotFileDialog_Click(object sender, EventArgs e)
        {
            textBox2.Text = GetFolderPathDialogResult();
        }

        private void ButtonDeleteFolderDialog_Click(object sender, EventArgs e)
        {
            DeleteFolderPathTextBox.Text = GetFolderPathDialogResult();
        }

        private string GetFolderPathDialogResult()
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.SelectedPath = Directory.GetCurrentDirectory();
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    return folderBrowserDialog.SelectedPath;
                }
            }
            return string.Empty;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            button6.Enabled = false;
            foreach (Control c in this.Controls)
            {
                c.Enabled = false;
            }

            KeyComboGroupBox.Enabled = true;
            TextBoxKeyCombo.Text = "Press up to three keys...";
            KeyPicker.StartPicking();
            Focus();
            TextBoxKeyCombo.Focus();
            TextBoxKeyCombo.Select(TextBoxKeyCombo.Text.Length, 0);
        }

        private void ForceNumericalEvent_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (char.IsDigit(e.KeyChar) == false &&
                char.IsControl(e.KeyChar) == false)
            {
                e.Handled = true;
            }
        }

        private void ForceNumericalEvent_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text.Any(c => char.IsDigit(c) == false))
            {
                textBox.Text = "0";
            }

            if (textBox.Text == string.Empty)
            {
                textBox.Text = "0";
            }
        }

        private void RefreshProcessButton_Click(object sender, EventArgs e)
        {
            RefreshProcessListView();
        }

        private void KillProcessListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (KillProcessListView.SelectedItems.Count > 0)
            {
                KillRestartProcessNameTextBox.Text = KillProcessListView.SelectedItems[0].Text;
            }
        }

        public static Bitmap GetBitMapFromRegion(Rectangle rectangle)
        {
            Bitmap res = null;
            try
            {
                res = new Bitmap(rectangle.Width, rectangle.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                var g = Graphics.FromImage(res);
                g.CopyFromScreen(rectangle.X, rectangle.Y, 0, 0, res.Size);
            }
            catch (Exception)
            {
                // do nothing
            }
            return res;
        }

        private class ScreenGrabber
        {
            private Point?[] Points { get; set; }
            private Add AddForm { get; }
            private int ClickIndex { get; set; }
            public Rectangle? Region { private get; set; }

            public ScreenGrabber(Add addForm)
            {
                ClickIndex = 0;
                AddForm = addForm;
            }

            public void StartPicking()
            {
                HookManager.CleanHook();
                Region = null;
                ClickIndex = 0;
                Points = new Point?[2];
                AddForm.button5.Enabled = false;
                AddForm.button5.Text = "Click first point.";
                HookManager.SetMouseDown(GlobalMouseDown);
            }

            private void GlobalMouseDown(object sender, MouseEventArgs e)
            {
                Points[ClickIndex++] = new Point(Cursor.Position.X, Cursor.Position.Y);

                if (ClickIndex == 1)
                {
                    AddForm.button5.Text = "Click second point";
                }
                else
                {
                    AddForm.button5.Text = "Select Region";
                    AddForm.button5.Enabled = true;

                    this.Region = CalculateRegion();
                    if (Region != null)
                    {
                        Rectangle rec = (Rectangle)Region;
                        this.AddForm.pictureBox1.Image = GetBitMapFromRegion(rec);
                    }
                    else
                    {
                        this.AddForm.pictureBox1.Image = null;
                    }

                    HookManager.CleanHook();
                    foreach (Control c in this.AddForm.Controls)
                    {
                        c.Enabled = true;
                    }
                }
            }

            public Rectangle? CalculateRegion()
            {
                if (Region != null)
                {
                    return (Rectangle)Region;
                }
                else if (Points.All(p => p != null) && Points[0] != Points[1])
                {
                    Rectangle rec = new Rectangle();
                    var firstPoint = Points[0].Value;
                    var secondPoint = Points[1].Value;

                    if (firstPoint.X < secondPoint.X &&
                        firstPoint.Y < secondPoint.Y)
                    {
                        rec.X = firstPoint.X;
                        rec.Y = firstPoint.Y;
                        rec.Width = secondPoint.X - firstPoint.X;
                        rec.Height = secondPoint.Y - firstPoint.Y;
                    }
                    else if (firstPoint.X > secondPoint.X &&
                             firstPoint.Y < secondPoint.Y)
                    {
                        rec.X = secondPoint.X;
                        rec.Y = firstPoint.Y;
                        rec.Width = firstPoint.X - secondPoint.X;
                        rec.Height = secondPoint.Y - firstPoint.Y;
                    }
                    else if (firstPoint.X < secondPoint.X &&
                             firstPoint.Y > secondPoint.Y)
                    {
                        rec.X = firstPoint.X;
                        rec.Y = secondPoint.Y;
                        rec.Width = secondPoint.X - firstPoint.X;
                        rec.Height = firstPoint.Y - secondPoint.Y;
                    }
                    else if (firstPoint.X > secondPoint.X &&
                             firstPoint.Y > secondPoint.Y)
                    {
                        rec.X = secondPoint.X;
                        rec.Y = secondPoint.Y;
                        rec.Width = firstPoint.X - secondPoint.X;
                        rec.Height = firstPoint.Y - secondPoint.Y;
                    }
                    return rec;
                }

                return null;
            }
        }

        private class PickKeyCombo
        {
            private Add AddForm { get; set; }
            public Keys[] Keys { get; set; }

            public PickKeyCombo(Add addForm)
            {
                this.AddForm = addForm;
                this.Keys = new Keys[DefaultKeyAction.KEY_COUNT];
            }

            public void StartPicking()
            {
                Keys = new Keys[DefaultKeyAction.KEY_COUNT];
                HookManager.CleanHook();
                HookManager.SetKeyDown(GlobalKeyDown);
                HookManager.SetKeyUp(GlobalKeyUp);
            }

            private void GlobalKeyDown(object sender, KeyEventArgs e)
            {
                string[] parts = DefaultKeyAction.GetKeyCombo(Keys, true).Split('+');
                if (parts.Where(p => p == PrettyKeys.Convert(e.KeyCode)).Any()) return; // dont allow duplicate keys in the combo

                if (parts.Length < DefaultKeyAction.KEY_COUNT)
                {
                    for (int i = 0; i < Keys.Length; i++)
                    {
                        if (Keys[i] == System.Windows.Forms.Keys.None)
                        {
                            Keys[i] = e.KeyCode;
                            break;
                        }
                    }
                }
                AddForm.DrawKeyDisplay();
            }

            private void GlobalKeyUp(object sender, KeyEventArgs e)
            {
                // update form
                foreach (Control c in AddForm.Controls)
                {
                    c.Enabled = true;
                }
                AddForm.button6.Enabled = true;
                AddForm.DrawKeyDisplay();
                AddForm.TextBoxKeyCombo.Select(AddForm.TextBoxKeyCombo.Text.Length, 0);

                // remove hook
                HookManager.CleanHook();
            }
        }
    }
}
