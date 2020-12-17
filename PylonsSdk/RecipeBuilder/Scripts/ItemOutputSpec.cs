using PylonsSdk.Tx;
using System;
using UnityEngine;

namespace PylonsSdk.RecipeBuilder
{
    [Serializable]
    [CreateAssetMenu(fileName = "New Item Output", menuName = "Pylons/Recipe/Item Output")]
    public class ItemOutputSpec : ScriptableObject
    {
        public DoubleOutputParamSpec[] Doubles;
        public LongOutputParamSpec[] Longs;
        public StringOutputParamSpec[] Strings;
        public long TransferFee;

        public ItemOutput ToItemOutput()
        {
            throw new NotImplementedException("This functionality doesn't exist yet");
        }
    }
}