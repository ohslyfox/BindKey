using BindKey.KeyActions;
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
    }
}
