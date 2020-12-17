using PylonsSdk.Tx;
using System;
using UnityEngine;

namespace PylonsSdk.RecipeBuilder
{
    [Serializable]
    [CreateAssetMenu(fileName = "New Item Input", menuName = "Pylons/Recipe/Item Input")]
    public class ItemInputSpec : ScriptableObject
    {
        public DoubleInputParamSpec[] Doubles;
        public LongInputParamSpec[] Longs;
        public StringInputParamSpec[] Strings;

        public ItemInput ToItemInput ()
        {
            throw new NotImplementedException("This functionality doesn't exist yet");
        }
    }
}