using System;

namespace PylonsSDK.Tx
{
    public readonly struct TxError
    {
        public readonly int Code;
        public readonly string Message;

        public TxError(int code, string message)
        {
            Code = code;
            Message = message ?? throw new ArgumentNullException(nameof(message));
        }
    }
}