using BindKey.KeyActions;
using System.Linq;
using System.Windows.Forms;

namespace BindKey.AddOptions
{
    internal class KillStartProcessOptions : DefaultAddOptions
    {
        public const string CONTROL_PROCESSNAME = "KillRestartProcessNameTextBox";
        public const string CONTROL_FILEPATH = "textBox3";
        public const string CONTROL_RESTART = "KillRestartRestartCheckBox";
        public const string CONTROL_ASADMIN = "KillRestartAdminCheckbox";
        public const string CONTROL_MATCHNAME = "checkBox2";

        public override ActionTypes Type { get => ActionTypes.KillStartProcess; }
        public string ProcessName { get => (AddForm.Controls.Find(CONTROL_PROCESSNAME, true).First() as TextBox).Text; }
        public string FilePath { get => (AddForm.Controls.Find(CONTROL_FILEPATH, true).First() as TextBox).Text; }
        public bool MatchName { get => (AddForm.Controls.Find(CONTROL_MATCHNAME, true).First() as CheckBox).Checked; }
        public bool Restart { get => (AddForm.Controls.Find(CONTROL_RESTART, true).First() as CheckBox).Checked; }
        public bool AsAdmin { get => (AddForm.Controls.Find(CONTROL_ASADMIN, true).First() as CheckBox).Checked; }

        public KillStartProcessOptions(Add addForm)
            : base(addForm)
        { }
    }
}
