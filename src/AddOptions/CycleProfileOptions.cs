using BindKey.KeyActions;
using System;
using System.Windows.Forms;

namespace BindKey.AddOptions
{
    internal class CycleProfileOptions : DefaultAddOptions
    {
        public const string CONTROL_FORWARD = "RadioCycleForward";
        public const string CONTROL_BACKWARD = "RadioCycleBackward";

        public override ActionTypes Type => ActionTypes.CycleProfile;
        public bool IsForward { get => (GetControl(CONTROL_FORWARD) as RadioButton).Checked; }
        public bool IsBackward { get => (GetControl(CONTROL_BACKWARD) as RadioButton).Checked; }

        public CycleProfileOptions(Add addForm)
            : base(addForm)
        { }

        public override void FillForm(IKeyAction action)
        {
            var convertedAction = action as CycleProfileAction ?? throw new ArgumentException();
            base.FillForm(convertedAction);
            SetControl<RadioButton>(CONTROL_FORWARD, convertedAction.IsForward);
            SetControl<RadioButton>(CONTROL_BACKWARD, convertedAction.IsForward == false);
        }

        public override bool Validate()
        {
            bool res = base.Validate();
            if (IsForward == false && IsBackward == false)
            {
                MessageBox.Show("Error: forward or backward option must be selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                res = false;
            }
            return res;
        }
    }
}
