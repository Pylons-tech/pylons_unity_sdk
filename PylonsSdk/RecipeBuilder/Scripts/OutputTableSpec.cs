using UnityEngine;

namespace PylonsSdk.RecipeBuilder
{
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