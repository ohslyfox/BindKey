using BindKey.KeyActions;
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
    }
}
