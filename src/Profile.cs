using BindKey.KeyActions;
using BindKey.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BindKey
{
    internal partial class Profile : Form
    {
        private KeyActionData Data { get; }
        public Profile(KeyActionData data)
        {
            InitializeComponent();
            this.Data = data;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void Save()
        {
            if (ValidateForm())
            {
                string profileName = ProfileTextBox.Text.Trim();
                Data.AddProfile(profileName);
                Data.SelectedProfile = profileName;
                this.Close();
                this.Dispose();
            }
            else
            {
                ProfileTextBox.Text = string.Empty;
                ProfileTextBox.Focus();
            }
        }

        private void ProfileTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Save();
            }
        }

        private bool ValidateForm()
        {
            bool res = true;
            if (Data.ProfileMap.Any(kvp => kvp.Key.ToUpper() == ProfileTextBox.Text.Trim().ToUpper()))
            {
                MessageBox.Show($"Profile name \"{ProfileTextBox.Text.Trim()}\" already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                res = false;
            }

            if (string.IsNullOrWhiteSpace(ProfileTextBox.Text))
            {
                MessageBox.Show("Profile name cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                res = false;
            }

            if (ProfileTextBox.Text.Contains("*"))
            {
                MessageBox.Show("Profile name cannot contain \"*\".", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                res = false;
            }

            return res;
        }
    }
}
