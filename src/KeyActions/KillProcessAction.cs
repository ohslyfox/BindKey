using BindKey.AddOptions;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace BindKey.KeyActions
{
    internal class KillProcessAction : DefaultKeyAction
    {
        public string ProcessName { get; }
        public override ActionTypes Type { get => ActionTypes.KillProcess; }

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

        protected override Dictionary<string, string> ItemsToSave
        {
            get
            {
                var res = base.ItemsToSave;
                res[nameof(ProcessName)] = ProcessName;
                return res;
            }
        }

        protected override void KeyActionProcess()
        {
            try
            {
                Process[] processes = Process.GetProcessesByName(ProcessName);
                string filePath = string.Empty;
                foreach (Process process in processes)
                {
                    if (filePath == string.Empty && process.MainModule != null)
                    {
                        filePath = process.MainModule.FileName;
                    }
                    process.Kill();
                }
            }
            catch
            {
                BindKey.GetInstance().ShowBalloonTip("Error", "Could not kill or start process.");
            }
        }

        public override string ToString()
        {
            return $"Kill {this.ProcessName}" + NextString;
        }
    }
}
