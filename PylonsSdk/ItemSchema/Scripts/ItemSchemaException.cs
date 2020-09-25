namespace PylonsSdk.ItemSchema
{
    public class ItemSchemaException : System.Exception
    {
        public ItemSchemaException() : base() { }
        public ItemSchemaException(string message) : base(message) { }
        public ItemSchemaException(string message, System.Exception innerException) : base(message, innerException) { }
    }
}