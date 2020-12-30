using BindKey.KeyActions;
using System;

namespace BindKey.AddOptions
{
    internal static class AddOptionsFactory
    {
        public static IAddOptions GetAddOptionsFromActionType(ActionTypes type, Add addForm)
        {
            IAddOptions res = null;

            switch (type)
            {
                case ActionTypes.OpenProcess:
                    res = new OpenProcessOptions(addForm);
                    break;
                case ActionTypes.ScreenCapture:
                    res = new ScreenCaptureOptions(addForm);
                    break;
                case ActionTypes.KillProcess:
                    res = new KillProcessOptions(addForm);
                    break;
                case ActionTypes.DeleteFiles:
                    res = new DeleteFilesOptions(addForm);
                    break;
                case ActionTypes.CycleProfile:
                    res = new CycleProfileOptions(addForm);
                    break;
                default:
                    throw new ArgumentException($"DEV ERROR: could not create add options from factory of type {type}.");
            }

            return res;
        }
    }
}
