using PylonsSdk.Internal.Ipc;
using PylonsSdk.Internal.Ipc.Messages;
using PylonsSdk.Tx;

class Foo : UnityEngine.MonoBehaviour
{
    void Awake ()
    {
        PylonsSdk.Service.WalletServiceTest("foo", Ev1, Ev2, Ev1);
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