using BindKey.AddOptions;
using System;

namespace BindKey.KeyActions
{
    internal static class KeyActionFactory
    {
        public static IKeyAction GetNewKeyActionOfType(ActionTypes type, IAddOptions options, string GUID = "")
        {
            IKeyAction? res = null;

            switch (type)
            {
                case ActionTypes.OpenProcess:
                    res = new OpenProcessAction(options as OpenProcessOptions, GUID);
                    break;
                case ActionTypes.ScreenCapture:
                    res = new ScreenCaptureAction(options as ScreenCaptureOptions, GUID);
                    break;
                case ActionTypes.KillProcess:
                    res = new KillProcessAction(options as KillProcessOptions, GUID);
                    break;
                case ActionTypes.DeleteFiles:
                    res = new DeleteFilesAction(options as DeleteFilesOptions, GUID);
                    break;
                default:
                    throw new ArgumentException($"DEV ERROR: could not create key action from factory of type {type}.");
            }
            return res;
        }

        public static IKeyAction GetNewKeyActionOfType(string[] parts)
        {
            IKeyAction? res = null;
            ActionTypes type = (ActionTypes)Enum.Parse(typeof(ActionTypes), parts[0], true);

            switch (type)
            {
                case ActionTypes.OpenProcess:
                    res = new OpenProcessAction(parts);
                    break;
                case ActionTypes.ScreenCapture:
                    res = new ScreenCaptureAction(parts);
                    break;
                case ActionTypes.KillProcess:
                    res = new KillProcessAction(parts);
                    break;
                case ActionTypes.DeleteFiles:
                    res = new DeleteFilesAction(parts);
                    break;
                default:
                    throw new ArgumentException($"DEV ERROR: could not create key action from factory of type {type}.");
            }
            return res;
        }
    }
}
