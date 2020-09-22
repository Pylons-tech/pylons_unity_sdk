namespace PylonsIpc
{
    public interface IBroadcastable
    {
        void Broadcast(params IpcEvent[] evts);
    }
}
