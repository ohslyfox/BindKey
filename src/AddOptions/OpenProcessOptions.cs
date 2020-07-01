using BindKey.KeyActions;
using System.Linq;
using System.Windows.Forms;

namespace BindKey.AddOptions
{
    internal class OpenProcessOptions : DefaultAddOptions
    {
        public const string CONTROL_FILEPATH = "textBox1";
        public const string CONTROL_ASADMIN = "checkBox1";

        public override ActionTypes Type { get => ActionTypes.OpenProcess; }
        public string FilePath { get => (AddForm.Controls.Find(CONTROL_FILEPATH, true).First() as TextBox).Text; }
        public bool AsAdmin { get => (AddForm.Controls.Find(CONTROL_ASADMIN, true).First() as CheckBox).Checked; }

        public OpenProcessOptions(Add addForm)
            : base(addForm)
        { }
    }
}
