using PylonsSDK.Ipc.Internal;
using PylonsSDK.Ipc.Internal.Messages;
using PylonsSDK.Tx;

class Foo : UnityEngine.MonoBehaviour
{
    void Awake ()
    {
        PylonsSDK.Service.WalletServiceTest("foo", Ev1, Ev2, Ev1);
    }

    void Ev1 (object sender, object response)
    {
        UnityEngine.Debug.Log("ev1");
    }

    void Ev2(object sender, object response)
    {
        UnityEngine.Debug.Log("ev2");
    }
}