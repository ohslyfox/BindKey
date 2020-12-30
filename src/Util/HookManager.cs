using Gma.System.MouseKeyHook;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BindKey.Util
{
    internal static class HookManager
    {
        private static IKeyboardMouseEvents _globalHook = null;
        private static IKeyboardMouseEvents GlobalHook
        {
            get
            {
                if (_globalHook == null)
                {
                    _globalHook = Hook.GlobalEvents();
                }
                return _globalHook;
            }
        }

        public static void CleanHook()
        {
            if (_globalHook != null)
            {
                _globalHook.Dispose();
                _globalHook = null;
            }
        }

        public static void SetCombinationHook(Dictionary<Combination, Action> combos)
        {
            try
            {
                GlobalHook.OnCombination(combos);
            }
            catch
            {
                CleanHook();
            }
        }

        public static void SetMouseDown(MouseEventHandler function)
        {
            try
            {
                GlobalHook.MouseDown += function;
            }
            catch
            {
                CleanHook();
            }
        }

        public static void SetMouseUp(MouseEventHandler function)
        {
            try
            {
                GlobalHook.MouseUp += function;
            }
            catch
            {
                CleanHook();
            }
        }

        public static void SetKeyDown(KeyEventHandler function)
        {
            try
            {
                GlobalHook.KeyDown += function;
            }
            catch
            {
                CleanHook();
            }
        }

        public static void SetKeyUp(KeyEventHandler function)
        {
            try
            {
                GlobalHook.KeyUp += function;
            }
            catch
            {
                CleanHook();
            }
        }
    }
}
