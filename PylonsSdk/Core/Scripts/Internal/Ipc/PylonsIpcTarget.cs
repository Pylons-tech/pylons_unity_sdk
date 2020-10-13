using UnityEngine;

namespace PylonsSdk.Internal
{
    [PylonsIpc.DefaultIpcTarget]
    public class PylonsIpcTarget : PylonsIpc.IpcTarget
    {
        public const string WALLET_DATA_PATH = "_wallet";

        private static string privKey = null;

        public static void SetPrivKey(string key) => privKey = key;

        public override string GenerateDevProcessArguments(bool hosted)
        {
            string output;
            if (privKey != null) output = $"{GetAddress()} {privKey}";
            else output = GetAddress();
            if (hosted) return string.Join("", @"/k """"", GetRealPathToDevProcess("Packages/com.pylons.unity.sdk.devwallet/pylons_dwallet.exe "), output);
            else return output;
        }

        public PylonsIpcTarget() : base(
            ".activities.CoreInterfaceActivity",
                "com.pylons.wallet",
                ".WalletService",
                50001,
                "pylons_dwallet",
                GetRealPathToDevProcess("Packages/com.pylons.sdk.devwallet/pylons_dwallet.exe"),
                "cmd.exe")
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