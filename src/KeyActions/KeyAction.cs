using BindKey.AddOptions;
using BindKey.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BindKey.KeyActions
{
    internal delegate void KeyAction();

    internal interface IKeyAction
    {
        ActionTypes Type { get; }
        Keys[] Keys { get; }
        string KeyCombo { get; }
        Dictionary<string, string> Properties { get; }
        string GUID { get; }
        string NextKeyActionGUID { get; }
        bool Enabled { get; set; }
        bool Pinned { get; set; }
        bool Notify { get; }
        KeyAction Action { get; }
        IKeyAction NextKeyAction { get; }
        void SetNextAction(IKeyAction action);
        void ExecuteActions();
        void ClearKeyCombo();
    }

    internal abstract class DefaultKeyAction : IKeyAction
    {
        public const int KEY_COUNT = 3;

        public virtual Dictionary<string, string> Properties
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    { nameof(Type), Type.ToString() },
                    { nameof(GUID), GUID },
                    { nameof(NextKeyActionGUID), NextKeyActionGUID },
                    { nameof(Enabled), Enabled.ToString() },
                    { nameof(Pinned), Pinned.ToString() },
                    { nameof(Keys), string.Join(",", Keys) },
                    { nameof(Notify), Notify.ToString() }
                };
            }
        }

        public abstract ActionTypes Type { get; }
        protected abstract void KeyActionProcess();
        public override abstract string ToString();
        protected string NextString { get => $"{(this.NextKeyAction != null ? $" -> {this.NextKeyAction}" : string.Empty)}"; }

        private KeyAction _action = null;
        public KeyAction Action
        {
            get
            {
                if (_action == null)
                {
                    _action = new KeyAction(KeyActionProcess);
                }
                return _action;
            }
        }

        public IKeyAction NextKeyAction { get; private set; }
        public bool Enabled { get; set; }
        public bool Pinned { get; set; }
        public bool Notify { get; }
        public Keys[] Keys { get; private set; }
        public string KeyCombo { get => GetKeyCombo(this.Keys, false); }
        public string GUID { get; }
        public string NextKeyActionGUID { get; private set; }

        private DefaultKeyAction()
        {
            this.GUID = Guid.NewGuid().ToString();
            ClearKeyCombo();
        }

        public DefaultKeyAction(DefaultAddOptions options, string GUID = "")
            : this()
        {
            if (string.IsNullOrWhiteSpace(GUID) == false)
            {
                this.GUID = GUID;
            }
            this.Keys = options.Keys;
            this.Enabled = options.Enabled;
            this.Pinned = options.Pinned;
            this.Notify = options.Notify;
            SetNextAction(options.NextAction);
        }

        public DefaultKeyAction(Dictionary<string, string> propertyMap)
            : this()
        {
            this.GUID = propertyMap[nameof(GUID)];
            this.NextKeyActionGUID = propertyMap[nameof(NextKeyActionGUID)];
            this.Enabled = bool.Parse(propertyMap[nameof(Enabled)]);
            this.Pinned = bool.Parse(propertyMap[nameof(Pinned)]);
            this.Notify = bool.Parse(propertyMap[nameof(Notify)]);

            var keys = propertyMap[nameof(Keys)].Split(',');
            this.Keys[0] = (Keys)Enum.Parse(typeof(Keys), keys[0], true);
            this.Keys[1] = (Keys)Enum.Parse(typeof(Keys), keys[1], true);
            this.Keys[2] = (Keys)Enum.Parse(typeof(Keys), keys[2], true);
        }

        protected void AddMessage(string title, string message, ToolTipIcon icon)
        {
            if (this.Notify)
            {
                BindKey.AddMessage(title, message, icon);
            }
        }

        public static string GetKeyCombo(Keys[] keys, bool pretty)
        {
            string res = string.Empty;
            int count = 0;
            foreach (Keys key in keys.Where(k => k != System.Windows.Forms.Keys.None))
            {
                if (count != 0) res += "+";
                res += pretty ? PrettyKeys.Convert(key) : key.ToString();
                count++;
            }
            return res;
        }

        public void SetNextAction(IKeyAction action)
        {
            if (action != null)
            {
                this.NextKeyAction = action;
                this.NextKeyActionGUID = action.GUID;
            }
            else
            {
                this.NextKeyAction = null;
                this.NextKeyActionGUID = null;
            }
        }

        public void ExecuteActions()
        {
            Action?.Invoke();
            var tempNextAction = NextKeyAction;
            while (tempNextAction != null && tempNextAction.Action != Action)
            {
                tempNextAction.Action?.Invoke();
                tempNextAction = tempNextAction.NextKeyAction;
            }
            BindKey.NotifyAllMessages();
        }

        public void ClearKeyCombo()
        {
            this.Keys = new Keys[KEY_COUNT];
        }
    }
}