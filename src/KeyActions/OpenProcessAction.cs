using BindKey.AddOptions;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

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

        public OpenProcessAction(Dictionary<string, string> propertyMap)
            : base(propertyMap)
        {
            try
            {
                this.FilePath = propertyMap[nameof(FilePath)];
                this.AsAdmin = bool.Parse(propertyMap[nameof(AsAdmin)]);
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
                res[nameof(FilePath)] = FilePath;
                res[nameof(AsAdmin)] = AsAdmin.ToString();
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
                BindKey.ShowBalloonTip("Error", $"BindKey was unable to start process \"{this.FilePath}\"", ToolTipIcon.Error);
            }
        }

        public override string ToString()
        {
            return $"Open {this.FilePath}" +
                   $"{(this.AsAdmin ? " as administrator" : string.Empty)}" + NextString;
        }
    }
}
