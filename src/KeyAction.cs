using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace BindKey
{
    public enum ActionTypes
    {
        None = 0,
        OpenProcess = 1,
        ScreenCapture = 2,
        KillStartProcess = 3
    }

    internal interface IKeyAction
    {
        ActionTypes Type { get; }
        Keys[] Keys { get; }
        string KeyCombo { get; }
        string SaveString { get; }
        bool Enabled { get; set; }
        List<IKeyAction> ChainActions { get; }
        void ExecuteAction();
    }

    internal abstract class DefaultKeyAction : IKeyAction
    {
        public const int KEY_COUNT = 3;

        public abstract ActionTypes Type { get; }
        public abstract string SaveString { get; }
        public abstract List<IKeyAction> ChainActions { get; }
        public abstract void ExecuteAction();
        
        public bool Enabled { get; set; }
        public Keys[] Keys { get; set; } = new Keys[KEY_COUNT];
        public enum KeyPartEnum
        {
            First = 0,
            Second = 1,
            Third = 2
        }

        public string KeyCombo { get => GetKeyCombo(this.Keys, false); }

        public static string GetKeyCombo(Keys[] keys, bool pretty)
        {
            string res = "";
            int count = 0;
            foreach (Keys key in keys.Where(k => k != System.Windows.Forms.Keys.None))
            {
                if (count != 0) res += "+";
                res += pretty ? PrettyKeys.Convert(key) : key.ToString();
                count++;
            }
            return res;
        }

        protected virtual void ExecuteChainActions()
        {
            foreach (var action in ChainActions)
            {
                action.ExecuteAction();
            }
        }
    }

    internal class OpenProcessAction : DefaultKeyAction
    {
        public string FilePath { get; set; }
        public bool AsAdmin { get; set; }

        public override ActionTypes Type { get => ActionTypes.OpenProcess; }
        public override string SaveString
        {
            get
            {
                return ($"{Type.ToString()}|||||{Keys[0].ToString()}|||||{Keys[1].ToString()}" +
                        $"|||||{Keys[2].ToString()}|||||{FilePath}|||||{AsAdmin.ToString()}|||||{Enabled.ToString()}");
            }
        }

        public override List<IKeyAction> ChainActions => throw new NotImplementedException();

        public override void ExecuteAction()
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
            catch (Exception e)
            {
                MessageBox.Show("Error: could not start process.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //ExecuteChainActions();
        }
    }

    internal class ScreenCaptureAction : DefaultKeyAction
    {
        public Rectangle ScreenRegion { get; set; }
        public string FolderPath { get; set; }

        public override ActionTypes Type { get => ActionTypes.ScreenCapture; }
        public override string SaveString
        {
            get
            {
                return ($"{Type.ToString()}|||||{Keys[0].ToString()}|||||{Keys[1].ToString()}" +
                        $"|||||{Keys[2].ToString()}|||||{ScreenRegion.X}|||||{ScreenRegion.Y}" +
                        $"|||||{ScreenRegion.Width}|||||{ScreenRegion.Height}|||||{FolderPath}|||||{Enabled.ToString()}");
            }
        }

        public override List<IKeyAction> ChainActions => throw new NotImplementedException();

        public override void ExecuteAction()
        {
            try
            {
                Bitmap bmp = new Bitmap(ScreenRegion.Width, ScreenRegion.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                Graphics g = Graphics.FromImage(bmp);
                g.CopyFromScreen(ScreenRegion.X, ScreenRegion.Y, 0, 0, bmp.Size);

                Random rand = new Random();
                string name = string.Empty;
                do
                {
                    name = string.Empty;
                    for (int i = 0; i < 10; i++)
                    {
                        switch (rand.Next(0, 3))
                        {
                            case 0:
                                name += (char)rand.Next(65, 90);
                                break;
                            case 1:
                                name += (char)rand.Next(97, 123);
                                break;
                            case 2:
                                name += (char)rand.Next(48, 58);
                                break;
                        }

                    }
                } while (File.Exists(Path.Combine(FolderPath, name) + ".png"));
                
                bmp.Save(Path.Combine(FolderPath, name) + ".png");
                Clipboard.SetImage(bmp);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: could not capture screen region, check folder path.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //ExecuteChainActions();
        }
    }

    internal class KillStartProcessAction : DefaultKeyAction
    {
        public string ProcessName { get; set; }
        public bool Restart { get; set; }
        public string FilePath { get; set; }
        public bool AsAdmin { get; set; }
        public bool MatchName { get; set; }

        public override ActionTypes Type { get => ActionTypes.KillStartProcess; }
        public override string SaveString
        {
            get
            {
                return $"{Type.ToString()}|||||{Keys[0].ToString()}|||||{Keys[1].ToString()}|||||" +
                       $"{Keys[2].ToString()}|||||{Enabled.ToString()}|||||{ProcessName}|||||{Restart.ToString()}|||||" +
                       $"{AsAdmin.ToString()}|||||{MatchName.ToString()}|||||{FilePath}";
            }
        }

        public override List<IKeyAction> ChainActions => throw new NotImplementedException();

        public override void ExecuteAction()
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
            catch (Exception e)
            {
                MessageBox.Show("Error: could not kill or start process.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //ExecuteChainActions();
        }
    }
}