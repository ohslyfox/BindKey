using BindKey.AddOptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace BindKey.KeyActions
{
    internal class KillStartProcessAction : DefaultKeyAction
    {
        public string ProcessName { get; }
        public bool Restart { get; }
        public string FilePath { get; }
        public bool AsAdmin { get; }
        public bool MatchName { get; }
        public override ActionTypes Type { get => ActionTypes.KillStartProcess; }

        protected override List<string> SaveOrder
        {
            get
            {
                List<string> res = new List<string>(base.SaveOrder);
                res.AddRange(new List<string>
                {
                    this.ProcessName, this.Restart.ToString(), AsAdmin.ToString(),
                    this.MatchName.ToString(), this.FilePath
                });
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

                if (processes.Length > 0 && Restart)
                {
                    Process p = new Process();
                    if (AsAdmin && (MatchName == false || FilePath.Split('.').Length > 1))
                    {
                        p.StartInfo.UseShellExecute = true;
                        p.StartInfo.Verb = "runas";
                    }

                    p.StartInfo.FileName = MatchName ? filePath : FilePath;
                    p.Start();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error: could not kill or start process.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public KillStartProcessAction(KillStartProcessOptions options, string GUID = "")
            : base(options, GUID)
        {
            this.ProcessName = options.ProcessName;
            this.Restart = options.Restart;
            this.AsAdmin = options.AsAdmin;
            this.MatchName = options.MatchName;
            this.FilePath = options.FilePath;
        }

        public KillStartProcessAction(string[] parts)
            : base(parts)
        {
            this.ProcessName = parts[7];
            this.Restart = bool.Parse(parts[8]);
            this.AsAdmin = bool.Parse(parts[9]);
            this.MatchName = bool.Parse(parts[10]);
            this.FilePath = parts[11];
        }

        public override string ToString()
        {
            return $"Kill {this.ProcessName} " +
                   $"{(this.Restart ? "and " + (this.MatchName ? "restart " : "start " + this.FilePath) + (this.AsAdmin ? " as administrator " : string.Empty) : string.Empty)}" +
                   $"{(this.NextKeyAction != null ? $" -> {this.NextKeyAction.ToString()}": string.Empty)}";
        }
    }
}
