using BindKey.AddOptions;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BindKey.KeyActions
{
    internal class CycleProfileAction : DefaultKeyAction
    {
        public override ActionTypes Type => ActionTypes.CycleProfile;
        public bool IsForward { get; }

        protected override Dictionary<string, string> ItemsToSave
        {
            get
            {
                var res = base.ItemsToSave;
                res[nameof(IsForward)] = IsForward.ToString();
                return res;
            }
        }

        public CycleProfileAction(CycleProfileOptions options, string GUID = "")
            : base(options, GUID)
        {
            this.IsForward = options.IsForward;
        }


        public CycleProfileAction(Dictionary<string, string> propertyMap)
            : base(propertyMap)
        {
            try
            {
                this.IsForward = bool.Parse(propertyMap[nameof(IsForward)]);
            }
            catch
            {
                MessageBox.Show("Failed to create key action. Corrupted save file or bad input.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void KeyActionProcess()
        {
            try
            {
                var newProfileName = BindKey.GetInstance().CycleProfile(this.IsForward);
                BindKey.GetInstance().ShowBalloonTip("BindKey", $"Cycled to profile: {newProfileName}.");
            }
            catch (Exception e)
            {
                BindKey.GetInstance().ShowBalloonTip("Error", $"Could not cycle profile. Please report this message {e.Message}.");
            }
        }

        public override string ToString()
        {
            return $"Cycle to {(this.IsForward ? "next" : "previous")} profile" + NextString;
        }
    }
}
