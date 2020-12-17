using System;
using UnityEngine;
using PylonsSdk.ItemTools;

namespace PylonsSdk.RecipeBuilder
{
    [Serializable]
    [CreateAssetMenu(fileName = "New Recipe", menuName = "Pylons/Recipe/Recipe")]
    public class RecipeSpec : ScriptableObject
    {
        public CookbookSpec Cookbook;
        public string Name;
        public string Description;
        public bool Enabled;
        /// <summary>
        /// Execution delay, represented in seconds.
        /// RecipeBuilderTool has to convert this back into
        /// block time.
        /// </summary>
        public double ExecutionDelay;
        public CoinInputSpec[] CoinInputs = new CoinInputSpec[0];
        // TODO: I want to use ItemSchema here but it's going to be harder to implement in a hurry
        // and we need to redesign the system somewhat to be a little easier to bridge to the node.
        // So ATM we have these lower-level types instead.
        //public ItemSchema[] ItemInputs = new ItemSchema[0];
        public ItemInputSpec[] ItemInputs = new ItemInputSpec[0];
        public OutputTableSpec[] Outputs = new OutputTableSpec[0];
        public ChainIdentity[] Identities = new ChainIdentity[0];
    }
}
