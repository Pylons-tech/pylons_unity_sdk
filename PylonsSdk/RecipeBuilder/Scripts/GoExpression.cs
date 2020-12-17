using System;
using UnityEngine;

namespace PylonsSdk.RecipeBuilder
{
    [Serializable]
    [CreateAssetMenu(fileName = "New GoExpression", menuName = "Pylons/Recipe/GoExpression")]
    public class GoExpression : ScriptableObject
    {
        public string Name;
        public string Description;
        public string Expression;
    }
}