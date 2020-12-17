using System;
using UnityEngine;

namespace PylonsSdk.RecipeBuilder
{
    [Serializable]
    public class LongWeightRangeSpec : ScriptableObject
    {
        public long Lower;
        public long Upper;
        public int Weight;
    }
}