using BindKey.AddOptions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace BindKey.KeyActions
{
    internal class ScreenCaptureAction : DefaultKeyAction
    {
        public Rectangle ScreenRegion { get; }
        public string FolderPath { get; }
        public override ActionTypes Type { get => ActionTypes.ScreenCapture; }

        public ScreenCaptureAction(ScreenCaptureOptions options, string GUID = "")
            : base(options, GUID)
        {
            if (options.ScreenRegion == null)
            {
                Console.WriteLine("DEV ERROR: screen capture action attempted to be created with no valid screen region");
                Environment.Exit(1);
            }

            this.ScreenRegion = (Rectangle)options.ScreenRegion;
            this.FolderPath = options.FolderPath;
        }

        public ScreenCaptureAction(Dictionary<string, string> propertyMap)
            : base(propertyMap)
        {
            try
            {
                this.FolderPath = propertyMap[nameof(FolderPath)];

                var region = propertyMap[nameof(ScreenRegion)].Split(',');
                var rec = new Rectangle();
                rec.X = Int32.Parse(region[0]);
                rec.Y = Int32.Parse(region[1]);
                rec.Width = Int32.Parse(region[2]);
                rec.Height = Int32.Parse(region[3]);
                this.ScreenRegion = rec;
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
                res[nameof(ScreenRegion)] = $"{ScreenRegion.X},{ScreenRegion.Y},{ScreenRegion.Width},{ScreenRegion.Height}";
                res[nameof(FolderPath)] = FolderPath;
                return res;
            }
        }

        protected override void KeyActionProcess()
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
            catch
            {
                BindKey.GetInstance().ShowBalloonTip("Error", "Could not capture screen region, check folder path.", ToolTipIcon.Error);
            }
        }

        public override string ToString()
        {
            return $"Capture {this.ScreenRegion.Width}x{this.ScreenRegion.Height} " +
                   $"to {(this.FolderPath == string.Empty ? "default folder" : this.FolderPath)}" + NextString;
        }
    }
}
