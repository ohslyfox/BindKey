using BindKey.KeyActions;
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

        public void FillForm(CycleProfileAction action)
        {
            base.FillForm(action);
            SetControl<RadioButton>(CONTROL_FORWARD, action.IsForward);
            SetControl<RadioButton>(CONTROL_BACKWARD, action.IsForward == false);
        }
    }
}
