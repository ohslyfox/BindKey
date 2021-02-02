using BindKey.AddOptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BindKey.KeyActions
{
    internal class ShowHideProcessAction : DefaultKeyAction
    {
        public enum FocusType
        {
            Focus = 0,
            Minimize = 1,
            Maximize = 2,
            MinMax = 3,
            MinFocus = 4
        }

        private static readonly Dictionary<string, FocusType> DESC_TO_TYPE = new Dictionary<string, FocusType>()
        {
            { FocusType.Focus.GetDescription(), FocusType.Focus },
            { FocusType.Minimize.GetDescription(), FocusType.Minimize },
            { FocusType.Maximize.GetDescription(), FocusType.Maximize },
            { FocusType.MinMax.GetDescription(), FocusType.MinMax },
            { FocusType.MinFocus.GetDescription(), FocusType.MinFocus }
        };

        public override ActionTypes Type => ActionTypes.ShowHideProcess;
        public string ProcessName { get; }
        public FocusType ActionToTake { get; }

        protected override Dictionary<string, string> ItemsToSave
        {
            get
            {
                var res = base.ItemsToSave;
                res[nameof(ProcessName)] = this.ProcessName;
                res[nameof(ActionToTake)] = this.ActionToTake.GetDescription();
                return res;
            }
        }

        [DllImport("user32.dll")]
        private static extern IntPtr SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsIconic(IntPtr hWnd);

        public ShowHideProcessAction(ShowHideProcessOptions options, string GUID = "")
            : base(options, GUID)
        {
            this.ProcessName = options.ProcessName;
            this.ActionToTake = DESC_TO_TYPE[options.ActionToTake];
        }

        public ShowHideProcessAction(Dictionary<string, string> propertyMap)
            : base(propertyMap)
        {
            try
            {
                this.ProcessName = propertyMap[nameof(ProcessName)];
                this.ActionToTake = DESC_TO_TYPE[propertyMap[nameof(ActionToTake)]];
            }
            catch
            {
                MessageBox.Show("Failed to create key action. Corrupted save file or bad input.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void KeyActionProcess()
        {
            try
            {
                Process[] processes = Process.GetProcessesByName(ProcessName);
                foreach (Process process in processes)
                {
                    IntPtr hWnd = process.MainWindowHandle;
                    switch (ActionToTake)
                    {
                        case FocusType.Focus:
                            FocusWindow(hWnd);
                            break;
                        case FocusType.Minimize:
                            MinimizeWindow(hWnd);
                            break;
                        case FocusType.Maximize:
                            MaximizeWindow(hWnd);
                            break;
                        case FocusType.MinMax:
                            HandleMinMaxAction(hWnd);
                            break;
                        case FocusType.MinFocus:
                            HandleMinFocusAction(hWnd);
                            break;
                    }
                }
            }
            catch
            {
                BindKey.ShowBalloonTip("Error", $"Could not {this.ActionToTake.GetDescription()} {this.ProcessName}.", ToolTipIcon.Error);
            }
        }

        private void HandleMinMaxAction(IntPtr hWnd)
        {
            if (IsIconic(hWnd))
            {
                ShowWindow(hWnd, 3);
                SetForegroundWindow(hWnd);
            }
            else
            {
                ShowWindow(hWnd, 6);
            }
        }

        private void HandleMinFocusAction(IntPtr hWnd)
        {
            if (IsIconic(hWnd))
            {
                FocusWindow(hWnd);
            }
            else
            {
                MinimizeWindow(hWnd);
            }
        }

        private void FocusWindow(IntPtr hWnd)
        {
            ShowWindow(hWnd, 1);
            SetForegroundWindow(hWnd);
        }

        private void MaximizeWindow(IntPtr hWnd)
        {
            ShowWindow(hWnd, 3);
            SetForegroundWindow(hWnd);
        }

        private void MinimizeWindow(IntPtr hWnd)
        {
            ShowWindow(hWnd, 6);
        }

        public override string ToString()
        {
            return $"{this.ActionToTake.GetDescription()} {this.ProcessName}" + NextString;
        }
    }

    internal static class FocusTypeExtensions
    {
        public static string GetDescription(this ShowHideProcessAction.FocusType type)
        {
            string res = string.Empty;
            switch (type)
            {
                case ShowHideProcessAction.FocusType.Focus:
                    res = "Focus";
                    break;
                case ShowHideProcessAction.FocusType.Minimize:
                    res = "Minimize";
                    break;
                case ShowHideProcessAction.FocusType.Maximize:
                    res = "Maximize";
                    break;
                case ShowHideProcessAction.FocusType.MinMax:
                    res = "Minimize / Maximize";
                    break;
                case ShowHideProcessAction.FocusType.MinFocus:
                    res = "Minimize / Focus";
                    break;
            }
            return res;
        }
    }
}
