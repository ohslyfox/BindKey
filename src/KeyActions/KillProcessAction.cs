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
                    if (process.HasExited)
                    {
                        instancesKilled++;
                    }
                    /*if (process.HasExited == false)
                    {
                        process.Kill();
                        process.WaitForExit(1000);
                        
                        if (process.HasExited == false)
                        {
                            KillChildren(process.Id);
                        }

                        if (process.HasExited == false)
                        {
                            process.Kill();
                            process.WaitForExit(500);
                        }

                        if (process.HasExited)
                        {
                            instancesKilled++;
                        }
                    }*/
                }

                AddMessage("Kill Process Action", $"Killed {instancesKilled} of {processes.Length} instances of {ProcessName}.", ToolTipIcon.Info);
            }
            catch
            {
                AddMessage("Error", "Could not kill or start process.", ToolTipIcon.Error);
            }
        }

        /*private void KillChildren(int pID)
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher($"SELECT * FROM WIN32_PROCESS WHERE PARENTPROCESSID={pID}");
                ManagementObjectCollection processes = searcher.Get();
                foreach (var mo in processes)
                {
                    var proc = Process.GetProcessById(Convert.ToInt32(mo["ProcessID"]));
                    if (proc != null && proc.HasExited == false)
                    {
                        proc.Kill();
                    }
                }
            }
            catch
            {
                BindKey.ShowBalloonTip("Error", "Could not kill or start process.", ToolTipIcon.Error);
            }
        }*/

        public override string ToString()
        {
            return $"Kill {this.ProcessName}" + NextString;
        }
    }
}
