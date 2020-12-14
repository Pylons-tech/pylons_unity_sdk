using System;
using UnityEngine;

namespace PylonsSdk.RecipeBuilder
{
    [Serializable]
    [CreateAssetMenu(fileName = "New Output Table", menuName = "Pylons/Recipe/Output Table")]
    public class OutputTableSpec : ScriptableObject
    {
        public string Name;
        public string Description;
        public double Weight;
        public CoinOutputSpec[] CoinOutputs;
        public ItemInputTransformationSpec[] ItemInputTransformations;
        public ItemOutputSpec[] ItemOutputs;
    }
}