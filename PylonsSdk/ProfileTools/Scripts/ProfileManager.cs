
using UnityEngine;
using PylonsSdk.Internal.Ipc.Messages;
using PylonsIpc;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace PylonsSdk.ProfileTools
{
    public static class ProfileManager
    {
        public static bool Initialized { get; private set; }

        [PylonsService.RunOnServiceLive]
        public static void ColdBootInit()
        {
#if UNITY_EDITOR
            var defaultPrivkey = GetDefaultStoredKey();
            if (!string.IsNullOrEmpty(defaultPrivkey))
            {
                IpcInteraction.Stage(() => new AddKeypair(defaultPrivkey), (s, p) => { Initialized = true; });
            }
            else
            {
                PylonsService.instance.RegisterProfile("", (s, p) => { Initialized = true; });
            }        
#endif
        }

#if UNITY_EDITOR
        public static string GetDefaultStoredKey()
        {
            var defs = ProfileDef.GetAll();
            if (defs.Length > 0) return defs[0].PrivateKey;
            else return "";
        }
#endif

    }
}