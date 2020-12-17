using UnityEditor;
using UnityEngine;

namespace PylonsSdk.RecipeBuilder.Editor
{
    [CustomEditor(typeof(CoinOutputSpec))]
    [CanEditMultipleObjects]
    public class CoinOutputSpecEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            HandleCoinOutput(serializedObject);
            serializedObject.ApplyModifiedProperties();
        }

        public static void HandleCoinOutput(SerializedObject so)
        {
            var count = so.FindProperty("Count");
            using (var vertical = new EditorGUILayout.VerticalScope())
            {
                using (var horizontal = new EditorGUILayout.HorizontalScope())
                {
                    EditorGUILayout.LabelField("Coin", EditorStyles.miniLabel);
                    EditorGUILayout.PropertyField(so.FindProperty("Coin"), new GUIContent(""));
                }
                EditorGUILayout.LabelField("Count");
                if (count.objectReferenceValue == null) EditorGUILayout.PropertyField(count, new GUIContent());
                else GoExpressionEditor.HandleGoExpression(new SerializedObject(count.objectReferenceValue));
            }
        }
    }
}