using PylonsSdk.Internal.Ipc;
using PylonsSdk.Internal.Ipc.Messages;
using PylonsSdk.Tx;

class Foo : UnityEngine.MonoBehaviour
{
    void Start ()
    {
        // These should run in order and not step on each other
        PylonsService.instance.WalletServiceTest("foo", Ev1);
        PylonsService.instance.WalletServiceTest("bar", Ev2);
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