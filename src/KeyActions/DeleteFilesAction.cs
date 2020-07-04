using BindKey.AddOptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace BindKey.KeyActions
{
    internal class DeleteFilesAction : DefaultKeyAction
    {
        public string FolderPath { get; }
        public string SearchPattern { get; }
        public string Days { get; }
        public string Hours { get; }
        public string Minutes { get; }
        public string Seconds { get; }
        public override ActionTypes Type { get => ActionTypes.DeleteFiles; }

        protected override List<string> SaveOrder
        {
            get
            {
                List<string> res = new List<string>(base.SaveOrder);
                res.AddRange(new List<string>
                {
                    this.FolderPath, this.SearchPattern, this.Days,
                    this.Hours, this.Minutes, this.Seconds
                });
                return res;
            }
        }

        protected override void KeyActionProcess()
        {
            try
            {
                if (Directory.Exists(this.FolderPath))
                {
                    var files = Directory.GetFiles(this.FolderPath, string.IsNullOrWhiteSpace(this.SearchPattern) ? "*" : this.SearchPattern);
                    foreach (var file in files)
                    {
                        DateTime time = File.GetCreationTime(file);
                        time = time.AddDays(double.Parse(this.Days));
                        time = time.AddHours(double.Parse(this.Hours));
                        time = time.AddMinutes(double.Parse(this.Minutes));
                        time = time.AddSeconds(double.Parse(this.Seconds));
                        if (DateTime.Now > time)
                        {
                            File.Delete(file);
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error: could not delete file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public DeleteFilesAction(DeleteFilesOptions options, string GUID)
            : base(options, GUID)
        {
            this.FolderPath = options.FolderPath;
            this.SearchPattern = options.SearchPattern;
            this.Days = options.Days;
            this.Hours = options.Hours;
            this.Minutes = options.Minutes;
            this.Seconds = options.Seconds;
        }

        public DeleteFilesAction(string[] parts)
            : base(parts)
        {
            this.FolderPath = parts[7];
            this.SearchPattern = parts[8];
            this.Days = parts[9];
            this.Hours = parts[10];
            this.Minutes = parts[11];
            this.Seconds = parts[12];
        }

        public override string ToString()
        {
            return $"Search {this.FolderPath} for files matching {(string.IsNullOrWhiteSpace(this.SearchPattern) ? "*" : this.SearchPattern)} " +
                   $"and delete results older than {(string.IsNullOrWhiteSpace(this.Days) ? "0" : this.Days)} days " +
                   $"{(string.IsNullOrWhiteSpace(this.Hours) ? "0" : this.Hours)} hours " +
                   $"{(string.IsNullOrWhiteSpace(this.Minutes) ? "0" : this.Minutes)} minutes " +
                   $"{(string.IsNullOrWhiteSpace(this.Seconds) ? "0" : this.Seconds)} seconds" + NextString;
        }
    }
}
