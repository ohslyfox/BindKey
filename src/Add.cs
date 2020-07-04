using BindKey.AddOptions;
using BindKey.KeyActions;
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
        public const int DEFAULT_BOTTOM_OF_FORM = TOP_OF_FORM_MARGIN + KEY_COMBO_GROUPBOX_HEIGHT + CONTROL_MARGIN +
                                                  ACTION_GROUPBOX_HEIGHT + CONTROL_MARGIN;
        public const int HEADER_HEIGHT = 38;
        public const int CONTROL_MARGIN = 10;
        public const int TOP_OF_FORM_MARGIN = 3;
        public const int BOTTOM_OF_FORM_MARGIN = 10;
        public const int KEY_COMBO_GROUPBOX_HEIGHT = 75;
        public const int ACTION_GROUPBOX_HEIGHT = 146;
        public const int OPEN_PROCESS_PANEL_HEIGHT = 80;
        public const int SCREENSHOT_PROCESS_PANEL_HEIGHT = 190;
        public const int KILL_PROCESS_PANEL_HEIGHT = 190;
        public const int DELETE_FILES_PANEL_HEIGHT = 180;
        public const int SAVE_BUTTON_HEIGHT = 23;
        #endregion

        #region dimension point constants
        public static readonly Point DIMENSIONS_DEFAULT = new Point(DEFAULT_FORM_WIDTH, HEADER_HEIGHT + DEFAULT_BOTTOM_OF_FORM);
        public static readonly Point DIMENSIONS_PROCESS = new Point(DEFAULT_FORM_WIDTH, DIMENSIONS_DEFAULT.Y + OPEN_PROCESS_PANEL_HEIGHT + CONTROL_MARGIN +
                                                                                        SAVE_BUTTON_HEIGHT + BOTTOM_OF_FORM_MARGIN);
        public static readonly Point DIMENSIONS_SCREENCAPTURE = new Point(DEFAULT_FORM_WIDTH, DIMENSIONS_DEFAULT.Y + SCREENSHOT_PROCESS_PANEL_HEIGHT + CONTROL_MARGIN +
                                                                                              SAVE_BUTTON_HEIGHT + BOTTOM_OF_FORM_MARGIN);
        public static readonly Point DIMENSIONS_KILLPROCESS = new Point(DEFAULT_FORM_WIDTH, DIMENSIONS_DEFAULT.Y + KILL_PROCESS_PANEL_HEIGHT + CONTROL_MARGIN +
                                                                                            SAVE_BUTTON_HEIGHT + BOTTOM_OF_FORM_MARGIN);
        public static readonly Point DIMENSIONS_DELETEFILES = new Point(DEFAULT_FORM_WIDTH, DIMENSIONS_DEFAULT.Y + DELETE_FILES_PANEL_HEIGHT + CONTROL_MARGIN +
                                                                                            SAVE_BUTTON_HEIGHT + BOTTOM_OF_FORM_MARGIN);
        #endregion

        #region location point constants
        public static readonly Point LOCATION_PANELS = new Point(DEFAULT_PANEL_X, DEFAULT_BOTTOM_OF_FORM);
        #endregion

        private PickKeyCombo KeyPicker { get; set; }
        private ScreenGrabber Grabber { get; set; }
        private IKeyAction LocalAction { get; set; }
        private string LocalActionGUID { get => LocalAction == null ? string.Empty : LocalAction.GUID; }
        private List<IKeyAction> KeyActions { get; set; }
        private List<IKeyAction> AvailableNextActions { get; set; }

        public Keys[] Keys { get; set; } = new Keys[3];
        public Rectangle? SelectedRegion { get => Grabber.Region; }
        public IKeyAction NextAction { get => NextActionCombo.SelectedItem as IKeyAction; }

        public Add(List<IKeyAction> keyActions, IKeyAction selectedAction)
        {
            this.KeyActions = keyActions;
            this.LocalAction = selectedAction;
            this.AvailableNextActions = selectedAction == null ? this.KeyActions : this.KeyActions.Where(ka => KeyActionMeetsDisplayCriteria(ka)).ToList();

            InitializeComponent();
            SetDefaultDimensionsAndLocations();
            PopulateNextActions();
            PopulateSelectedAction(selectedAction);
        }

        private bool KeyActionMeetsDisplayCriteria(IKeyAction ka)
        {
            var temp = ka;
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

        private void PopulateNextActions()
        {
            NextActionCombo.Items.Add(string.Empty);
            foreach (var action in AvailableNextActions)
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

        private void PopulateSelectedAction(IKeyAction selectedAction)
        {
            if (selectedAction != null)
            {
                LocalAction = selectedAction;
                NextActionCombo.SelectedItem = AvailableNextActions.FirstOrDefault(ka => ka.GUID == selectedAction.NextKeyActionGUID);
                FillFormControlsFromSelectedAction(selectedAction);
            }
            DrawKeyDisplay();
        }

        private void FillFormControlsFromSelectedAction(IKeyAction selectedAction)
        {
            switch (selectedAction.Type)
            {
                case ActionTypes.OpenProcess:
                    FillFormControlsOpenProcessAction(selectedAction);
                    break;
                case ActionTypes.ScreenCapture:
                    FillFormControlsScreenCaptureAction(selectedAction);
                    break;
                case ActionTypes.KillProcess:
                    FillFormControlsKillStartsProcessAction(selectedAction);
                    break;
                case ActionTypes.DeleteFiles:
                    FillFormControlsDeleteProcessAction(selectedAction);
                    break;
            }
        }

        private void FillFormControlsDeleteProcessAction(IKeyAction selectedAction)
        {
            DeleteFilesAction action = selectedAction as DeleteFilesAction;
            DeleteFiles.Checked = true;
            object boxedKeys = action.Keys.Clone();
            Keys[0] = ((Keys[])boxedKeys)[0];
            Keys[1] = ((Keys[])boxedKeys)[1];
            Keys[2] = ((Keys[])boxedKeys)[2];
            DeleteFolderPathTextBox.Text = action.FolderPath;
            DeleteSearchPatternTextBox.Text = action.SearchPattern;
            DeleteDaysTextBox.Text = action.Days;
            DeleteHoursTextBox.Text = action.Hours;
            DeleteMinutesTextBox.Text = action.Minutes;
            DeleteSecondsTextBox.Text = action.Seconds;
        }

        private void FillFormControlsKillStartsProcessAction(IKeyAction selectedAction)
        {
            KillProcessAction killRestartProcessAction = selectedAction as KillProcessAction;
            KillProcess.Checked = true;
            object boxedKeys = killRestartProcessAction.Keys.Clone();
            Keys[0] = ((Keys[])boxedKeys)[0];
            Keys[1] = ((Keys[])boxedKeys)[1];
            Keys[2] = ((Keys[])boxedKeys)[2];
            KillRestartProcessNameTextBox.Text = killRestartProcessAction.ProcessName;
            CheckBoxEnabled.Checked = killRestartProcessAction.Enabled;
        }

        private void FillFormControlsScreenCaptureAction(IKeyAction selectedAction)
        {
            ScreenCaptureAction screenCaptureAction = selectedAction as ScreenCaptureAction;
            ScreenCapture.Checked = true;
            object boxedKeys = screenCaptureAction.Keys.Clone();
            Keys[0] = ((Keys[])boxedKeys)[0];
            Keys[1] = ((Keys[])boxedKeys)[1];
            Keys[2] = ((Keys[])boxedKeys)[2];
            textBox2.Text = screenCaptureAction.FolderPath;
            CheckBoxEnabled.Checked = screenCaptureAction.Enabled;
            Grabber = new ScreenGrabber(this, screenCaptureAction.ScreenRegion);
            Bitmap bmp = new Bitmap(screenCaptureAction.ScreenRegion.Width, screenCaptureAction.ScreenRegion.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bmp);
            g.CopyFromScreen(screenCaptureAction.ScreenRegion.X, screenCaptureAction.ScreenRegion.Y, 0, 0, bmp.Size);
            pictureBox1.Image = bmp;
        }

        private void FillFormControlsOpenProcessAction(IKeyAction selectedAction)
        {
            OpenProcessAction openProcessAction = selectedAction as OpenProcessAction;
            OpenProcess.Checked = true;
            object boxedKeys = openProcessAction.Keys.Clone();
            Keys[0] = ((Keys[])boxedKeys)[0];
            Keys[1] = ((Keys[])boxedKeys)[1];
            Keys[2] = ((Keys[])boxedKeys)[2];
            OpenFilePathTextBox.Text = openProcessAction.FilePath;
            checkBox1.Checked = openProcessAction.AsAdmin;
            CheckBoxEnabled.Checked = openProcessAction.Enabled;
        }

        private void AdjustFormSizeOnLoad()
        {
            if (this.LocalAction != null)
            {
                this.Text = "Edit Event";
                switch (this.LocalAction.Type)
                {
                    case ActionTypes.OpenProcess:
                        FormAdjustProcess();
                        break;
                    case ActionTypes.ScreenCapture:
                        FormAdjustScreenCapture();
                        break;
                    case ActionTypes.KillProcess:
                        FormAdjustKillRestartProcess();
                        break;
                    case ActionTypes.DeleteFiles:
                        FormAdjustDeleteFiles();
                        break;
                }
            }
            else
            {
                this.Width = DIMENSIONS_DEFAULT.X;
                this.Height = DIMENSIONS_DEFAULT.Y;
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

        private void DrawKeyDisplay()
        {
            TextBoxKeyCombo.Text = DefaultKeyAction.GetKeyCombo(this.Keys, true);
        }

        private void KeyComboTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (KeyPicker == null && e.KeyCode == System.Windows.Forms.Keys.Back)
            {
                this.Keys = new Keys[DefaultKeyAction.KEY_COUNT];
                DrawKeyDisplay();
            }
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                ActionTypes type = ResolveSelectedAction();
                IAddOptions addOptions = AddOptionsFactory.GetAddOptionsOfType(type, this);
                IKeyAction newAction = KeyActionFactory.GetNewKeyActionOfType(type, addOptions, LocalActionGUID);

                if (LocalAction == null)
                {
                    KeyActions.Add(newAction);
                }
                else
                {
                    KeyActions[KeyActions.IndexOf(LocalAction)] = newAction;
                }
                this.Close();
                this.Dispose();
            }
        }

        private bool ValidateForm()
        {
            if (Keys.Where(k => k != System.Windows.Forms.Keys.None).Distinct().Count() != Keys.Where(k => k != System.Windows.Forms.Keys.None).Count())
            {
                MessageBox.Show("Error: duplicate keys in key combination.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (this.CheckBoxEnabled.Checked &&
                KeyActions.Any(ka => ka.Enabled &&
                                     ka.Equals(LocalAction) == false &&
                                     string.IsNullOrWhiteSpace(ka.KeyCombo) == false &&
                                     ka.KeyCombo == DefaultKeyAction.GetKeyCombo(Keys, false)))
            {
                MessageBox.Show("Error: an event is already bound to the key combo " + DefaultKeyAction.GetKeyCombo(Keys, true) + ".", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            switch (ResolveSelectedAction())
            {
                case ActionTypes.OpenProcess:
                    if (string.IsNullOrWhiteSpace(OpenFilePathTextBox.Text))
                    {
                        MessageBox.Show("Error: a process or folder name must be set.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    break;
                case ActionTypes.KillProcess:
                    if (string.IsNullOrWhiteSpace(KillRestartProcessNameTextBox.Text))
                    {
                        MessageBox.Show("Error: a process name must be set.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    break;
                case ActionTypes.ScreenCapture:
                    if (string.IsNullOrWhiteSpace(textBox2.Text))
                    {
                        MessageBox.Show("Error: a folder path must be set.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    if (Grabber != null)
                    {
                        if (Grabber.CalculateRegion() == null)
                        {
                            MessageBox.Show("Error: a screen region must be selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error: a screen region must be selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    break;
                case ActionTypes.DeleteFiles:
                    if (string.IsNullOrWhiteSpace(DeleteFolderPathTextBox.Text))
                    {
                        MessageBox.Show("Error: a folder path must be set.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    break;
            }
            return true;
        }

        private void CreatePickKeyComboForm()
        {
            if (KeyPicker == null)
            {
                KeyPicker = new PickKeyCombo(this);
            }
        }

        private void FormAdjustProcess()
        {
            ShowHidePanels(ActionTypes.OpenProcess);
            this.Width = DIMENSIONS_PROCESS.X;
            this.Height = DIMENSIONS_PROCESS.Y;
            ButtonSave.Location = new Point(DEFAULT_BUTTON_X, DEFAULT_BOTTOM_OF_FORM + OPEN_PROCESS_PANEL_HEIGHT + CONTROL_MARGIN);
        }

        private void FormAdjustScreenCapture()
        {
            ShowHidePanels(ActionTypes.ScreenCapture);
            this.Width = DIMENSIONS_SCREENCAPTURE.X;
            this.Height = DIMENSIONS_SCREENCAPTURE.Y;
            ButtonSave.Location = new Point(DEFAULT_BUTTON_X, DEFAULT_BOTTOM_OF_FORM + SCREENSHOT_PROCESS_PANEL_HEIGHT + CONTROL_MARGIN);
        }

        private void FormAdjustKillRestartProcess()
        {
            ShowHidePanels(ActionTypes.KillProcess);
            this.Width = DIMENSIONS_KILLPROCESS.X;
            this.Height = DIMENSIONS_KILLPROCESS.Y;
            ButtonSave.Location = new Point(DEFAULT_BUTTON_X, DEFAULT_BOTTOM_OF_FORM + KILL_PROCESS_PANEL_HEIGHT + CONTROL_MARGIN);
        }

        private void FormAdjustDeleteFiles()
        {
            ShowHidePanels(ActionTypes.DeleteFiles);
            this.Width = DIMENSIONS_DELETEFILES.X;
            this.Height = DIMENSIONS_DELETEFILES.Y;
            ButtonSave.Location = new Point(DEFAULT_BUTTON_X, DEFAULT_BOTTOM_OF_FORM + DELETE_FILES_PANEL_HEIGHT + CONTROL_MARGIN);
        }

        private void ShowHidePanels(ActionTypes type)
        {
            if (type == ActionTypes.None) return;
            panelProcess.Visible = type == ActionTypes.OpenProcess;
            PanelScreenCapture.Visible = type == ActionTypes.ScreenCapture;
            PanelKillRestartProcess.Visible = type == ActionTypes.KillProcess;
            PanelDeleteFiles.Visible = type == ActionTypes.DeleteFiles;
        }

        private void RadioProcess_CheckedChanged(object sender, EventArgs e)
        {
            FormAdjustProcess();
        }

        private void RadioScreenshot_CheckedChanged(object sender, EventArgs e)
        {
            FormAdjustScreenCapture();
        }

        private void KillRestartProcess_CheckedChanged(object sender, EventArgs e)
        {
            RefreshProcessListView();
            FormAdjustKillRestartProcess();
        }

        private void DeleteFiles_CheckedChanged(object sender, EventArgs e)
        {
            FormAdjustDeleteFiles();
        }

        private void RefreshProcessListView()
        {
            RefreshProcessButton.Enabled = false;
            KillRestartProcessNameTextBox.Text = string.Empty;
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
            Grabber = new ScreenGrabber(this, null);
            Grabber.SetMouseHook();
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
                folderBrowserDialog.SelectedPath = textBox2.Text == string.Empty ? Directory.GetCurrentDirectory() : textBox2.Text.Contains(".") ? textBox2.Text.Substring(0, textBox2.Text.LastIndexOf("\\")) : textBox2.Text;
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    return folderBrowserDialog.SelectedPath;
                }
            }
            return string.Empty;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            foreach (Control c in this.Controls)
            {
                c.Enabled = false;
            }
            KeyComboGroupBox.Enabled = true;
            button6.Enabled = false;
            TextBoxKeyCombo.Text = "Press up to three keys...";
            CreatePickKeyComboForm();
            this.Focus();
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

        private class ScreenGrabber
        {
            private IKeyboardMouseEvents m_GlobalHook = Hook.GlobalEvents();
            private List<Point> Points = new List<Point>();
            public Rectangle? Region { get; set; }
            private Add AddForm { get; set; }
            private int index { get; set; }
            public ScreenGrabber(Add addForm, Rectangle? region)
            {
                index = 0;
                AddForm = addForm;
                Region = region;
            }

            private void MouseDownHandler(object sender, MouseEventArgs e)
            {
                index++;

                Points.Add(new Point(Cursor.Position.X, Cursor.Position.Y));

                if (index == 1)
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
                        Bitmap bmp = new Bitmap(rec.Width, rec.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                        Graphics g = Graphics.FromImage(bmp);
                        g.CopyFromScreen(rec.X, rec.Y, 0, 0, bmp.Size);
                        AddForm.pictureBox1.Image = bmp;
                    }

                    m_GlobalHook.MouseDown -= MouseDownHandler;
                    m_GlobalHook.Dispose();
                }
            }

            public void SetMouseHook()
            {
                AddForm.button5.Enabled = false;
                AddForm.button5.Text = "Click first point.";
                Points = new List<Point>();
                index = 0;
                m_GlobalHook.MouseDown += MouseDownHandler;
            }

            public Rectangle? CalculateRegion()
            {
                if (Region != null)
                {
                    return (Rectangle)Region;
                }
                else if (Points.Count == 2 && Points[0] != Points[1])
                {
                    Rectangle rec = new Rectangle();
                    if (Points[0].X < Points[1].X &&
                        Points[0].Y < Points[1].Y)
                    {
                        rec.X = Points[0].X;
                        rec.Y = Points[0].Y;
                        rec.Width = Points[1].X - Points[0].X;
                        rec.Height = Points[1].Y - Points[0].Y;
                    }
                    else if (Points[0].X > Points[1].X &&
                             Points[0].Y < Points[1].Y)
                    {
                        rec.X = Points[1].X;
                        rec.Y = Points[0].Y;
                        rec.Width = Points[0].X - Points[1].X;
                        rec.Height = Points[1].Y - Points[0].Y;
                    }
                    else if (Points[0].X < Points[1].X &&
                             Points[0].Y > Points[1].Y)
                    {
                        rec.X = Points[0].X;
                        rec.Y = Points[1].Y;
                        rec.Width = Points[1].X - Points[0].X;
                        rec.Height = Points[0].Y - Points[1].Y;
                    }
                    else if (Points[0].X > Points[1].X &&
                             Points[0].Y > Points[1].Y)
                    {
                        rec.X = Points[1].X;
                        rec.Y = Points[1].Y;
                        rec.Width = Points[0].X - Points[1].X;
                        rec.Height = Points[0].Y - Points[1].Y;
                    }
                    return rec;
                }

                return null;
            }
        }

        private class PickKeyCombo
        {
            private IKeyboardMouseEvents m_GlobalHook = Hook.GlobalEvents();
            private Keys[] Keys { get; set; }
            private Add AddForm { get; set; }

            public PickKeyCombo(Add addForm)
            {
                this.AddForm = addForm;
                Keys = new Keys[DefaultKeyAction.KEY_COUNT];
                m_GlobalHook.KeyDown += GlobalKeyDown;
                m_GlobalHook.KeyUp += GlobalKeyUp;
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
                AddForm.TextBoxKeyCombo.Text = DefaultKeyAction.GetKeyCombo(Keys, true);
            }

            private void GlobalKeyUp(object sender, KeyEventArgs e)
            {
                // set keys
                AddForm.Keys = Keys;

                // update form
                foreach (Control c in AddForm.Controls)
                {
                    c.Enabled = true;
                }
                AddForm.button6.Enabled = true;
                AddForm.DrawKeyDisplay();
                AddForm.TextBoxKeyCombo.Select(AddForm.TextBoxKeyCombo.Text.Length, 0);
                AddForm.KeyPicker = null;

                // remove hook
                CleanGlobalHook();
            }

            private void CleanGlobalHook()
            {
                if (m_GlobalHook != null)
                {
                    m_GlobalHook.KeyDown -= GlobalKeyDown;
                    m_GlobalHook.KeyUp -= GlobalKeyUp;
                    m_GlobalHook.Dispose();
                }
            }
        }

        
    }
}
