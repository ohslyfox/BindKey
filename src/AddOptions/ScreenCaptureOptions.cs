using BindKey.KeyActions;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace BindKey.AddOptions
{
    internal class ScreenCaptureOptions : DefaultAddOptions
    {
        public const string CONTROL_FOLDERPATH = "textBox2";

        public override ActionTypes Type { get => ActionTypes.ScreenCapture; }
        public string FolderPath { get => (AddForm.Controls.Find(CONTROL_FOLDERPATH, true).First() as TextBox).Text; }
        public Rectangle? ScreenRegion { get => AddForm.SelectedRegion; }

        public ScreenCaptureOptions(Add addForm)
            : base(addForm)
        { }
    }
}
