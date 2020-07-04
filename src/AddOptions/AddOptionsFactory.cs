using BindKey.KeyActions;
using System;

namespace BindKey.AddOptions
{
    internal class AddOptionsFactory
    {
        public static IAddOptions GetAddOptionsOfType(ActionTypes type, Add addForm)
        {
            IAddOptions? res = null;

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
                default:
                    Console.WriteLine($"DEV ERROR: could not create add options from factory of type {type}.");
                    Environment.Exit(1);
                    break;
            }

            return res;
        }
    }
}
