using System;
using UnityEngine;

namespace PylonsSdk.RecipeBuilder
{
    /// <summary>
    /// Internal data structure used by the RecipeBuilder to keep and track
    /// metadata correlating the local RecipeSpec object trees with the
    /// various recipes that exist on real Pylons blockchains.
    /// This is necessary because we need to be able to track the
    /// relationship between a RecipeSpec and its representations on
    /// both (e.g.) the local dev node and the real production network.
    /// 
    /// TODO: Dunno what this actually looks like, yet, just that it exists
    /// </summary>
    [Serializable]
    public class ChainIdentity : ScriptableObject
    {
    }
}
