using BindKey.KeyActions;
using System;

namespace BindKey.AddOptions
{
    internal static class AddOptionsUtil
    {
        public static void FillFormFromAction(IKeyAction action, Add form)
        {
            var type = action.Type;
            IAddOptions options = AddOptionsFactory.GetAddOptionsOfType(type, form);

            switch (type)
            {
                case ActionTypes.OpenProcess:
                    ((OpenProcessOptions)options).FillForm(action as OpenProcessAction);
                    break;
                case ActionTypes.ScreenCapture:
                    ((ScreenCaptureOptions)options).FillForm(action as ScreenCaptureAction);
                    break;
                case ActionTypes.KillProcess:
                    ((KillProcessOptions)options).FillForm(action as KillProcessAction);
                    break;
                case ActionTypes.DeleteFiles:
                    ((DeleteFilesOptions)options).FillForm(action as DeleteFilesAction);
                    break;
                default:
                    throw new ArgumentException($"DEV ERROR: could not fill form from type {type}.");
            }
        }

        public static bool ValidateFormFromType(ActionTypes type, IAddOptions options)
        {
            bool res = false;

            switch (type)
            {
                case ActionTypes.OpenProcess:
                    res = ((OpenProcessOptions)options).Validate();
                    break;
                case ActionTypes.ScreenCapture:
                    res = ((ScreenCaptureOptions)options).Validate();
                    break;
                case ActionTypes.KillProcess:
                    res = ((KillProcessOptions)options).Validate();
                    break;
                case ActionTypes.DeleteFiles:
                    res = ((DeleteFilesOptions)options).Validate();
                    break;
                default:
                    throw new ArgumentException($"DEV ERROR: could not validate form from type {type}.");
            }
            return res;
        }
    }
}
