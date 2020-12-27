using BindKey.AddOptions;
using System;
using System.Collections.Generic;

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
                case ActionTypes.CycleProfile:
                    res = new CycleProfileAction(options as CycleProfileOptions, GUID);
                    break;
                default:
                    throw new ArgumentException($"DEV ERROR: could not create key action from factory of type {type}.");
            }
            return res;
        }

        public static IKeyAction GetNewKeyActionOfType(Dictionary<string, string> propertyMap)
        {
            IKeyAction? res = null;
            ActionTypes type = (ActionTypes)Enum.Parse(typeof(ActionTypes), propertyMap["Type"], true);

            switch (type)
            {
                case ActionTypes.OpenProcess:
                    res = new OpenProcessAction(propertyMap);
                    break;
                case ActionTypes.ScreenCapture:
                    res = new ScreenCaptureAction(propertyMap);
                    break;
                case ActionTypes.KillProcess:
                    res = new KillProcessAction(propertyMap);
                    break;
                case ActionTypes.DeleteFiles:
                    res = new DeleteFilesAction(propertyMap);
                    break;
                case ActionTypes.CycleProfile:
                    res = new CycleProfileAction(propertyMap);
                    break;
                default:
                    throw new ArgumentException($"DEV ERROR: could not create key action from factory of type {type}.");
            }
            return res;
        }
    }
}
