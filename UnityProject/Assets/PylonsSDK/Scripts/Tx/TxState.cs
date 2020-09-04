namespace PylonsSDK.Tx
{
    public enum TxState
    {
        TX_REFUSED = -1,
        TX_NOT_YET_SENT = 0,
        TX_NOT_YET_COMMITTED = 1,
        TX_ACCEPTED = 2
    }
}