using BindKey.AddOptions;
using BindKey.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BindKey.KeyActions
{
    internal class CycleProfileAction : DefaultKeyAction
    {
        public override ActionTypes Type => ActionTypes.CycleProfile;
        public bool IsForward { get; }

        public override Dictionary<string, string> Properties
        {
            get
            {
                var res = base.Properties;
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
                var data = KeyActionData.GetInstance();
                var names = data.ProfileNames.ToList();
                var index = names.FindIndex(n => n == data.SelectedProfile);
                int newIndex = IsForward ? index + 1 : index - 1;
                if (newIndex >= names.Count)
                {
                    newIndex = 0;
                }
                else if (newIndex < 0)
                {
                    newIndex = names.Count - 1;
                }
                data.SelectedProfile = names[newIndex];
             
                AddMessage("Cycle Profile", $"Cycled to: {names[newIndex]}", ToolTipIcon.Info);
            }
            catch (Exception e)
            {
                AddMessage("Error", $"Could not cycle profile. Please report this message {e.Message}.", ToolTipIcon.Error);
            }
        }

        public override string ToString()
        {
            return $"Cycle to {(this.IsForward ? "next" : "previous")} profile" + NextString;
        }
    }
}
