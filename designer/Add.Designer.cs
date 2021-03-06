﻿namespace BindKey
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
            this.panelProcess = new System.Windows.Forms.Panel();
            this.ButtonOpenProcessFileDialog = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.OpenFilePathTextBox = new System.Windows.Forms.TextBox();
            this.ActionGroupBox = new System.Windows.Forms.GroupBox();
            this.CheckBoxPinned = new System.Windows.Forms.CheckBox();
            this.ActionComboBox = new System.Windows.Forms.ComboBox();
            this.NextActionCombo = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.NextActionLabel = new System.Windows.Forms.Label();
            this.CheckBoxEnabled = new System.Windows.Forms.CheckBox();
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
            this.PanelCycleProfile = new System.Windows.Forms.Panel();
            this.RadioCycleBackward = new System.Windows.Forms.RadioButton();
            this.RadioCycleForward = new System.Windows.Forms.RadioButton();
            this.label9 = new System.Windows.Forms.Label();
            this.PanelFocusProcess = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.FocusProcessActionComboBox = new System.Windows.Forms.ComboBox();
            this.RefreshProcessButton2 = new System.Windows.Forms.Button();
            this.FocusProcessListView = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FocusProcessNameTextBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.CheckBoxNotify = new System.Windows.Forms.CheckBox();
            this.panelProcess.SuspendLayout();
            this.ActionGroupBox.SuspendLayout();
            this.PanelScreenCapture.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.PanelKillRestartProcess.SuspendLayout();
            this.KeyComboGroupBox.SuspendLayout();
            this.PanelDeleteFiles.SuspendLayout();
            this.PanelCycleProfile.SuspendLayout();
            this.PanelFocusProcess.SuspendLayout();
            this.SuspendLayout();
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
            this.panelProcess.Tag = "ActionPanel";
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
            this.ActionGroupBox.Controls.Add(this.CheckBoxNotify);
            this.ActionGroupBox.Controls.Add(this.CheckBoxPinned);
            this.ActionGroupBox.Controls.Add(this.ActionComboBox);
            this.ActionGroupBox.Controls.Add(this.NextActionCombo);
            this.ActionGroupBox.Controls.Add(this.label10);
            this.ActionGroupBox.Controls.Add(this.NextActionLabel);
            this.ActionGroupBox.Controls.Add(this.CheckBoxEnabled);
            this.ActionGroupBox.Location = new System.Drawing.Point(12, 85);
            this.ActionGroupBox.Name = "ActionGroupBox";
            this.ActionGroupBox.Size = new System.Drawing.Size(248, 85);
            this.ActionGroupBox.TabIndex = 16;
            this.ActionGroupBox.TabStop = false;
            this.ActionGroupBox.Text = "Options";
            // 
            // CheckBoxPinned
            // 
            this.CheckBoxPinned.AutoSize = true;
            this.CheckBoxPinned.Location = new System.Drawing.Point(189, 0);
            this.CheckBoxPinned.Name = "CheckBoxPinned";
            this.CheckBoxPinned.Size = new System.Drawing.Size(59, 17);
            this.CheckBoxPinned.TabIndex = 22;
            this.CheckBoxPinned.Text = "Pinned";
            this.CheckBoxPinned.UseVisualStyleBackColor = true;
            // 
            // ActionComboBox
            // 
            this.ActionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ActionComboBox.FormattingEnabled = true;
            this.ActionComboBox.Location = new System.Drawing.Point(77, 25);
            this.ActionComboBox.Name = "ActionComboBox";
            this.ActionComboBox.Size = new System.Drawing.Size(165, 21);
            this.ActionComboBox.TabIndex = 22;
            this.ActionComboBox.SelectedIndexChanged += new System.EventHandler(this.ActionComboBox_SelectedIndexChanged);
            // 
            // NextActionCombo
            // 
            this.NextActionCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.NextActionCombo.FormattingEnabled = true;
            this.NextActionCombo.Location = new System.Drawing.Point(77, 50);
            this.NextActionCombo.Name = "NextActionCombo";
            this.NextActionCombo.Size = new System.Drawing.Size(165, 21);
            this.NextActionCombo.TabIndex = 6;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 28);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(40, 13);
            this.label10.TabIndex = 23;
            this.label10.Text = "Action:";
            // 
            // NextActionLabel
            // 
            this.NextActionLabel.AutoSize = true;
            this.NextActionLabel.Location = new System.Drawing.Point(6, 53);
            this.NextActionLabel.Name = "NextActionLabel";
            this.NextActionLabel.Size = new System.Drawing.Size(65, 13);
            this.NextActionLabel.TabIndex = 20;
            this.NextActionLabel.Text = "Next Action:";
            // 
            // CheckBoxEnabled
            // 
            this.CheckBoxEnabled.AutoSize = true;
            this.CheckBoxEnabled.Checked = true;
            this.CheckBoxEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckBoxEnabled.Location = new System.Drawing.Point(70, 0);
            this.CheckBoxEnabled.Name = "CheckBoxEnabled";
            this.CheckBoxEnabled.Size = new System.Drawing.Size(65, 17);
            this.CheckBoxEnabled.TabIndex = 1;
            this.CheckBoxEnabled.Text = "Enabled";
            this.CheckBoxEnabled.UseVisualStyleBackColor = true;
            // 
            // ButtonSave
            // 
            this.ButtonSave.Enabled = false;
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
            this.PanelScreenCapture.Tag = "ActionPanel";
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
            this.PanelKillRestartProcess.Tag = "ActionPanel";
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
            this.PanelDeleteFiles.Tag = "ActionPanel";
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
            // PanelCycleProfile
            // 
            this.PanelCycleProfile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelCycleProfile.Controls.Add(this.RadioCycleBackward);
            this.PanelCycleProfile.Controls.Add(this.RadioCycleForward);
            this.PanelCycleProfile.Controls.Add(this.label9);
            this.PanelCycleProfile.Location = new System.Drawing.Point(363, 693);
            this.PanelCycleProfile.Name = "PanelCycleProfile";
            this.PanelCycleProfile.Size = new System.Drawing.Size(248, 62);
            this.PanelCycleProfile.TabIndex = 21;
            this.PanelCycleProfile.Tag = "ActionPanel";
            this.PanelCycleProfile.Visible = false;
            // 
            // RadioCycleBackward
            // 
            this.RadioCycleBackward.AutoSize = true;
            this.RadioCycleBackward.Location = new System.Drawing.Point(111, 28);
            this.RadioCycleBackward.Name = "RadioCycleBackward";
            this.RadioCycleBackward.Size = new System.Drawing.Size(73, 17);
            this.RadioCycleBackward.TabIndex = 2;
            this.RadioCycleBackward.Text = "Backward";
            this.RadioCycleBackward.UseVisualStyleBackColor = true;
            // 
            // RadioCycleForward
            // 
            this.RadioCycleForward.AutoSize = true;
            this.RadioCycleForward.Checked = true;
            this.RadioCycleForward.Location = new System.Drawing.Point(12, 30);
            this.RadioCycleForward.Name = "RadioCycleForward";
            this.RadioCycleForward.Size = new System.Drawing.Size(63, 17);
            this.RadioCycleForward.TabIndex = 1;
            this.RadioCycleForward.TabStop = true;
            this.RadioCycleForward.Text = "Forward";
            this.RadioCycleForward.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 11);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(81, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Cycle Direction:";
            // 
            // PanelFocusProcess
            // 
            this.PanelFocusProcess.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelFocusProcess.Controls.Add(this.label12);
            this.PanelFocusProcess.Controls.Add(this.FocusProcessActionComboBox);
            this.PanelFocusProcess.Controls.Add(this.RefreshProcessButton2);
            this.PanelFocusProcess.Controls.Add(this.FocusProcessListView);
            this.PanelFocusProcess.Controls.Add(this.FocusProcessNameTextBox);
            this.PanelFocusProcess.Controls.Add(this.label11);
            this.PanelFocusProcess.Location = new System.Drawing.Point(363, 761);
            this.PanelFocusProcess.Name = "PanelFocusProcess";
            this.PanelFocusProcess.Size = new System.Drawing.Size(248, 217);
            this.PanelFocusProcess.TabIndex = 22;
            this.PanelFocusProcess.Tag = "ActionPanel";
            this.PanelFocusProcess.Visible = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(9, 185);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(40, 13);
            this.label12.TabIndex = 23;
            this.label12.Text = "Action:";
            // 
            // FocusProcessActionComboBox
            // 
            this.FocusProcessActionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FocusProcessActionComboBox.FormattingEnabled = true;
            this.FocusProcessActionComboBox.Items.AddRange(new object[] {
            "Focus",
            "Minimize",
            "Maximize",
            "Minimize / Maximize",
            "Minimize / Focus"});
            this.FocusProcessActionComboBox.Location = new System.Drawing.Point(67, 182);
            this.FocusProcessActionComboBox.Name = "FocusProcessActionComboBox";
            this.FocusProcessActionComboBox.Size = new System.Drawing.Size(165, 21);
            this.FocusProcessActionComboBox.TabIndex = 24;
            // 
            // RefreshProcessButton2
            // 
            this.RefreshProcessButton2.Location = new System.Drawing.Point(11, 47);
            this.RefreshProcessButton2.Name = "RefreshProcessButton2";
            this.RefreshProcessButton2.Size = new System.Drawing.Size(221, 23);
            this.RefreshProcessButton2.TabIndex = 0;
            this.RefreshProcessButton2.TabStop = false;
            this.RefreshProcessButton2.Text = "Refresh List";
            this.RefreshProcessButton2.UseVisualStyleBackColor = true;
            this.RefreshProcessButton2.Click += new System.EventHandler(this.RefreshProcessButton_Click);
            // 
            // FocusProcessListView
            // 
            this.FocusProcessListView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FocusProcessListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.FocusProcessListView.FullRowSelect = true;
            this.FocusProcessListView.GridLines = true;
            this.FocusProcessListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.FocusProcessListView.HideSelection = false;
            this.FocusProcessListView.Location = new System.Drawing.Point(11, 76);
            this.FocusProcessListView.MultiSelect = false;
            this.FocusProcessListView.Name = "FocusProcessListView";
            this.FocusProcessListView.Size = new System.Drawing.Size(221, 100);
            this.FocusProcessListView.TabIndex = 0;
            this.FocusProcessListView.TabStop = false;
            this.FocusProcessListView.UseCompatibleStateImageBehavior = false;
            this.FocusProcessListView.View = System.Windows.Forms.View.Details;
            this.FocusProcessListView.SelectedIndexChanged += new System.EventHandler(this.KillProcessListView_SelectedIndexChanged);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Process Name";
            // 
            // FocusProcessNameTextBox
            // 
            this.FocusProcessNameTextBox.Location = new System.Drawing.Point(11, 21);
            this.FocusProcessNameTextBox.Name = "FocusProcessNameTextBox";
            this.FocusProcessNameTextBox.Size = new System.Drawing.Size(221, 20);
            this.FocusProcessNameTextBox.TabIndex = 0;
            this.FocusProcessNameTextBox.TabStop = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 6);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(79, 13);
            this.label11.TabIndex = 0;
            this.label11.Text = "Process Name:";
            // 
            // CheckBoxNotify
            // 
            this.CheckBoxNotify.AutoSize = true;
            this.CheckBoxNotify.Checked = true;
            this.CheckBoxNotify.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckBoxNotify.Location = new System.Drawing.Point(135, 0);
            this.CheckBoxNotify.Name = "CheckBoxNotify";
            this.CheckBoxNotify.Size = new System.Drawing.Size(53, 17);
            this.CheckBoxNotify.TabIndex = 23;
            this.CheckBoxNotify.Text = "Notify";
            this.CheckBoxNotify.UseVisualStyleBackColor = true;
            // 
            // Add
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 1161);
            this.Controls.Add(this.PanelFocusProcess);
            this.Controls.Add(this.PanelCycleProfile);
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
            this.PanelCycleProfile.ResumeLayout(false);
            this.PanelCycleProfile.PerformLayout();
            this.PanelFocusProcess.ResumeLayout(false);
            this.PanelFocusProcess.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelProcess;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox OpenFilePathTextBox;
        private System.Windows.Forms.GroupBox ActionGroupBox;
        private System.Windows.Forms.Button ButtonSave;
        private System.Windows.Forms.Panel PanelScreenCapture;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.CheckBox CheckBoxEnabled;
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
        private System.Windows.Forms.Panel PanelCycleProfile;
        private System.Windows.Forms.RadioButton RadioCycleBackward;
        private System.Windows.Forms.RadioButton RadioCycleForward;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox CheckBoxPinned;
        private System.Windows.Forms.ComboBox ActionComboBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel PanelFocusProcess;
        private System.Windows.Forms.Button RefreshProcessButton2;
        private System.Windows.Forms.ListView FocusProcessListView;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.TextBox FocusProcessNameTextBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox FocusProcessActionComboBox;
        private System.Windows.Forms.CheckBox CheckBoxNotify;
    }
}