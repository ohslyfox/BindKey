using BindKey.AddOptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management;
using System.Windows.Forms;

namespace BindKey.KeyActions
{
    internal class KillProcessAction : DefaultKeyAction
    {
        public override ActionTypes Type { get => ActionTypes.KillProcess; }
        public string ProcessName { get; }

        public KillProcessAction(KillProcessOptions options, string GUID = "")
            : base(options, GUID)
        {
            this.ProcessName = options.ProcessName;
        }

        public KillProcessAction(Dictionary<string, string> propertyMap)
            : base(propertyMap)
        {
            try
            {
                this.ProcessName = propertyMap[nameof(ProcessName)];
            }
            catch
            {
                MessageBox.Show("Failed to create key action. Corrupted save file or bad input.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public override Dictionary<string, string> Properties
        {
            get
            {
                var res = base.Properties;
                res[nameof(ProcessName)] = ProcessName;
                return res;
            }
        }

        protected override void KeyActionProcess()
        {
            try
            {
                Process[] processes = Process.GetProcessesByName(ProcessName);
                int instancesKilled = 0;

                foreach (Process process in processes)
                {
                    process.Kill();
                    process.WaitForExit(1000);
                    if (process.HasExited)
                    {
                        instancesKilled++;
                    }
                }

                AddMessage("Kill Process Action", $"Killed {instancesKilled} of {processes.Length} instances of {ProcessName}.", ToolTipIcon.Info);
            }
            catch
            {
                AddMessage("Error", $"Failed to kill process: {ProcessName}.", ToolTipIcon.Error);
            }
        }

        public override string ToString()
        {
            return $"Kill {this.ProcessName}" + NextString;
        }
    }
}
