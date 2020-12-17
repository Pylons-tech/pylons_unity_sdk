using System;
using UnityEngine;

namespace PylonsSdk.RecipeBuilder
{
    [Serializable]
    public class LongInputParamSpec : ScriptableObject
    {
        public long MinValue;
        public long MaxValue;
        public string Key;
    }
}