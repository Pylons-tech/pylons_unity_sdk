using System;
using UnityEngine;

namespace PylonsSdk.RecipeBuilder
{
    [Serializable]
    public class LongOutputParamSpec : ScriptableObject
    {
        public double Rate;
        public string Key;
        public LongWeightRangeSpec[] WeightRanges;
        public GoExpression Expression;
    }
}