using BindKey.KeyActions;
using System;
using System.Windows.Forms;

namespace BindKey.AddOptions
{
    internal class KillProcessOptions : DefaultAddOptions
    {
        public const string CONTROL_PROCESSNAME = "KillRestartProcessNameTextBox";

        public override ActionTypes Type { get => ActionTypes.KillProcess; }
        public string ProcessName { get => (GetControl(CONTROL_PROCESSNAME) as TextBox).Text; }

        public KillProcessOptions(Add addForm)
            : base(addForm)
        { }

        public override void FillForm(IKeyAction action)
        {
            var convertedAction = action as KillProcessAction ?? throw new ArgumentException();
            base.FillForm(convertedAction);
            SetControl<TextBox>(CONTROL_PROCESSNAME, convertedAction.ProcessName);
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
            }
            return res;
        }
    }
}
