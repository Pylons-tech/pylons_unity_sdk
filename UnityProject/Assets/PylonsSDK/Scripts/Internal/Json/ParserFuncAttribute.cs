using System;
using Newtonsoft.Json.Linq;

namespace PylonsSDK.Internal.Json
{
    public sealed class ParserFuncAttribute : Attribute
    {
        public readonly Func<JToken, object> Function;

        public ParserFuncAttribute(Func<JToken, object> function)
        {
            Function = function ?? throw new ArgumentNullException(nameof(function));
        }
    }
}