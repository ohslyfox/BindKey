namespace BindKey
{
    partial class Add
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Add));
            this.OpenProcess = new System.Windows.Forms.RadioButton();
            this.panelProcess = new System.Windows.Forms.Panel();
            this.ButtonOpenProcessFileDialog = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.OpenFilePathTextBox = new System.Windows.Forms.TextBox();
            this.ActionGroupBox = new System.Windows.Forms.GroupBox();
            this.DeleteFiles = new System.Windows.Forms.RadioButton();
            this.NextActionCombo = new System.Windows.Forms.ComboBox();
            this.NextActionLabel = new System.Windows.Forms.Label();
            this.KillProcess = new System.Windows.Forms.RadioButton();
            this.CheckBoxEnabled = new System.Windows.Forms.CheckBox();
            this.ScreenCapture = new System.Windows.Forms.RadioButton();
            this.ButtonSave = new System.Windows.Forms.Button();
            this.PanelScreenCapture = new System.Windows.Forms.Panel();
            this.ButtonTakeScreenshotFileDialog = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button5 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.PanelKillRestartProcess = new System.Windows.Forms.Panel();
            this.RefreshProcessButton = new System.Windows.Forms.Button();
            this.KillProcessListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.KillRestartProcessNameTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.KeyComboGroupBox = new System.Windows.Forms.GroupBox();
            this.TextBoxKeyCombo = new System.Windows.Forms.TextBox();
            this.button6 = new System.Windows.Forms.Button();
            this.PanelDeleteFiles = new System.Windows.Forms.Panel();
            this.DeleteSearchPatternTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.DeleteFolderPathTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.DeleteSecondsLabel = new System.Windows.Forms.Label();
            this.DeleteMinutesLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.DeleteSecondsTextBox = new System.Windows.Forms.TextBox();
            this.DeleteMinutesTextBox = new System.Windows.Forms.TextBox();
            this.DeleteHoursTextBox = new System.Windows.Forms.TextBox();
            this.DeleteDaysTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panelProcess.SuspendLayout();
            this.ActionGroupBox.SuspendLayout();
            this.PanelScreenCapture.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.PanelKillRestartProcess.SuspendLayout();
            this.KeyComboGroupBox.SuspendLayout();
            this.PanelDeleteFiles.SuspendLayout();
            this.SuspendLayout();
            // 
            // OpenProcess
            // 
            this.OpenProcess.AutoSize = true;
            this.OpenProcess.Location = new System.Drawing.Point(9, 22);
            this.OpenProcess.Name = "OpenProcess";
            this.OpenProcess.Size = new System.Drawing.Size(161, 17);
            this.OpenProcess.TabIndex = 2;
            this.OpenProcess.TabStop = true;
            this.OpenProcess.Text = "Open Folder or Start Process";
            this.OpenProcess.UseVisualStyleBackColor = true;
            this.OpenProcess.CheckedChanged += new System.EventHandler(this.RadioProcess_CheckedChanged);
            // 
            // panelProcess
            // 
            this.panelProcess.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelProcess.Controls.Add(this.ButtonOpenProcessFileDialog);
            this.panelProcess.Controls.Add(this.checkBox1);
            this.panelProcess.Controls.Add(this.label3);
            this.panelProcess.Controls.Add(this.OpenFilePathTextBox);
            this.panelProcess.Location = new System.Drawing.Point(363, 23);
            this.panelProcess.Name = "panelProcess";
            this.panelProcess.Size = new System.Drawing.Size(248, 80);
            this.panelProcess.TabIndex = 15;
            this.panelProcess.Visible = false;
            // 
            // ButtonOpenProcessFileDialog
            // 
            this.ButtonOpenProcessFileDialog.Location = new System.Drawing.Point(157, 47);
            this.ButtonOpenProcessFileDialog.Name = "ButtonOpenProcessFileDialog";
            this.ButtonOpenProcessFileDialog.Size = new System.Drawing.Size(75, 23);
            this.ButtonOpenProcessFileDialog.TabIndex = 0;
            this.ButtonOpenProcessFileDialog.TabStop = false;
            this.ButtonOpenProcessFileDialog.Text = "Choose";
            this.ButtonOpenProcessFileDialog.UseVisualStyleBackColor = true;
            this.ButtonOpenProcessFileDialog.Click += new System.EventHandler(this.ButtonOpenProcessFileDialog_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(12, 51);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(124, 17);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.TabStop = false;
            this.checkBox1.Text = "Run As Administrator";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "File Path:";
            // 
            // OpenFilePathTextBox
            // 
            this.OpenFilePathTextBox.Location = new System.Drawing.Point(11, 21);
            this.OpenFilePathTextBox.Name = "OpenFilePathTextBox";
            this.OpenFilePathTextBox.Size = new System.Drawing.Size(221, 20);
            this.OpenFilePathTextBox.TabIndex = 0;
            this.OpenFilePathTextBox.TabStop = false;
            // 
            // ActionGroupBox
            // 
            this.ActionGroupBox.Controls.Add(this.DeleteFiles);
            this.ActionGroupBox.Controls.Add(this.NextActionCombo);
            this.ActionGroupBox.Controls.Add(this.NextActionLabel);
            this.ActionGroupBox.Controls.Add(this.KillProcess);
            this.ActionGroupBox.Controls.Add(this.CheckBoxEnabled);
            this.ActionGroupBox.Controls.Add(this.ScreenCapture);
            this.ActionGroupBox.Controls.Add(this.OpenProcess);
            this.ActionGroupBox.Location = new System.Drawing.Point(12, 85);
            this.ActionGroupBox.Name = "ActionGroupBox";
            this.ActionGroupBox.Size = new System.Drawing.Size(248, 146);
            this.ActionGroupBox.TabIndex = 16;
            this.ActionGroupBox.TabStop = false;
            this.ActionGroupBox.Text = "Action";
            // 
            // DeleteFiles
            // 
            this.DeleteFiles.AutoSize = true;
            this.DeleteFiles.Location = new System.Drawing.Point(9, 91);
            this.DeleteFiles.Name = "DeleteFiles";
            this.DeleteFiles.Size = new System.Drawing.Size(80, 17);
            this.DeleteFiles.TabIndex = 5;
            this.DeleteFiles.TabStop = true;
            this.DeleteFiles.Text = "Delete Files";
            this.DeleteFiles.UseVisualStyleBackColor = true;
            this.DeleteFiles.CheckedChanged += new System.EventHandler(this.DeleteFiles_CheckedChanged);
            // 
            // NextActionCombo
            // 
            this.NextActionCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.NextActionCombo.FormattingEnabled = true;
            this.NextActionCombo.Location = new System.Drawing.Point(77, 114);
            this.NextActionCombo.Name = "NextActionCombo";
            this.NextActionCombo.Size = new System.Drawing.Size(165, 21);
            this.NextActionCombo.TabIndex = 6;
            // 
            // NextActionLabel
            // 
            this.NextActionLabel.AutoSize = true;
            this.NextActionLabel.Location = new System.Drawing.Point(6, 117);
            this.NextActionLabel.Name = "NextActionLabel";
            this.NextActionLabel.Size = new System.Drawing.Size(65, 13);
            this.NextActionLabel.TabIndex = 20;
            this.NextActionLabel.Text = "Next Action:";
            // 
            // KillProcess
            // 
            this.KillProcess.AutoSize = true;
            this.KillProcess.Location = new System.Drawing.Point(9, 45);
            this.KillProcess.Name = "KillProcess";
            this.KillProcess.Size = new System.Drawing.Size(79, 17);
            this.KillProcess.TabIndex = 3;
            this.KillProcess.TabStop = true;
            this.KillProcess.Text = "Kill Process";
            this.KillProcess.UseVisualStyleBackColor = true;
            this.KillProcess.CheckedChanged += new System.EventHandler(this.KillRestartProcess_CheckedChanged);
            // 
            // CheckBoxEnabled
            // 
            this.CheckBoxEnabled.AutoSize = true;
            this.CheckBoxEnabled.Checked = true;
            this.CheckBoxEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckBoxEnabled.Location = new System.Drawing.Point(177, 0);
            this.CheckBoxEnabled.Name = "CheckBoxEnabled";
            this.CheckBoxEnabled.Size = new System.Drawing.Size(65, 17);
            this.CheckBoxEnabled.TabIndex = 1;
            this.CheckBoxEnabled.Text = "Enabled";
            this.CheckBoxEnabled.UseVisualStyleBackColor = true;
            // 
            // ScreenCapture
            // 
            this.ScreenCapture.AutoSize = true;
            this.ScreenCapture.Location = new System.Drawing.Point(9, 68);
            this.ScreenCapture.Name = "ScreenCapture";
            this.ScreenCapture.Size = new System.Drawing.Size(107, 17);
            this.ScreenCapture.TabIndex = 4;
            this.ScreenCapture.TabStop = true;
            this.ScreenCapture.Text = "Take Screenshot";
            this.ScreenCapture.UseVisualStyleBackColor = true;
            this.ScreenCapture.CheckedChanged += new System.EventHandler(this.RadioScreenshot_CheckedChanged);
            // 
            // ButtonSave
            // 
            this.ButtonSave.Location = new System.Drawing.Point(12, 310);
            this.ButtonSave.Name = "ButtonSave";
            this.ButtonSave.Size = new System.Drawing.Size(250, 23);
            this.ButtonSave.TabIndex = 17;
            this.ButtonSave.TabStop = false;
            this.ButtonSave.Text = "Save";
            this.ButtonSave.UseVisualStyleBackColor = true;
            this.ButtonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // PanelScreenCapture
            // 
            this.PanelScreenCapture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelScreenCapture.Controls.Add(this.ButtonTakeScreenshotFileDialog);
            this.PanelScreenCapture.Controls.Add(this.pictureBox1);
            this.PanelScreenCapture.Controls.Add(this.button5);
            this.PanelScreenCapture.Controls.Add(this.label2);
            this.PanelScreenCapture.Controls.Add(this.textBox2);
            this.PanelScreenCapture.Location = new System.Drawing.Point(363, 107);
            this.PanelScreenCapture.Name = "PanelScreenCapture";
            this.PanelScreenCapture.Size = new System.Drawing.Size(248, 190);
            this.PanelScreenCapture.TabIndex = 0;
            this.PanelScreenCapture.Visible = false;
            // 
            // ButtonTakeScreenshotFileDialog
            // 
            this.ButtonTakeScreenshotFileDialog.Location = new System.Drawing.Point(179, 19);
            this.ButtonTakeScreenshotFileDialog.Name = "ButtonTakeScreenshotFileDialog";
            this.ButtonTakeScreenshotFileDialog.Size = new System.Drawing.Size(53, 23);
            this.ButtonTakeScreenshotFileDialog.TabIndex = 0;
            this.ButtonTakeScreenshotFileDialog.TabStop = false;
            this.ButtonTakeScreenshotFileDialog.Text = "Choose";
            this.ButtonTakeScreenshotFileDialog.UseVisualStyleBackColor = true;
            this.ButtonTakeScreenshotFileDialog.Click += new System.EventHandler(this.ButtonTakeScreenshotFileDialog_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(11, 76);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(221, 100);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 18;
            this.pictureBox1.TabStop = false;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(11, 47);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(221, 23);
            this.button5.TabIndex = 0;
            this.button5.TabStop = false;
            this.button5.Text = "Select Region";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Folder Path:";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(11, 21);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(162, 20);
            this.textBox2.TabIndex = 0;
            this.textBox2.TabStop = false;
            // 
            // PanelKillRestartProcess
            // 
            this.PanelKillRestartProcess.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelKillRestartProcess.Controls.Add(this.RefreshProcessButton);
            this.PanelKillRestartProcess.Controls.Add(this.KillProcessListView);
            this.PanelKillRestartProcess.Controls.Add(this.KillRestartProcessNameTextBox);
            this.PanelKillRestartProcess.Controls.Add(this.label4);
            this.PanelKillRestartProcess.Location = new System.Drawing.Point(363, 302);
            this.PanelKillRestartProcess.Name = "PanelKillRestartProcess";
            this.PanelKillRestartProcess.Size = new System.Drawing.Size(248, 190);
            this.PanelKillRestartProcess.TabIndex = 18;
            this.PanelKillRestartProcess.Visible = false;
            // 
            // RefreshProcessButton
            // 
            this.RefreshProcessButton.Location = new System.Drawing.Point(11, 47);
            this.RefreshProcessButton.Name = "RefreshProcessButton";
            this.RefreshProcessButton.Size = new System.Drawing.Size(221, 23);
            this.RefreshProcessButton.TabIndex = 0;
            this.RefreshProcessButton.TabStop = false;
            this.RefreshProcessButton.Text = "Refresh List";
            this.RefreshProcessButton.UseVisualStyleBackColor = true;
            this.RefreshProcessButton.Click += new System.EventHandler(this.RefreshProcessButton_Click);
            // 
            // KillProcessListView
            // 
            this.KillProcessListView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KillProcessListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.KillProcessListView.FullRowSelect = true;
            this.KillProcessListView.GridLines = true;
            this.KillProcessListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.KillProcessListView.HideSelection = false;
            this.KillProcessListView.Location = new System.Drawing.Point(11, 76);
            this.KillProcessListView.MultiSelect = false;
            this.KillProcessListView.Name = "KillProcessListView";
            this.KillProcessListView.Size = new System.Drawing.Size(221, 100);
            this.KillProcessListView.TabIndex = 0;
            this.KillProcessListView.TabStop = false;
            this.KillProcessListView.UseCompatibleStateImageBehavior = false;
            this.KillProcessListView.View = System.Windows.Forms.View.Details;
            this.KillProcessListView.SelectedIndexChanged += new System.EventHandler(this.KillProcessListView_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Process Name";
            // 
            // KillRestartProcessNameTextBox
            // 
            this.KillRestartProcessNameTextBox.Location = new System.Drawing.Point(11, 21);
            this.KillRestartProcessNameTextBox.Name = "KillRestartProcessNameTextBox";
            this.KillRestartProcessNameTextBox.Size = new System.Drawing.Size(221, 20);
            this.KillRestartProcessNameTextBox.TabIndex = 0;
            this.KillRestartProcessNameTextBox.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Process Name:";
            // 
            // KeyComboGroupBox
            // 
            this.KeyComboGroupBox.Controls.Add(this.TextBoxKeyCombo);
            this.KeyComboGroupBox.Controls.Add(this.button6);
            this.KeyComboGroupBox.Location = new System.Drawing.Point(12, 3);
            this.KeyComboGroupBox.Name = "KeyComboGroupBox";
            this.KeyComboGroupBox.Size = new System.Drawing.Size(248, 75);
            this.KeyComboGroupBox.TabIndex = 19;
            this.KeyComboGroupBox.TabStop = false;
            this.KeyComboGroupBox.Text = "Key Combination";
            // 
            // TextBoxKeyCombo
            // 
            this.TextBoxKeyCombo.Location = new System.Drawing.Point(9, 20);
            this.TextBoxKeyCombo.Name = "TextBoxKeyCombo";
            this.TextBoxKeyCombo.ReadOnly = true;
            this.TextBoxKeyCombo.Size = new System.Drawing.Size(233, 20);
            this.TextBoxKeyCombo.TabIndex = 21;
            this.TextBoxKeyCombo.TabStop = false;
            this.TextBoxKeyCombo.Text = "KEY_COMBO";
            this.TextBoxKeyCombo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TextBoxKeyCombo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyComboTextBox_KeyDown);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(7, 46);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(236, 23);
            this.button6.TabIndex = 0;
            this.button6.Text = "Set Key Combo";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // PanelDeleteFiles
            // 
            this.PanelDeleteFiles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelDeleteFiles.Controls.Add(this.DeleteSearchPatternTextBox);
            this.PanelDeleteFiles.Controls.Add(this.label8);
            this.PanelDeleteFiles.Controls.Add(this.button1);
            this.PanelDeleteFiles.Controls.Add(this.DeleteFolderPathTextBox);
            this.PanelDeleteFiles.Controls.Add(this.label7);
            this.PanelDeleteFiles.Controls.Add(this.DeleteSecondsLabel);
            this.PanelDeleteFiles.Controls.Add(this.DeleteMinutesLabel);
            this.PanelDeleteFiles.Controls.Add(this.label6);
            this.PanelDeleteFiles.Controls.Add(this.label5);
            this.PanelDeleteFiles.Controls.Add(this.DeleteSecondsTextBox);
            this.PanelDeleteFiles.Controls.Add(this.DeleteMinutesTextBox);
            this.PanelDeleteFiles.Controls.Add(this.DeleteHoursTextBox);
            this.PanelDeleteFiles.Controls.Add(this.DeleteDaysTextBox);
            this.PanelDeleteFiles.Controls.Add(this.label1);
            this.PanelDeleteFiles.Location = new System.Drawing.Point(363, 498);
            this.PanelDeleteFiles.Name = "PanelDeleteFiles";
            this.PanelDeleteFiles.Size = new System.Drawing.Size(248, 180);
            this.PanelDeleteFiles.TabIndex = 20;
            this.PanelDeleteFiles.Visible = false;
            // 
            // DeleteSearchPatternTextBox
            // 
            this.DeleteSearchPatternTextBox.Location = new System.Drawing.Point(12, 75);
            this.DeleteSearchPatternTextBox.Name = "DeleteSearchPatternTextBox";
            this.DeleteSearchPatternTextBox.Size = new System.Drawing.Size(220, 20);
            this.DeleteSearchPatternTextBox.TabIndex = 0;
            this.DeleteSearchPatternTextBox.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 59);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "File Search Pattern:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(179, 25);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(53, 23);
            this.button1.TabIndex = 0;
            this.button1.TabStop = false;
            this.button1.Text = "Choose";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.ButtonDeleteFolderDialog_Click);
            // 
            // DeleteFolderPathTextBox
            // 
            this.DeleteFolderPathTextBox.Location = new System.Drawing.Point(12, 27);
            this.DeleteFolderPathTextBox.Name = "DeleteFolderPathTextBox";
            this.DeleteFolderPathTextBox.Size = new System.Drawing.Size(161, 20);
            this.DeleteFolderPathTextBox.TabIndex = 0;
            this.DeleteFolderPathTextBox.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 11);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Folder Path:";
            // 
            // DeleteSecondsLabel
            // 
            this.DeleteSecondsLabel.AutoSize = true;
            this.DeleteSecondsLabel.Location = new System.Drawing.Point(179, 131);
            this.DeleteSecondsLabel.Name = "DeleteSecondsLabel";
            this.DeleteSecondsLabel.Size = new System.Drawing.Size(49, 13);
            this.DeleteSecondsLabel.TabIndex = 0;
            this.DeleteSecondsLabel.Text = "Seconds";
            // 
            // DeleteMinutesLabel
            // 
            this.DeleteMinutesLabel.AutoSize = true;
            this.DeleteMinutesLabel.Location = new System.Drawing.Point(121, 131);
            this.DeleteMinutesLabel.Name = "DeleteMinutesLabel";
            this.DeleteMinutesLabel.Size = new System.Drawing.Size(44, 13);
            this.DeleteMinutesLabel.TabIndex = 0;
            this.DeleteMinutesLabel.Text = "Minutes";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(65, 131);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Hours";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 131);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Days";
            // 
            // DeleteSecondsTextBox
            // 
            this.DeleteSecondsTextBox.Location = new System.Drawing.Point(182, 147);
            this.DeleteSecondsTextBox.Name = "DeleteSecondsTextBox";
            this.DeleteSecondsTextBox.Size = new System.Drawing.Size(50, 20);
            this.DeleteSecondsTextBox.TabIndex = 0;
            this.DeleteSecondsTextBox.TabStop = false;
            this.DeleteSecondsTextBox.Text = "0";
            this.DeleteSecondsTextBox.TextChanged += new System.EventHandler(this.ForceNumericalEvent_TextChanged);
            this.DeleteSecondsTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ForceNumericalEvent_KeyPress);
            // 
            // DeleteMinutesTextBox
            // 
            this.DeleteMinutesTextBox.Location = new System.Drawing.Point(124, 147);
            this.DeleteMinutesTextBox.Name = "DeleteMinutesTextBox";
            this.DeleteMinutesTextBox.Size = new System.Drawing.Size(50, 20);
            this.DeleteMinutesTextBox.TabIndex = 0;
            this.DeleteMinutesTextBox.TabStop = false;
            this.DeleteMinutesTextBox.Text = "0";
            this.DeleteMinutesTextBox.TextChanged += new System.EventHandler(this.ForceNumericalEvent_TextChanged);
            this.DeleteMinutesTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ForceNumericalEvent_KeyPress);
            // 
            // DeleteHoursTextBox
            // 
            this.DeleteHoursTextBox.Location = new System.Drawing.Point(68, 147);
            this.DeleteHoursTextBox.Name = "DeleteHoursTextBox";
            this.DeleteHoursTextBox.Size = new System.Drawing.Size(50, 20);
            this.DeleteHoursTextBox.TabIndex = 0;
            this.DeleteHoursTextBox.TabStop = false;
            this.DeleteHoursTextBox.Text = "0";
            this.DeleteHoursTextBox.TextChanged += new System.EventHandler(this.ForceNumericalEvent_TextChanged);
            this.DeleteHoursTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ForceNumericalEvent_KeyPress);
            // 
            // DeleteDaysTextBox
            // 
            this.DeleteDaysTextBox.Location = new System.Drawing.Point(12, 147);
            this.DeleteDaysTextBox.Name = "DeleteDaysTextBox";
            this.DeleteDaysTextBox.Size = new System.Drawing.Size(50, 20);
            this.DeleteDaysTextBox.TabIndex = 0;
            this.DeleteDaysTextBox.TabStop = false;
            this.DeleteDaysTextBox.Text = "0";
            this.DeleteDaysTextBox.TextChanged += new System.EventHandler(this.ForceNumericalEvent_TextChanged);
            this.DeleteDaysTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ForceNumericalEvent_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 107);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Delete Files Older Than:";
            // 
            // Add
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1249, 860);
            this.Controls.Add(this.PanelDeleteFiles);
            this.Controls.Add(this.KeyComboGroupBox);
            this.Controls.Add(this.PanelKillRestartProcess);
            this.Controls.Add(this.PanelScreenCapture);
            this.Controls.Add(this.ButtonSave);
            this.Controls.Add(this.ActionGroupBox);
            this.Controls.Add(this.panelProcess);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Add";
            this.Text = "Add Event";
            this.Load += new System.EventHandler(this.Add_Load);
            this.panelProcess.ResumeLayout(false);
            this.panelProcess.PerformLayout();
            this.ActionGroupBox.ResumeLayout(false);
            this.ActionGroupBox.PerformLayout();
            this.PanelScreenCapture.ResumeLayout(false);
            this.PanelScreenCapture.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.PanelKillRestartProcess.ResumeLayout(false);
            this.PanelKillRestartProcess.PerformLayout();
            this.KeyComboGroupBox.ResumeLayout(false);
            this.KeyComboGroupBox.PerformLayout();
            this.PanelDeleteFiles.ResumeLayout(false);
            this.PanelDeleteFiles.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.RadioButton OpenProcess;
        private System.Windows.Forms.Panel panelProcess;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox OpenFilePathTextBox;
        private System.Windows.Forms.GroupBox ActionGroupBox;
        private System.Windows.Forms.Button ButtonSave;
        private System.Windows.Forms.RadioButton ScreenCapture;
        private System.Windows.Forms.Panel PanelScreenCapture;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.CheckBox CheckBoxEnabled;
        private System.Windows.Forms.RadioButton KillProcess;
        private System.Windows.Forms.Panel PanelKillRestartProcess;
        private System.Windows.Forms.TextBox KillRestartProcessNameTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox KeyComboGroupBox;
        private System.Windows.Forms.Button ButtonOpenProcessFileDialog;
        private System.Windows.Forms.Button ButtonTakeScreenshotFileDialog;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.TextBox TextBoxKeyCombo;
        private System.Windows.Forms.ComboBox NextActionCombo;
        private System.Windows.Forms.Label NextActionLabel;
        private System.Windows.Forms.ListView KillProcessListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button RefreshProcessButton;
        private System.Windows.Forms.Panel PanelDeleteFiles;
        private System.Windows.Forms.TextBox DeleteSecondsTextBox;
        private System.Windows.Forms.TextBox DeleteMinutesTextBox;
        private System.Windows.Forms.TextBox DeleteHoursTextBox;
        private System.Windows.Forms.TextBox DeleteDaysTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label DeleteSecondsLabel;
        private System.Windows.Forms.Label DeleteMinutesLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox DeleteFolderPathTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox DeleteSearchPatternTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RadioButton DeleteFiles;
    }
}