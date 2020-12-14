using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PylonsSdk.RecipeBuilder
{
    [CreateAssetMenu(fileName = "New Recipe", menuName = "Pylons/Recipe")]
    public class RecipeSpec : ScriptableObject
    {
        public CookbookSpec Cookbook;
        public string Name;
        public string Description;
        public bool Enabled;
        public TimeSpan ExecutionDelay;
        public CoinInputSpec[] CoinInputs;
        public ItemInputSpec[] ItemInputs;
        // how do we manage public/local node stuff here? (we don't need to actually do it, but we need to know what the ui looks like and how the data is stored)
    }
}
