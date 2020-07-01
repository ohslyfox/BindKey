using BindKey.AddOptions;
using BindKey.KeyActions;
using Gma.System.MouseKeyHook;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace BindKey
{
    internal partial class Add : Form
    {
        #region dimension constants
        public static readonly Point DIMENSIONS_DEFAULT = new Point(288, 264);
        public static readonly Point DIMENSIONS_PROCESS = new Point(288, 376);
        public static readonly Point DIMENSIONS_SCREENCAPTURE = new Point(288, 486);
        public static readonly Point DIMENSIONS_KILLRESTARTPROCESS_1 = new Point(288, 364);
        public static readonly Point DIMENSIONS_KILLRESTARTPROCESS_2 = new Point(288, 400);
        public static readonly Point DIMENSIONS_KILLRESTARTPROCESS_3 = new Point(288, 444);
        public static readonly Point DIMENSIONS_KILLRESTARTPROCESS_PANEL_1 = new Point(248, 69);
        public static readonly Point DIMENSIONS_KILLRESTARTPROCESS_PANEL_2 = new Point(248, 105);
        public static readonly Point DIMENSIONS_KILLRESTARTPROCESS_PANEL_3 = new Point(248, 148);
        #endregion

        #region location constants
        public static readonly Point LOCATION_PANELS = new Point(12, 224);
        #endregion

        private enum ContextDependentFormState
        {
            First = 0,
            Second = 1,
            Third = 2
        }

        private PickKeyCombo KeyPicker { get; set; }
        private ScreenGrabber Grabber { get; set; }
        private IKeyAction LocalAction;
        private string LocalActionGUID { get => LocalAction == null ? string.Empty : LocalAction.GUID; }
        private List<IKeyAction> KeyActions;
        private List<IKeyAction> AvailableNextActions;
        public Keys[] Keys = new Keys[3];
        public Rectangle? SelectedRegion { get => Grabber.Region; }
        public IKeyAction NextAction { get => NextActionCombo.SelectedItem as IKeyAction; }

        public Add(List<IKeyAction> keyActions, IKeyAction selectedAction)
        {
            InitializeComponent();
            SetLocations();
            this.KeyActions = keyActions;
            this.KeyPicker = null;
            this.LocalAction = selectedAction;
            this.AvailableNextActions = selectedAction == null ? this.KeyActions : this.KeyActions.Where(ka => KeyActionMeetsDisplayCriteria(ka)).ToList();
            PopulateNextActions();
            PopulateSelectedAction(selectedAction);
        }

        private bool KeyActionMeetsDisplayCriteria(IKeyAction ka)
        {
            var temp = ka;
            while (temp != null)
            {
                if (temp.GUID == LocalActionGUID || temp.NextActionGUID == LocalActionGUID)
                {
                    return false;
                }
                temp = temp.NextAction;
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

        private void SetLocations()
        {
            foreach (Panel panel in this.Controls.OfType<Panel>())
            {
                panel.Location = LOCATION_PANELS;
            }
        }

        private void PopulateSelectedAction(IKeyAction selectedAction)
        {
            if (selectedAction != null)
            {
                LocalAction = selectedAction;
                NextActionCombo.SelectedItem = AvailableNextActions.FirstOrDefault(ka => ka.GUID == selectedAction.NextActionGUID);
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
                case ActionTypes.KillStartProcess:
                    FillFormControlsKillStartsProcessAction(selectedAction);
                    break;
            }
        }

        private void FillFormControlsKillStartsProcessAction(IKeyAction selectedAction)
        {
            KillStartProcessAction killRestartProcessAction = selectedAction as KillStartProcessAction;
            KillStartProcess.Checked = true;
            object boxedKeys = killRestartProcessAction.Keys.Clone();
            Keys[0] = ((Keys[])boxedKeys)[0];
            Keys[1] = ((Keys[])boxedKeys)[1];
            Keys[2] = ((Keys[])boxedKeys)[2];
            KillRestartProcessNameTextBox.Text = killRestartProcessAction.ProcessName;
            KillRestartRestartCheckBox.Checked = killRestartProcessAction.Restart;
            KillRestartAdminCheckbox.Checked = killRestartProcessAction.AsAdmin;
            CheckBoxEnabled.Checked = killRestartProcessAction.Enabled;
            checkBox2.Checked = killRestartProcessAction.MatchName;
            textBox3.Text = killRestartProcessAction.FilePath;
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
            textBox1.Text = openProcessAction.FilePath;
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
                    case ActionTypes.KillStartProcess:
                        FormAdjustKillRestartProcess(ResolveFormState(ActionTypes.KillStartProcess));
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

        private ContextDependentFormState ResolveFormState(ActionTypes type)
        {
            ContextDependentFormState res = ContextDependentFormState.First;
            switch (type)
            {
                case ActionTypes.KillStartProcess:
                    if (KillRestartRestartCheckBox.Checked)
                    {
                        res = ContextDependentFormState.Second;
                        if (checkBox2.Checked == false)
                        {
                            res = ContextDependentFormState.Third;
                        }
                    }
                    break;
            }
            return res;
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
                                     ka.KeyCombo == DefaultKeyAction.GetKeyCombo(Keys, false)))
            {
                MessageBox.Show("Error: an event is already bound to the key combo " + DefaultKeyAction.GetKeyCombo(Keys, true) + ".", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            switch (ResolveSelectedAction())
            {
                case ActionTypes.KillStartProcess:
                    if (string.IsNullOrEmpty(KillRestartProcessNameTextBox.Text))
                    {
                        MessageBox.Show("Error: a process name must be set.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    break;
                case ActionTypes.ScreenCapture:
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
            Point buttonLoc = new Point();
            buttonLoc.X = 11;
            buttonLoc.Y = panelProcess.Location.Y + panelProcess.Height + 6;
            ButtonSave.Location = buttonLoc;
        }

        private void FormAdjustScreenCapture()
        {
            ShowHidePanels(ActionTypes.ScreenCapture);
            this.Width = DIMENSIONS_SCREENCAPTURE.X;
            this.Height = DIMENSIONS_SCREENCAPTURE.Y;
            Point buttonLoc = new Point();
            buttonLoc.X = 11;
            buttonLoc.Y = PanelScreenCapture.Location.Y + PanelScreenCapture.Height + 6;
            ButtonSave.Location = buttonLoc;
        }

        private void FormAdjustKillRestartProcess(ContextDependentFormState formState)
        {
            ShowHidePanels(ActionTypes.KillStartProcess);
            switch (formState)
            {
                case ContextDependentFormState.First:
                    this.Width = DIMENSIONS_KILLRESTARTPROCESS_1.X;
                    this.Height = DIMENSIONS_KILLRESTARTPROCESS_1.Y;
                    this.PanelKillRestartProcess.Width = DIMENSIONS_KILLRESTARTPROCESS_PANEL_1.X;
                    this.PanelKillRestartProcess.Height = DIMENSIONS_KILLRESTARTPROCESS_PANEL_1.Y;
                    break;
                case ContextDependentFormState.Second:
                    this.Width = DIMENSIONS_KILLRESTARTPROCESS_2.X;
                    this.Height = DIMENSIONS_KILLRESTARTPROCESS_2.Y;
                    this.PanelKillRestartProcess.Width = DIMENSIONS_KILLRESTARTPROCESS_PANEL_2.X;
                    this.PanelKillRestartProcess.Height = DIMENSIONS_KILLRESTARTPROCESS_PANEL_2.Y;
                    break;
                case ContextDependentFormState.Third:
                    this.Width = DIMENSIONS_KILLRESTARTPROCESS_3.X;
                    this.Height = DIMENSIONS_KILLRESTARTPROCESS_3.Y;
                    this.PanelKillRestartProcess.Width = DIMENSIONS_KILLRESTARTPROCESS_PANEL_3.X;
                    this.PanelKillRestartProcess.Height = DIMENSIONS_KILLRESTARTPROCESS_PANEL_3.Y;
                    break;
            }
            Point buttonLoc = new Point();
            buttonLoc.X = 11;
            buttonLoc.Y = PanelKillRestartProcess.Location.Y + PanelKillRestartProcess.Height + 6;
            ButtonSave.Location = buttonLoc;
        }

        private void ShowHidePanels(ActionTypes type)
        {
            if (type == ActionTypes.None) return;
            panelProcess.Visible = type == ActionTypes.OpenProcess;
            PanelScreenCapture.Visible = type == ActionTypes.ScreenCapture;
            PanelKillRestartProcess.Visible = type == ActionTypes.KillStartProcess;
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
            FormAdjustKillRestartProcess(ResolveFormState(ActionTypes.KillStartProcess));
        }

        private void KillRestartRestartCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            KillRestartAdminCheckbox.Visible = !KillRestartAdminCheckbox.Visible;
            checkBox2.Visible = !checkBox2.Visible;
            FormAdjustKillRestartProcess(ResolveFormState(ActionTypes.KillStartProcess));
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
                openFileDialog.InitialDirectory = textBox1.Text == string.Empty ? Directory.GetCurrentDirectory() : textBox1.Text.Contains(".") ? textBox1.Text.Substring(0, textBox1.Text.LastIndexOf("\\")) : textBox1.Text;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Text = openFileDialog.FileName;
                }
            }
        }

        private void ButtonTakeScreenshotFileDialog_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.SelectedPath = textBox2.Text == string.Empty ? Directory.GetCurrentDirectory() : textBox2.Text.Contains(".") ? textBox2.Text.Substring(0, textBox2.Text.LastIndexOf("\\")) : textBox2.Text;
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    textBox2.Text = folderBrowserDialog.SelectedPath;
                }
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = textBox3.Text == string.Empty ? Directory.GetCurrentDirectory() : textBox3.Text.Contains(".") ? textBox3.Text.Substring(0, textBox3.Text.LastIndexOf("\\")) : textBox3.Text;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    textBox3.Text = openFileDialog.FileName;
                }
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            FormAdjustKillRestartProcess(ResolveFormState(ActionTypes.KillStartProcess));
        }


        private void button6_Click(object sender, EventArgs e)
        {
            foreach (Control c in this.Controls)
            {
                c.Enabled = false;
            }
            groupBox1.Enabled = true;
            button6.Enabled = false;
            TextBoxKeyCombo.Text = "Press up to three keys...";
            CreatePickKeyComboForm();
            this.Focus();
            TextBoxKeyCombo.Focus();
            TextBoxKeyCombo.Select(TextBoxKeyCombo.Text.Length, 0);
        }

        protected internal class ScreenGrabber
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

        protected internal class PickKeyCombo
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
