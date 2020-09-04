namespace PylonsSDK.Internal
{
    [CrossPlatformIPC.DefaultIPCTarget]
    public class PylonsIPCTarget : CrossPlatformIPC.IPCTarget
    {
        public PylonsIPCTarget() : base(
            ".activities.CoreInterfaceActivity",
                "com.pylons.wallet",
                ".WalletService",
                "fromClient",
                50001,
                "pylons_dwallet",
                "/DevWallet/pylons_dwallet.exe",
                "cmd.exe",
                string.Join("", @"/k """"", UnityEngine.Application.dataPath.Replace("/Assets", ""), $@"/DevWallet/pylons_dwallet.exe"" {GetAddress()}"""))
        { }

        private static string GetAddress()
        {
            var a = UnityEngine.Resources.Load<UnityEngine.TextAsset>("node.cfg")?.text;
            if (a == null)
            {
                UnityEngine.Debug.LogWarning("Couldn't fine node.cfg; assuming Pylons node is running locally.");
                a = "127.0.0.1";
            }
            return a;
        }
    }
}