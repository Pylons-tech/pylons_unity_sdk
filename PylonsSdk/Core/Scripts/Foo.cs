using PylonsSdk.Internal.Ipc;
using PylonsSdk.Internal.Ipc.Messages;
using PylonsSdk.Tx;

class Foo : UnityEngine.MonoBehaviour
{
    void Start ()
    {
        PylonsService.instance.WalletServiceTest("foo", Ev1);
    }

    void Ev1 (object sender, object response)
    {
        UnityEngine.Debug.Log("ev1");
    }
}