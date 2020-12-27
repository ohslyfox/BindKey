using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BindKey.Util
{
    internal static class PrettyKeys
    {
        public static readonly Dictionary<Keys, string> PrettyDictionary = new Dictionary<Keys, string>
        {
            { Keys.OemCloseBrackets, "RightBracket" },
            { Keys.OemOpenBrackets, "LeftBracket" },
            { Keys.OemQuestion, "ForwardSlash" },
            { Keys.OemSemicolon, "Semicolon" },
            { Keys.LControlKey, "Control" },
            { Keys.RControlKey, "Control" },
            { Keys.Oemtilde, "Tilde" },
            { Keys.Oem5, "BackSlash" },
            { Keys.LShiftKey, "Shift" },
            { Keys.RShiftKey, "Shift" },
            { Keys.LWin, "Windows" },
            { Keys.RWin, "Windows" },
            { Keys.LMenu, "Alt" },
            { Keys.RMenu, "Alt" },
            { Keys.Oemcomma, "Comma" },
            { Keys.OemPeriod, "Period" },
            { Keys.Oem7, "Quote" },
            { Keys.OemMinus, "Minus" },
            { Keys.Oemplus, "Plus" },
            { Keys.Return, "Enter" },
            { Keys.D0, "0" },
            { Keys.D1, "1" },
            { Keys.D2, "2" },
            { Keys.D3, "3" },
            { Keys.D4, "4" },
            { Keys.D5, "5" },
            { Keys.D6, "6" },
            { Keys.D7, "7" },
            { Keys.D8, "8" },
            { Keys.D9, "9" },
        };

        public static Keys Convert(string key)
        {
            var outVal = PrettyDictionary.Where(kvp => kvp.Value == key).FirstOrDefault();
            return outVal.Value == null ? Keys.None : outVal.Key;
        }

        public static string Convert(Keys key)
        {
            string res = key.ToString();
            if (PrettyDictionary.TryGetValue(key, out string temp)) {
                res = temp;
            }
            return res;
        }
    }
}
