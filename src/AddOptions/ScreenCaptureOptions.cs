using BindKey.KeyActions;
using System.Drawing;
using System.Windows.Forms;

namespace BindKey.AddOptions
{
    internal class ScreenCaptureOptions : DefaultAddOptions
    {
        public const string CONTROL_FOLDERPATH = "textBox2";
        public const string CONTROL_PICTUREBOX = "pictureBox1";

        public override ActionTypes Type { get => ActionTypes.ScreenCapture; }
        public string FolderPath { get => (GetControl(CONTROL_FOLDERPATH) as TextBox).Text; }
        public Rectangle? ScreenRegion { get => AddForm.SelectedRegion; }

        public ScreenCaptureOptions(Add addForm)
            : base(addForm)
        { }

        public void FillForm(ScreenCaptureAction action)
        {
            base.FillForm(action);
            AddForm.SelectedRegion = action.ScreenRegion;
            SetControl<PictureBox>(CONTROL_PICTUREBOX, Add.GetBitMapFromRegion(action.ScreenRegion));
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
                if (ScreenRegion == null)
                {
                    MessageBox.Show("Error: a screen region must be selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return res;
        }
    }
}
