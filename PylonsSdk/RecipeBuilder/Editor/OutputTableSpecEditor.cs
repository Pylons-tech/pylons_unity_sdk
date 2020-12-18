using PylonsSdk.Editor;
using UnityEditor;
using UnityEngine;

namespace PylonsSdk.RecipeBuilder.Editor
{
    [CustomEditor(typeof(OutputTableSpec))]
    [CanEditMultipleObjects]
    public class OutputTableSpecEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            HandleOutput(serializedObject);
            serializedObject.ApplyModifiedProperties();
        }

        public static void HandleOutput(SerializedObject so)
        {
            void coinOutputHeader(SerializedProperty arr)
            {
                EditorGUILayout.LabelField(new GUIContent(arr.name, "List of all coins that this table entry generates when run."));
                if (arr.arraySize == 0 || arr.GetArrayElementAtIndex(0).objectReferenceValue != null)
                {
                    arr.InsertArrayElementAtIndex(0);
                    arr.GetArrayElementAtIndex(0).objectReferenceValue = null;
                }
                EditorGUILayout.PropertyField(arr.GetArrayElementAtIndex(0), new GUIContent());
            }
            void itemOutputHeader(SerializedProperty arr)
            {
                EditorGUILayout.LabelField(new GUIContent(arr.name, "List of all items that this table entry generates when run."));
                if (arr.arraySize == 0 || arr.GetArrayElementAtIndex(0).objectReferenceValue != null)
                {
                    arr.InsertArrayElementAtIndex(0);
                    arr.GetArrayElementAtIndex(0).objectReferenceValue = null;
                }
                EditorGUILayout.PropertyField(arr.GetArrayElementAtIndex(0), new GUIContent());
            }
            bool coinOutputChild(SerializedProperty childProperty)
            {
                if (childProperty.objectReferenceInstanceIDValue != 0)
                {
                    using (var pane = new EditorGUILayout.VerticalScope("box"))
                    {
                        var childObject = new SerializedObject(childProperty.objectReferenceValue);
                        using (var miniPropField = new EditorGUILayout.VerticalScope())
                            EditorGUILayout.PropertyField(childProperty, new GUIContent());
                        CoinOutputSpecEditor.HandleCoinOutput(childObject);
                        if (childProperty.objectReferenceInstanceIDValue == 0) return false;
                        else
                        {
                            so.ApplyModifiedProperties();
                            return true;
                        }
                    }
                }
                else return true;
            }
            bool itemOutputChild(SerializedProperty childProperty)
            {
                if (childProperty.objectReferenceInstanceIDValue != 0)
                {
                    using (var pane = new EditorGUILayout.VerticalScope("box"))
                    {
                        var childObject = new SerializedObject(childProperty.objectReferenceValue);
                        using (var miniPropField = new EditorGUILayout.VerticalScope())
                            EditorGUILayout.PropertyField(childProperty, new GUIContent());
                        ItemOutputSpecEditor.HandleItemOutput(childObject);
                        if (childProperty.objectReferenceInstanceIDValue == 0) return false;
                        else
                        {
                            so.ApplyModifiedProperties();
                            return true;
                        }
                    }
                }
                else return true;
            }

            using (var vertical = new EditorGUILayout.VerticalScope())
            {
                EditorGUILayout.PropertyField(so.FindProperty("Name"));
                EditorGUILayout.PropertyField(so.FindProperty("Description"));
                EditorGUILayout.PropertyField(so.FindProperty("Weight"));
                EditorGUILayout.Separator();
                Helpers.ArrayFields(so.FindProperty("CoinOutputs"), coinOutputHeader, coinOutputChild);
                Helpers.ArrayFields(so.FindProperty("ItemOutputs"), itemOutputHeader, itemOutputChild);
            }
        }
    }
}