using System;
using UnityEngine;

namespace PylonsSdk.RecipeBuilder
{
    [Serializable]
    public class StringOutputParamSpec : ScriptableObject
    {
        public double Rate;
        public string Key;
        public string Value;
        public GoExpression Expression;
    }
}