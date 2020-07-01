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

        public bool Enabled { get => (AddForm.Controls.Find(CONTROL_ENABLED, true).First() as CheckBox).Checked; }
        public Keys[] Keys { get => this.AddForm.Keys; }
        public IKeyAction NextAction { get => this.AddForm.NextAction; }

        protected Add AddForm { get; }
        public DefaultAddOptions(Add addForm)
        {
            this.AddForm = addForm;
        }
    }
}
