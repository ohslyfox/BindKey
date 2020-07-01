using BindKey.AddOptions;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace BindKey.KeyActions
{
    internal class OpenProcessAction : DefaultKeyAction
    {
        public string FilePath { get; }
        public bool AsAdmin { get; }

        public override ActionTypes Type { get => ActionTypes.OpenProcess; }
        public override string SaveString
        {
            get
            {
                return ($"{Type.ToString()}{DELIMITER}{GUID}{DELIMITER}{NextActionGUID}{DELIMITER}{Keys[0].ToString()}{DELIMITER}{Keys[1].ToString()}" +
                        $"{DELIMITER}{Keys[2].ToString()}{DELIMITER}{Enabled.ToString()}{DELIMITER}{FilePath}{DELIMITER}{AsAdmin.ToString()}");
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
            catch (Exception)
            {
                MessageBox.Show("Error: could not start process.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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

        public override string ToString()
        {
            return $"Open {this.FilePath}" +
                   $"{(this.AsAdmin ? " as administrator" : string.Empty)}" +
                   $"{(this.NextAction != null ? $" -> {this.NextAction.ToString()}" : string.Empty)}";
        }
    }
}
