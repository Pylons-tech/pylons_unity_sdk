using UnityEditor;
using UnityEngine;

namespace PylonsSdk.RecipeBuilder.Editor
{
    [CustomEditor(typeof(GoExpression))]
    [CanEditMultipleObjects]
    public class GoExpressionEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            HandleGoExpression(serializedObject);
            serializedObject.ApplyModifiedProperties();
        }

        public static void HandleGoExpression(SerializedObject so)
        {
            var expression = so.FindProperty("Expression");

            using (var vertical = new EditorGUILayout.VerticalScope())
            {
                EditorGUILayout.PropertyField(so.FindProperty("Name"));
                EditorGUILayout.PropertyField(so.FindProperty("Description"));
                EditorGUILayout.LabelField("Expression", EditorStyles.miniLabel);
                expression.stringValue = EditorGUILayout.TextArea(expression.stringValue);
            }
        }
    }
}