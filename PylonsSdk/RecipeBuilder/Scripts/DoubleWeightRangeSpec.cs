using System;
using UnityEngine;

namespace PylonsSdk.RecipeBuilder
{
    [Serializable]
    public class DoubleWeightRangeSpec : ScriptableObject
    {
        public double Lower;
        public double Upper;
        public int Weight;
    }
}