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
    }
}
