using BindKey.AddOptions;
using System.Collections.Generic;
using System.Diagnostics;

namespace BindKey.KeyActions
{
    internal class OpenProcessAction : DefaultKeyAction
    {
        public string FilePath { get; }
        public bool AsAdmin { get; }
        public override ActionTypes Type { get => ActionTypes.OpenProcess; }

        public OpenProcessAction(OpenProcessOptions options, string GUID = "")
            : base(options, GUID)
        {
            this.FilePath = options.FilePath;
            this.AsAdmin = options.AsAdmin;
        }

        public OpenProcessAction(string[] parts)
            : base(parts)
        {
            this.FilePath = parts[7];
            this.AsAdmin = bool.Parse(parts[8]);
        }

        protected override List<string> SaveOrder
        {
            get
            {
                List<string> res = new List<string>(base.SaveOrder);
                res.AddRange(new List<string>
                {
                    this.FilePath, this.AsAdmin.ToString()
                });
                return res;
            }
        }

        protected override void KeyActionProcess()
        {
            try
            {
                Process process = new Process();
                if (AsAdmin && FilePath.Split('.').Length > 1)
                {
                    process.StartInfo.UseShellExecute = true;
                    process.StartInfo.Verb = "runas";
                }
                process.StartInfo.FileName = FilePath;
                process.Start();
            }
            catch
            {
                BindKey.GetInstance().ShowBalloonTip("Error", $"BindKey was unable to start process \"{this.FilePath}\"");
            }
        }

        public override string ToString()
        {
            return $"Open {this.FilePath}" +
                   $"{(this.AsAdmin ? " as administrator" : string.Empty)}" + NextString;
        }
    }
}
