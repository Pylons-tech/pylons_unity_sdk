using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace PylonsSdk.Tx
{
    [JsonConverter(typeof(TxDataArrayConverter))]
    public readonly struct TxData
    {
        [JsonProperty("msg")]
        public readonly string Msg;
        [JsonProperty("status")]
        public readonly string Status;
        [JsonProperty("output")]
        public readonly TxDataOutput[] Output;
    }

    public class TxDataArrayConverter : JsonConverter<TxData[]>
    {
        public override bool CanRead => true;

        public override bool CanWrite => false;

        public override TxData[] ReadJson(JsonReader reader, Type objectType, TxData[] existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var token = JToken.Load(reader);
            switch (reader.TokenType)
            {
                case JsonToken.StartArray:
                    return token.ToObject<TxData[]>();
                case JsonToken.StartObject:
                    return new TxData[] { token.ToObject<TxData>() };
                default:
                    return new TxData[0];
            }
        }

        public override void WriteJson(JsonWriter writer, TxData[] value, JsonSerializer serializer) => throw new NotImplementedException();
    }
}