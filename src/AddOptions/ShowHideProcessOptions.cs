using BindKey.KeyActions;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BindKey.AddOptions
{
    internal class ShowHideProcessOptions : DefaultAddOptions
    {
        public const string CONTROL_PROCESSNAME = "FocusProcessNameTextBox";
        public const string CONTROL_ACTIONTOTAKE = "FocusProcessActionComboBox";

        public override ActionTypes Type => ActionTypes.ShowHideProcess;
        public string ProcessName { get => (GetControl(CONTROL_PROCESSNAME) as TextBox).Text; }
        public string ActionToTake { get => (GetControl(CONTROL_ACTIONTOTAKE) as ComboBox).Text; }

        public ShowHideProcessOptions(Add addForm)
            : base(addForm)
        { }

        public override void FillForm(IKeyAction action)
        {
            var convertedAction = action as ShowHideProcessAction ?? throw new ArgumentException();
            base.FillForm(convertedAction);
            SetControl<TextBox>(CONTROL_PROCESSNAME, convertedAction.ProcessName);
            SetControl<ComboBox>(CONTROL_ACTIONTOTAKE, (int)convertedAction.ActionToTake);
        }

        public override bool Validate()
        {
            bool res = base.Validate();
            if (res)
            {
                if (string.IsNullOrWhiteSpace(ProcessName))
                {
                    MessageBox.Show("Error: a process name must be set.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (string.IsNullOrWhiteSpace(ActionToTake))
                {
                    MessageBox.Show("Error: an action type must be selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return res;
        }
    }
}
