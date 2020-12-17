using System;
using UnityEngine;

namespace PylonsSdk.RecipeBuilder
{
    [Serializable]
    public class DoubleInputParamSpec : ScriptableObject
    {
        public double MinValue;
        public double MaxValue;
        public string Key;
    }
}