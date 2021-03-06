﻿using PylonsSdk.Tx;
using System;
using UnityEngine;

namespace PylonsSdk.RecipeBuilder
{
    [Serializable]
    [CreateAssetMenu(fileName = "New Coin Input", menuName = "Pylons/Recipe/Coin Input")]
    public class CoinInputSpec : ScriptableObject
    {
        public string Coin;
        public long Quantity;

        public CoinInput ToCoinInput()
        {
            throw new NotImplementedException("This functionality doesn't exist yet");
        }
    }
}