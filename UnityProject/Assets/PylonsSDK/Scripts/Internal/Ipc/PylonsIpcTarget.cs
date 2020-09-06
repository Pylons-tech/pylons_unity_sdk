using UnityEngine;

namespace PylonsSdk.Internal
{
    [CrossPlatformIpc.DefaultIpcTarget]
    public class PylonsIpcTarget : CrossPlatformIpc.IpcTarget
    {
        public PylonsIpcTarget() : base(
            ".activities.CoreInterfaceActivity",
                "com.pylons.wallet",
                ".WalletService",
                "fromClient",
                50001,
                "pylons_dwallet",
                "/DevWallet/pylons_dwallet.exe",
                "cmd.exe",
                string.Join("", @"/k """"", Application.dataPath.Replace("/Assets", ""), $@"/DevWallet/pylons_dwallet.exe"" {GetAddress()}"""))
        {
            Debug.Log("Set up PylonsIpcTarget");
        }

        private static string GetAddress()
        {
            var a = Resources.Load<TextAsset>("node.cfg")?.text;
            if (a == null)
            {
                Debug.LogWarning("Couldn't fine node.cfg; assuming Pylons node is running locally.");
                a = "127.0.0.1";
            }
            return a;
        }
    }
}