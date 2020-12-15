using UnityEditor;

namespace PylonsSdk.RecipeBuilder.Editor
{
    [CustomEditor(typeof(CoinInputSpec))]
    [CanEditMultipleObjects]
    public class CoinInputSpecEditor : UnityEditor.Editor
    {
        public static void HandleCoinInput (SerializedObject coinInput)
        {

        }
    }
}