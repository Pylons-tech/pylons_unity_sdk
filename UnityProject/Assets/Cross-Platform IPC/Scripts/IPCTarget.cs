namespace CrossPlatformIpc
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
    }
}

