using BindKey.KeyActions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace BindKey.AddOptions
{
    internal interface IAddOptions
    {
        public ActionTypes Type { get; }
        public Keys[] Keys { get; }
        public bool Enabled { get; }
        public void FillForm(IKeyAction action);
        public bool Validate();
    }

    internal abstract class DefaultAddOptions : IAddOptions
    {
        public const string CONTROL_ENABLED = "CheckBoxEnabled";

        public abstract ActionTypes Type { get; }
        
        public bool Enabled { get => (GetControl(CONTROL_ENABLED) as CheckBox).Checked; }
        public Keys[] Keys { get => this.AddForm.Keys; }
        public IKeyAction NextAction { get => this.AddForm.NextAction; }

        protected Add AddForm { get; }

        private Dictionary<Type, Action<string, object>> _setActions = null;
        private Dictionary<Type, Action<string, object>> SetActions
        {
            get
            {
                if (_setActions == null)
                {
                    _setActions = InitSetActions();
                }
                return _setActions;
            }
        }

        public DefaultAddOptions(Add addForm)
        {
            this.AddForm = addForm;
        }

        public virtual bool Validate()
        {
            if (Keys.Where(k => k != System.Windows.Forms.Keys.None).Distinct().Count() != Keys.Where(k => k != System.Windows.Forms.Keys.None).Count())
            {
                MessageBox.Show("Error: duplicate keys in key combination.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (this.Enabled &&
                AddForm.KeyActions.Any(ka => ka.Enabled &&
                                     ka.Equals(AddForm.LocalAction) == false &&
                                     string.IsNullOrWhiteSpace(ka.KeyCombo) == false &&
                                     ka.KeyCombo == DefaultKeyAction.GetKeyCombo(Keys, false)))
            {
                MessageBox.Show("Error: an event is already bound to the key combo " + DefaultKeyAction.GetKeyCombo(Keys, true) + ".", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        
        public void FillForm(IKeyAction action)
        {
            var keys = action.Keys.Clone() as Keys[];
            this.AddForm.Keys[0] = keys[0];
            this.AddForm.Keys[1] = keys[1];
            this.AddForm.Keys[2] = keys[2];
            SetControl<CheckBox>(CONTROL_ENABLED, action.Enabled);
            SetControl<RadioButton>(action.Type.ToString(), true);
        }

        protected Control GetControl(string name)
        {
            return this.AddForm.Controls.Find(name, true).First();
        }

        protected void SetControl<T>(string control, object value)
        {
            this.SetActions[typeof(T)]?.Invoke(control, value);
        }

        private Dictionary<Type, Action<string, object>> InitSetActions()
        {
            return new Dictionary<Type, Action<string, object>>
            {
                { typeof(TextBox), (x, y) => { (GetControl(x) as TextBox).Text = (string)y; } },
                { typeof(CheckBox), (x, y) => { (GetControl(x) as CheckBox).Checked = (bool)y; } },
                { typeof(PictureBox), (x, y) => { (GetControl(x) as PictureBox).Image = (Bitmap)y; } },
                { typeof(RadioButton), (x, y) => { (GetControl(x) as RadioButton).Checked = (bool)y; } }
            };
        }
    }
}
