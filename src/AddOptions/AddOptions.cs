using BindKey.KeyActions;
using System.Linq;
using System.Windows.Forms;

namespace BindKey.AddOptions
{
    internal interface IAddOptions
    {
        public ActionTypes Type { get; }
        public Keys[] Keys { get; }
        public bool Enabled { get; }
    }

    internal abstract class DefaultAddOptions : IAddOptions
    {
        public const string CONTROL_ENABLED = "CheckBoxEnabled";

        public abstract ActionTypes Type { get; }

        public bool Enabled { get => (GetControl(CONTROL_ENABLED) as CheckBox).Checked; }
        public Keys[] Keys { get => this.AddForm.Keys; }
        public IKeyAction NextAction { get => this.AddForm.NextAction; }

        protected Add AddForm { get; }

        protected Control GetControl(string name)
        {
            return this.AddForm.Controls.Find(name, true).First();
        }

        public DefaultAddOptions(Add addForm)
        {
            this.AddForm = addForm;
        }
    }
}
