using BindKey.AddOptions;
using System;
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
        string NextActionGUID { get; }
        bool Enabled { get; }
        KeyAction Action { get; }
        IKeyAction NextAction { get; }
        void SetNextAction(IKeyAction action);
        void ExecuteActions();
    }

    internal abstract class DefaultKeyAction : IKeyAction
    {
        public static System.Text.RegularExpressions.Regex DELIMITER_REGEX = new System.Text.RegularExpressions.Regex("\\|\\|\\|\\|\\|");
        public const string DELIMITER = "|||||"; 
        public const int KEY_COUNT = 3;

        public abstract ActionTypes Type { get; }
        public abstract string SaveString { get; }
        protected abstract void KeyActionProcess();
        public KeyAction Action { get => new KeyAction(KeyActionProcess); }
        public IKeyAction NextAction { get; private set; } = null;
        

        public bool Enabled { get; private set; }
        public Keys[] Keys { get; private set; } = new Keys[KEY_COUNT];
        public enum KeyPartEnum
        {
            First = 0,
            Second = 1,
            Third = 2
        }

        public string KeyCombo { get => GetKeyCombo(this.Keys, false); }

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

        public string GUID { get; } = Guid.NewGuid().ToString();
        public string NextActionGUID { get; private set; } = null;

        public void SetNextAction(IKeyAction action)
        {
            if (action != null)
            {
                this.NextAction = action;
                this.NextActionGUID = action.GUID;
            }
            else
            {
                this.NextAction = null;
                this.NextActionGUID = null;
            }
        }

        public void ExecuteActions()
        {
            Action?.Invoke();
            var tempNextAction = NextAction;
            while (tempNextAction != null && tempNextAction.Action != Action)
            {
                tempNextAction.Action?.Invoke();
                tempNextAction = tempNextAction.NextAction;
            }
        }

        public DefaultKeyAction(DefaultAddOptions options, string GUID = "")
            : base()
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
            this.NextActionGUID = parts[2];
            this.Keys = new Keys[3];
            this.Keys[0] = (Keys)Enum.Parse(typeof(Keys), parts[3], true);
            this.Keys[1] = (Keys)Enum.Parse(typeof(Keys), parts[4], true);
            this.Keys[2] = (Keys)Enum.Parse(typeof(Keys), parts[5], true);
            this.Enabled = bool.Parse(parts[6]);
        }
    }
}