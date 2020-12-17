using System;
using UnityEngine;

namespace PylonsSdk.RecipeBuilder
{
    [Serializable]
    [CreateAssetMenu(fileName = "New Coin Output", menuName = "Pylons/Recipe/Coin Output")]
    public class CoinOutputSpec : ScriptableObject
    {
        public string Coin;
        public GoExpression Quantity;

        public CoinOutput ToCoinOutput()
        {
            throw new NotImplementedException("This functionality doesn't exist yet");
        }
    }
}