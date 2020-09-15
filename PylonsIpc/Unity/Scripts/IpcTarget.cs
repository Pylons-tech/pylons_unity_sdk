
using System.Text;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor.PackageManager;
#endif

namespace PylonsIpc
{
    public abstract class IpcTarget
    {
        public static IpcTarget instance;

        static IpcTarget ()
        {
            instance = DefaultIpcTarget.Get();
        }

        public readonly string androidActivityName;
        public readonly string androidPackageName;
        public readonly string androidServiceName;
        public readonly string devProcessArguments;
        public readonly string devProcessName;
        public readonly string devProcessPath;
        public readonly string devProcessHostPath;
        public readonly string devProcessHostedStartArguments;
        public readonly bool devProcessIsHosted;
        public readonly int devProcessComPort;

        public IpcTarget (string _androidActivityName, string _androidPackageName, string _androidServiceName, string _devProcessArguments, int _devProcessComPort, string _devProcessName, string _devProcessPath)
        {
            androidActivityName = _androidActivityName;
            androidPackageName = _androidPackageName;
            androidServiceName = _androidServiceName;
            devProcessArguments = _devProcessArguments;
            devProcessName = _devProcessName;
            devProcessPath = _devProcessPath;
            devProcessHostPath = null;
            devProcessIsHosted = false;
            devProcessHostedStartArguments = null;
            devProcessComPort = _devProcessComPort;
        }

        public IpcTarget(string _androidActivityName, string _androidPackageName, string _androidServiceName, string _devProcessArguments, int _devProcessComPort, string _devProcessName, string _devProcessPath,
            string _devProcessHostPath, string _devProcessHostedStartArguments)
        {
            androidActivityName = _androidActivityName;
            androidPackageName = _androidPackageName;
            androidServiceName = _androidServiceName;
            devProcessArguments = _devProcessArguments;
            devProcessName = _devProcessName;
            devProcessPath = _devProcessPath;
            devProcessHostPath = _devProcessHostPath;
            devProcessIsHosted = true;
            devProcessHostedStartArguments = _devProcessHostedStartArguments;
            devProcessComPort = _devProcessComPort;
        }


        public static string GetRealPathToDevProcess(string devProcessPath)
        {
#if UNITY_EDITOR
            var defaultVal = Application.dataPath.Replace("/Assets", "") + "/" + devProcessPath;
            var split = devProcessPath.Split(new char[] { '\\', '/' });
            if (split[0] == ("Packages"))
            {
                var packages = Client.List();
                while (!packages.IsCompleted) { } // idle wait is slow but this will be moved to a codegen pass so it doesn't matter
                foreach (var package in packages.Result)
                {
                    if (package.name == split[1])
                    {
                        var sb = new StringBuilder(package.resolvedPath);
                        for (int i = 2; i < split.Length; i++) sb.Append($"\\{split[i]}");
                        return sb.ToString();
                    }
                }
                return defaultVal;
            }
            else return defaultVal;
            return devProcessPath;
#else
            // This API doesn't exist outside of editor. This doesn't matter b/c this path is only used in editor; this call will
            // just be compiled out.
            return devProcessPath;
#endif
        }
    }
}