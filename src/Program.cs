using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BindKey
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (EnforceSingleInstance()) return;
            string filePath = string.Empty;
            if (args.Length > 0)
            {
                if (File.Exists(args[0]) && args[0].EndsWith(".bk"))
                {
                    filePath = args[0];
                }
                else
                {
                    MessageBox.Show("Unrecognized file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            FileAssociations.EnsureAssociationsSet();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new BindKey(filePath));
        }

        private static bool EnforceSingleInstance()
        {
            bool res = false;
            Process[] p = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName);
            if (p.Length > 1)
            {
                MessageBox.Show("BindKey is already running.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                res = true;
            }
            return res;
        }

        private class FileAssociations
        {
            // needed so that Explorer windows get refreshed after the registry is updated
            [System.Runtime.InteropServices.DllImport("Shell32.dll")]
            private static extern int SHChangeNotify(int eventId, int flags, IntPtr item1, IntPtr item2);

            private const int SHCNE_ASSOCCHANGED = 0x8000000;
            private const int SHCNF_FLUSH = 0x1000;

            public static void EnsureAssociationsSet()
            {
                try
                {
                    var filePath = Process.GetCurrentProcess().MainModule.FileName;
                    EnsureAssociationsSet(
                        new FileAssociation
                        {
                            Extension = ".bk",
                            ProgId = "BindKey_Save_File",
                            FileTypeDescription = "BindKey Save File",
                            ExecutableFilePath = filePath
                        });
                }
                catch (Exception e)
                {
                    // do nothing
                }
            }

            private static void EnsureAssociationsSet(params FileAssociation[] associations)
            {
                bool madeChanges = false;
                foreach (var association in associations)
                {
                    madeChanges |= SetAssociation(
                        association.Extension,
                        association.ProgId,
                        association.FileTypeDescription,
                        association.ExecutableFilePath);
                }

                if (madeChanges)
                {
                    SHChangeNotify(SHCNE_ASSOCCHANGED, SHCNF_FLUSH, IntPtr.Zero, IntPtr.Zero);
                }
            }

            public static bool SetAssociation(string extension, string progId, string fileTypeDescription, string applicationFilePath)
            {
                bool madeChanges = false;
                madeChanges |= SetKeyDefaultValue(@"Software\Classes\" + extension, progId);
                madeChanges |= SetKeyDefaultValue(@"Software\Classes\" + progId, fileTypeDescription);
                madeChanges |= SetKeyDefaultValue($@"Software\Classes\{progId}\shell\open\command", "\"" + applicationFilePath + "\" \"%1\"");
                return madeChanges;
            }

            private static bool SetKeyDefaultValue(string keyPath, string value)
            {
                using (var key = Registry.CurrentUser.CreateSubKey(keyPath))
                {
                    if (key.GetValue(null) as string != value)
                    {
                        key.SetValue(null, value);
                        return true;
                    }
                }

                return false;
            }

            private class FileAssociation
            {
                public string Extension { get; set; }
                public string ProgId { get; set; }
                public string FileTypeDescription { get; set; }
                public string ExecutableFilePath { get; set; }
            }
        }
    }
}
