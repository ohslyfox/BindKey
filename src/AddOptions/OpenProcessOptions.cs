﻿using BindKey.KeyActions;
using System;
using System.Windows.Forms;

namespace BindKey.AddOptions
{
    internal class OpenProcessOptions : DefaultAddOptions
    {
        public const string CONTROL_FILEPATH = "OpenFilePathTextBox";
        public const string CONTROL_ASADMIN = "checkBox1";

        public override ActionTypes Type { get => ActionTypes.OpenProcess; }
        public string FilePath { get => (GetControl(CONTROL_FILEPATH) as TextBox).Text; }
        public bool AsAdmin { get => (GetControl(CONTROL_ASADMIN) as CheckBox).Checked; }

        public OpenProcessOptions(Add addForm)
            : base(addForm)
        { }

        public override void FillForm(IKeyAction action)
        {
            var convertedAction = action as OpenProcessAction ?? throw new ArgumentException();
            base.FillForm(convertedAction);
            SetControl<TextBox>(CONTROL_FILEPATH, convertedAction.FilePath);
            SetControl<CheckBox>(CONTROL_ASADMIN, convertedAction.AsAdmin);
        }

        public override bool Validate()
        {
            bool res = base.Validate();
            if (res)
            {
                if (string.IsNullOrWhiteSpace(FilePath))
                {
                    MessageBox.Show("Error: a process or folder name must be set.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return res;
        }
    }
}
