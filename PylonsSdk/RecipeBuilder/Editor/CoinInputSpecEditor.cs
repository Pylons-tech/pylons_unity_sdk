using UnityEditor;
using UnityEngine;

namespace PylonsSdk.RecipeBuilder.Editor
{
    [CustomEditor(typeof(CoinInputSpec))]
    [CanEditMultipleObjects]
    public class CoinInputSpecEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            HandleCoinInput(serializedObject);
            serializedObject.ApplyModifiedProperties();
        }

        public static void HandleCoinInput (SerializedObject so)
        {
            using (var horizontal = new EditorGUILayout.HorizontalScope())
            {
                using (var vertical = new EditorGUILayout.VerticalScope())
                {
                    EditorGUILayout.LabelField("Coin", EditorStyles.miniLabel);
                    EditorGUILayout.PropertyField(so.FindProperty("Coin"), new GUIContent(""));
                }
                using (var vertical = new EditorGUILayout.VerticalScope())
                {
                    EditorGUILayout.LabelField("Quantity", EditorStyles.miniLabel);
                    EditorGUILayout.PropertyField(so.FindProperty("Quantity"), new GUIContent(""));
                }
            }
        }
    }
}