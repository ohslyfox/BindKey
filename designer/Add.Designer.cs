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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.ActionGroupBox = new System.Windows.Forms.GroupBox();
            this.NextActionCombo = new System.Windows.Forms.ComboBox();
            this.NextActionLabel = new System.Windows.Forms.Label();
            this.KillStartProcess = new System.Windows.Forms.RadioButton();
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
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.button4 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.KillRestartAdminCheckbox = new System.Windows.Forms.CheckBox();
            this.KillRestartRestartCheckBox = new System.Windows.Forms.CheckBox();
            this.KillRestartProcessNameTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TextBoxKeyCombo = new System.Windows.Forms.TextBox();
            this.button6 = new System.Windows.Forms.Button();
            this.panelProcess.SuspendLayout();
            this.ActionGroupBox.SuspendLayout();
            this.PanelScreenCapture.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.PanelKillRestartProcess.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // OpenProcess
            // 
            this.OpenProcess.AutoSize = true;
            this.OpenProcess.Location = new System.Drawing.Point(9, 22);
            this.OpenProcess.Name = "OpenProcess";
            this.OpenProcess.Size = new System.Drawing.Size(161, 17);
            this.OpenProcess.TabIndex = 14;
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
            this.panelProcess.Controls.Add(this.textBox1);
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
            this.ButtonOpenProcessFileDialog.TabIndex = 17;
            this.ButtonOpenProcessFileDialog.Text = "Choose";
            this.ButtonOpenProcessFileDialog.UseVisualStyleBackColor = true;
            this.ButtonOpenProcessFileDialog.Click += new System.EventHandler(this.ButtonOpenProcessFileDialog_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(11, 51);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(124, 17);
            this.checkBox1.TabIndex = 16;
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
            this.label3.TabIndex = 16;
            this.label3.Text = "File Path:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(11, 21);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(221, 20);
            this.textBox1.TabIndex = 16;
            this.textBox1.TabStop = false;
            // 
            // ActionGroupBox
            // 
            this.ActionGroupBox.Controls.Add(this.NextActionCombo);
            this.ActionGroupBox.Controls.Add(this.NextActionLabel);
            this.ActionGroupBox.Controls.Add(this.KillStartProcess);
            this.ActionGroupBox.Controls.Add(this.CheckBoxEnabled);
            this.ActionGroupBox.Controls.Add(this.ScreenCapture);
            this.ActionGroupBox.Controls.Add(this.OpenProcess);
            this.ActionGroupBox.Location = new System.Drawing.Point(12, 84);
            this.ActionGroupBox.Name = "ActionGroupBox";
            this.ActionGroupBox.Size = new System.Drawing.Size(248, 131);
            this.ActionGroupBox.TabIndex = 16;
            this.ActionGroupBox.TabStop = false;
            this.ActionGroupBox.Text = "Action";
            // 
            // NextActionCombo
            // 
            this.NextActionCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.NextActionCombo.FormattingEnabled = true;
            this.NextActionCombo.Location = new System.Drawing.Point(77, 97);
            this.NextActionCombo.Name = "NextActionCombo";
            this.NextActionCombo.Size = new System.Drawing.Size(165, 21);
            this.NextActionCombo.TabIndex = 20;
            // 
            // NextActionLabel
            // 
            this.NextActionLabel.AutoSize = true;
            this.NextActionLabel.Location = new System.Drawing.Point(6, 100);
            this.NextActionLabel.Name = "NextActionLabel";
            this.NextActionLabel.Size = new System.Drawing.Size(65, 13);
            this.NextActionLabel.TabIndex = 20;
            this.NextActionLabel.Text = "Next Action:";
            // 
            // KillStartProcess
            // 
            this.KillStartProcess.AutoSize = true;
            this.KillStartProcess.Location = new System.Drawing.Point(9, 45);
            this.KillStartProcess.Name = "KillStartProcess";
            this.KillStartProcess.Size = new System.Drawing.Size(125, 17);
            this.KillStartProcess.TabIndex = 19;
            this.KillStartProcess.TabStop = true;
            this.KillStartProcess.Text = "Kill and Start Process";
            this.KillStartProcess.UseVisualStyleBackColor = true;
            this.KillStartProcess.CheckedChanged += new System.EventHandler(this.KillRestartProcess_CheckedChanged);
            // 
            // CheckBoxEnabled
            // 
            this.CheckBoxEnabled.AutoSize = true;
            this.CheckBoxEnabled.Checked = true;
            this.CheckBoxEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckBoxEnabled.Location = new System.Drawing.Point(177, 0);
            this.CheckBoxEnabled.Name = "CheckBoxEnabled";
            this.CheckBoxEnabled.Size = new System.Drawing.Size(65, 17);
            this.CheckBoxEnabled.TabIndex = 18;
            this.CheckBoxEnabled.Text = "Enabled";
            this.CheckBoxEnabled.UseVisualStyleBackColor = true;
            // 
            // ScreenCapture
            // 
            this.ScreenCapture.AutoSize = true;
            this.ScreenCapture.Location = new System.Drawing.Point(9, 68);
            this.ScreenCapture.Name = "ScreenCapture";
            this.ScreenCapture.Size = new System.Drawing.Size(107, 17);
            this.ScreenCapture.TabIndex = 15;
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
            this.PanelScreenCapture.Size = new System.Drawing.Size(248, 189);
            this.PanelScreenCapture.TabIndex = 17;
            this.PanelScreenCapture.Visible = false;
            // 
            // ButtonTakeScreenshotFileDialog
            // 
            this.ButtonTakeScreenshotFileDialog.Location = new System.Drawing.Point(179, 19);
            this.ButtonTakeScreenshotFileDialog.Name = "ButtonTakeScreenshotFileDialog";
            this.ButtonTakeScreenshotFileDialog.Size = new System.Drawing.Size(53, 23);
            this.ButtonTakeScreenshotFileDialog.TabIndex = 18;
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
            this.pictureBox1.Size = new System.Drawing.Size(221, 101);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 18;
            this.pictureBox1.TabStop = false;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(11, 47);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(221, 23);
            this.button5.TabIndex = 17;
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
            this.label2.TabIndex = 16;
            this.label2.Text = "Folder Path:";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(11, 21);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(162, 20);
            this.textBox2.TabIndex = 16;
            this.textBox2.TabStop = false;
            // 
            // PanelKillRestartProcess
            // 
            this.PanelKillRestartProcess.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelKillRestartProcess.Controls.Add(this.checkBox2);
            this.PanelKillRestartProcess.Controls.Add(this.button4);
            this.PanelKillRestartProcess.Controls.Add(this.label1);
            this.PanelKillRestartProcess.Controls.Add(this.textBox3);
            this.PanelKillRestartProcess.Controls.Add(this.KillRestartAdminCheckbox);
            this.PanelKillRestartProcess.Controls.Add(this.KillRestartRestartCheckBox);
            this.PanelKillRestartProcess.Controls.Add(this.KillRestartProcessNameTextBox);
            this.PanelKillRestartProcess.Controls.Add(this.label4);
            this.PanelKillRestartProcess.Location = new System.Drawing.Point(363, 302);
            this.PanelKillRestartProcess.Name = "PanelKillRestartProcess";
            this.PanelKillRestartProcess.Size = new System.Drawing.Size(248, 149);
            this.PanelKillRestartProcess.TabIndex = 18;
            this.PanelKillRestartProcess.Visible = false;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(12, 82);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(167, 17);
            this.checkBox2.TabIndex = 20;
            this.checkBox2.Text = "Match Start Process by Name";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.Visible = false;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(180, 116);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(52, 23);
            this.button4.TabIndex = 18;
            this.button4.Text = "Choose";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "File Path:";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(12, 118);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(162, 20);
            this.textBox3.TabIndex = 6;
            // 
            // KillRestartAdminCheckbox
            // 
            this.KillRestartAdminCheckbox.AutoSize = true;
            this.KillRestartAdminCheckbox.Location = new System.Drawing.Point(12, 65);
            this.KillRestartAdminCheckbox.Name = "KillRestartAdminCheckbox";
            this.KillRestartAdminCheckbox.Size = new System.Drawing.Size(124, 17);
            this.KillRestartAdminCheckbox.TabIndex = 5;
            this.KillRestartAdminCheckbox.Text = "Run As Administrator";
            this.KillRestartAdminCheckbox.UseVisualStyleBackColor = true;
            this.KillRestartAdminCheckbox.Visible = false;
            // 
            // KillRestartRestartCheckBox
            // 
            this.KillRestartRestartCheckBox.AutoSize = true;
            this.KillRestartRestartCheckBox.Location = new System.Drawing.Point(12, 47);
            this.KillRestartRestartCheckBox.Name = "KillRestartRestartCheckBox";
            this.KillRestartRestartCheckBox.Size = new System.Drawing.Size(101, 17);
            this.KillRestartRestartCheckBox.TabIndex = 2;
            this.KillRestartRestartCheckBox.Text = "Restart After Kill";
            this.KillRestartRestartCheckBox.UseVisualStyleBackColor = true;
            this.KillRestartRestartCheckBox.CheckedChanged += new System.EventHandler(this.KillRestartRestartCheckBox_CheckedChanged);
            // 
            // KillRestartProcessNameTextBox
            // 
            this.KillRestartProcessNameTextBox.Location = new System.Drawing.Point(11, 21);
            this.KillRestartProcessNameTextBox.Name = "KillRestartProcessNameTextBox";
            this.KillRestartProcessNameTextBox.Size = new System.Drawing.Size(221, 20);
            this.KillRestartProcessNameTextBox.TabIndex = 1;
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TextBoxKeyCombo);
            this.groupBox1.Controls.Add(this.button6);
            this.groupBox1.Location = new System.Drawing.Point(12, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(248, 75);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Key Combination";
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
            this.button6.TabIndex = 20;
            this.button6.TabStop = false;
            this.button6.Text = "Set Key Combo";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // Add
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1179, 735);
            this.Controls.Add(this.groupBox1);
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
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.RadioButton OpenProcess;
        private System.Windows.Forms.Panel panelProcess;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox ActionGroupBox;
        private System.Windows.Forms.Button ButtonSave;
        private System.Windows.Forms.RadioButton ScreenCapture;
        private System.Windows.Forms.Panel PanelScreenCapture;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.CheckBox CheckBoxEnabled;
        private System.Windows.Forms.RadioButton KillStartProcess;
        private System.Windows.Forms.Panel PanelKillRestartProcess;
        private System.Windows.Forms.CheckBox KillRestartRestartCheckBox;
        private System.Windows.Forms.TextBox KillRestartProcessNameTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox KillRestartAdminCheckbox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button ButtonOpenProcessFileDialog;
        private System.Windows.Forms.Button ButtonTakeScreenshotFileDialog;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.TextBox TextBoxKeyCombo;
        private System.Windows.Forms.ComboBox NextActionCombo;
        private System.Windows.Forms.Label NextActionLabel;
    }
}