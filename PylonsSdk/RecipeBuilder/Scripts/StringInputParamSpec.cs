using System;
using UnityEngine;

namespace PylonsSdk.RecipeBuilder
{
    [Serializable]
    public class StringInputParamSpec : ScriptableObject
    {
        public string Value;
        public string Key;
    }
}