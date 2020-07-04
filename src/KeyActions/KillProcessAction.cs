﻿using BindKey.AddOptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace BindKey.KeyActions
{
    internal class KillProcessAction : DefaultKeyAction
    {
        public string ProcessName { get; }
        public override ActionTypes Type { get => ActionTypes.KillProcess; }

        protected override List<string> SaveOrder
        {
            get
            {
                List<string> res = new List<string>(base.SaveOrder);
                res.AddRange(new List<string>
                {
                    this.ProcessName
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
            }
            catch (Exception)
            {
                MessageBox.Show("Error: could not kill or start process.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public KillProcessAction(KillProcessOptions options, string GUID = "")
            : base(options, GUID)
        {
            this.ProcessName = options.ProcessName;
        }

        public KillProcessAction(string[] parts)
            : base(parts)
        {
            this.ProcessName = parts[7];
        }

        public override string ToString()
        {
            return $"Kill {this.ProcessName}" +
                   $"{(this.NextKeyAction != null ? $" -> {this.NextKeyAction.ToString()}": string.Empty)}" + NextString;
        }
    }
}