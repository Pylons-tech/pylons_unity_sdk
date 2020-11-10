namespace PylonsSdk.Internal.Ipc.Messages
{
    /// <summary>
    /// IPC: Exports the current keypair from the linked wallet.
    /// Gets a KeyResponse containing those keys.
    /// </summary>
    public sealed class ExportKeys : IpcMessage
    {
        public ExportKeys() : base(ResponseType.KEY_RESPONSE)
        {
            // ExportKeys doesn't have any arguments, so it has an empty constructor.
            // Manually specifying an empty constructor is still necessary because we
            // have to provide the ResponseType to the base class.
        }
    }
}