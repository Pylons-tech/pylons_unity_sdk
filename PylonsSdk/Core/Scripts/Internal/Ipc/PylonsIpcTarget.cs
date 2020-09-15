using UnityEngine;

namespace PylonsSdk.Internal
{
    [PylonsIpc.DefaultIpcTarget]
    public class PylonsIpcTarget : PylonsIpc.IpcTarget
    {
        public PylonsIpcTarget() : base(
            ".activities.CoreInterfaceActivity",
                "com.pylons.wallet",
                ".WalletService",
                "fromClient",
                50001,
                "pylons_dwallet",
                GetRealPathToDevProcess("Packages/com.pylons.sdk.devwallet/pylons_dwallet.exe"),
                "cmd.exe",
                string.Join("", @"/k """"", GetRealPathToDevProcess("Packages/com.pylons.unity.sdk.devwallet/pylons_dwallet.exe"), $" {GetAddress()}"))
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