using System;
using UnityEngine;

namespace PylonsSdk.RecipeBuilder
{
    [Serializable]
    public class DoubleOutputParamSpec : ScriptableObject
    {
        public double Rate;
        public string Key;
        public DoubleWeightRangeSpec[] WeightRanges;
        public GoExpression Expression;
    }
}