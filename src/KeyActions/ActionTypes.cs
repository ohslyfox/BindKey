namespace BindKey.KeyActions
{
    internal enum ActionTypes
    {
        None = 0,
        CycleProfile = 1,
        DeleteFiles = 2,
        KillProcess = 3,
        OpenProcess = 4,
        ScreenCapture = 5,
        ShowHideProcess = 6
    }

    internal static class ActionTypesExtensions
    {
        public static string GetDescription(this ActionTypes type)
        {
            string res = string.Empty;
            switch (type)
            {
                case ActionTypes.CycleProfile:
                    res = "Cycle Profile";
                    break;
                case ActionTypes.DeleteFiles:
                    res = "Delete Files";
                    break;
                case ActionTypes.ShowHideProcess:
                    res = "Show or Hide a Process";
                    break;
                case ActionTypes.KillProcess:
                    res = "Kill Process";
                    break;
                case ActionTypes.OpenProcess:
                    res = "Open Folder or Start Process";
                    break;
                case ActionTypes.ScreenCapture:
                    res = "Take Screenshot";
                    break;
            }
            return res;
        }
    }
}
