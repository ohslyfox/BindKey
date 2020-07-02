using BindKey.AddOptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BindKey.KeyActions
{
    public enum ActionTypes
    {
        None = 0,
        OpenProcess = 1,
        ScreenCapture = 2,
        KillStartProcess = 3
    }

    public delegate void KeyAction();

    internal interface IKeyAction
    {
        ActionTypes Type { get; }
        Keys[] Keys { get; }
        string KeyCombo { get; }
        string SaveString { get; }
        string GUID { get; }
        string NextKeyActionGUID { get; }
        bool Enabled { get; }
        KeyAction Action { get; }
        IKeyAction NextKeyAction { get; }
        void SetNextAction(IKeyAction action);
        void ExecuteActions();
    }

    internal abstract class DefaultKeyAction : IKeyAction
    {
        public const string DELIMITER = "|||||";
        public const int KEY_COUNT = 3;
        public static System.Text.RegularExpressions.Regex DELIMITER_REGEX = new System.Text.RegularExpressions.Regex("\\|\\|\\|\\|\\|");

        protected virtual List<string> SaveOrder { get => new List<string> { this.Type.ToString(), this.GUID, this.NextKeyActionGUID,
                                                                             this.Keys[0].ToString(), this.Keys[1].ToString(), this.Keys[2].ToString(),
                                                                             this.Enabled.ToString() }; }

        public abstract ActionTypes Type { get; }
        protected abstract void KeyActionProcess();
        
        public KeyAction Action { get => new KeyAction(KeyActionProcess); }
        public IKeyAction NextKeyAction { get; private set; }
        public bool Enabled { get; private set; }
        public Keys[] Keys { get; private set; } = new Keys[KEY_COUNT];
        public string SaveString { get => string.Join(DELIMITER, this.SaveOrder.ToArray()); }
        public string KeyCombo { get => GetKeyCombo(this.Keys, false); }
        public string GUID { get; } = Guid.NewGuid().ToString();
        public string NextKeyActionGUID { get; private set; }

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
        }

        public DefaultKeyAction(DefaultAddOptions options, string GUID = "")
        {
            if (string.IsNullOrWhiteSpace(GUID) == false)
            {
                this.GUID = GUID;
            }
            this.Keys = options.Keys;
            this.Enabled = options.Enabled;
            SetNextAction(options.NextAction);
        }

        public DefaultKeyAction(string[] parts)
        {
            this.GUID = parts[1];
            this.NextKeyActionGUID = parts[2];
            this.Keys = new Keys[3];
            this.Keys[0] = (Keys)Enum.Parse(typeof(Keys), parts[3], true);
            this.Keys[1] = (Keys)Enum.Parse(typeof(Keys), parts[4], true);
            this.Keys[2] = (Keys)Enum.Parse(typeof(Keys), parts[5], true);
            this.Enabled = bool.Parse(parts[6]);
        }
    }
}