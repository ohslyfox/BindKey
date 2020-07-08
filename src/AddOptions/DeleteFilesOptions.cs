using System.Windows.Forms;
using BindKey.KeyActions;

namespace BindKey.AddOptions
{
    internal class DeleteFilesOptions : DefaultAddOptions
    {
        public const string CONTROL_DAYS = "DeleteDaysTextBox";
        public const string CONTROL_HOURS = "DeleteHoursTextBox";
        public const string CONTROL_MINUTES = "DeleteMinutesTextBox";
        public const string CONTROL_SECONDS = "DeleteSecondsTextBox";
        public const string CONTROL_SEARCHPATTERN = "DeleteSearchPatternTextBox";
        public const string CONTROL_FOLDERPATH = "DeleteFolderPathTextBox";

        public override ActionTypes Type { get => ActionTypes.DeleteFiles; }
        public string Days { get => (GetControl(CONTROL_DAYS) as TextBox).Text; }
        public string Hours { get => (GetControl(CONTROL_HOURS) as TextBox).Text; }
        public string Minutes { get => (GetControl(CONTROL_MINUTES) as TextBox).Text; }
        public string Seconds { get => (GetControl(CONTROL_SECONDS) as TextBox).Text; }
        public string SearchPattern { get => (GetControl(CONTROL_SEARCHPATTERN) as TextBox).Text; }
        public string FolderPath { get => (GetControl(CONTROL_FOLDERPATH) as TextBox).Text; }

        public DeleteFilesOptions(Add addForm)
            : base(addForm)
        { }

        public void FillForm(DeleteFilesAction action)
        {
            base.FillForm(action);
            SetControl<TextBox>(CONTROL_DAYS, action.Days);
            SetControl<TextBox>(CONTROL_HOURS, action.Hours);
            SetControl<TextBox>(CONTROL_MINUTES, action.Minutes);
            SetControl<TextBox>(CONTROL_SECONDS, action.Seconds);
            SetControl<TextBox>(CONTROL_SEARCHPATTERN, action.SearchPattern);
            SetControl<TextBox>(CONTROL_FOLDERPATH, action.FolderPath);
        }

        public override bool Validate()
        {
            bool res = base.Validate();
            if (res)
            {
                if (string.IsNullOrWhiteSpace(FolderPath))
                {
                    MessageBox.Show("Error: a folder path must be set.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return res;
        }
    }
}
