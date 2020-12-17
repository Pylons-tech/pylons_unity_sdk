using PylonsSdk.Editor;
using UnityEditor;
using UnityEngine;

namespace PylonsSdk.RecipeBuilder.Editor
{
    [CustomEditor(typeof(ItemInputSpec))]
    [CanEditMultipleObjects]
    public class ItemInputSpecEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            HandleItemInput(serializedObject);
            serializedObject.ApplyModifiedProperties();
        }

        public static void HandleItemInput(SerializedObject so)
        {
            var doubles = so.FindProperty("Doubles");
            var longs = so.FindProperty("Longs");
            var strings = so.FindProperty("Strings");
            void doubleHeader(SerializedProperty arr)
            {
                EditorGUILayout.LabelField(new GUIContent(arr.name, "Double constraints on potential item inputs for a recipe"));
                if (GUILayout.Button(new GUIContent("+", "Add a new double input parameter constraint"))) Helpers.AddEmbedded<DoubleInputParamSpec>(doubles);
            }
            void longHeader(SerializedProperty arr)
            {
                EditorGUILayout.LabelField(new GUIContent(arr.name, "Long constraints on potential item inputs for a recipe"));
                if (GUILayout.Button(new GUIContent("+", "Add a new long input parameter constraint"))) Helpers.AddEmbedded<LongInputParamSpec>(longs);
            }
            void stringHeader(SerializedProperty arr)
            {
                EditorGUILayout.LabelField(new GUIContent(arr.name, "String constraints on potential item inputs for a recipe"));
                if (GUILayout.Button(new GUIContent("+", "Add a new string input parameter constraint"))) Helpers.AddEmbedded<StringInputParamSpec>(strings);
            }
            bool numChild(SerializedProperty childProperty)
            {
                if (childProperty.objectReferenceInstanceIDValue != 0)
                {
                    using (var pane = new EditorGUILayout.VerticalScope("box"))
                    {
                        var childObject = new SerializedObject(childProperty.objectReferenceValue);
                        EditorGUILayout.PropertyField(childObject.FindProperty("MinValue"));
                        EditorGUILayout.PropertyField(childObject.FindProperty("MaxValue"));
                        EditorGUILayout.PropertyField(childObject.FindProperty("Key"));
                        if (GUILayout.Button(new GUIContent("-", "Remove this input parameter constraint")))
                        {
                            AssetDatabase.RemoveObjectFromAsset(childObject.targetObject);
                            return false;
                        }
                        else
                        {
                            childObject.ApplyModifiedProperties();
                            return true;
                        }
                    }
                }
                else return false;
            }
            bool strChild(SerializedProperty childProperty)
            {
                if (childProperty.objectReferenceInstanceIDValue != 0)
                {
                    using (var pane = new EditorGUILayout.VerticalScope("box"))
                    {
                        var childObject = new SerializedObject(childProperty.objectReferenceValue);
                        EditorGUILayout.PropertyField(childObject.FindProperty("Value"));
                        EditorGUILayout.PropertyField(childObject.FindProperty("Key"));
                        if (GUILayout.Button(new GUIContent("-", "Remove this input parameter constraint")))
                        {
                            AssetDatabase.RemoveObjectFromAsset(childObject.targetObject);
                            return false;
                        }
                        else
                        {
                            childObject.ApplyModifiedProperties();
                            return true;
                        }
                    }
                }
                else return false;
            }

            Helpers.ArrayFields(doubles, doubleHeader, numChild);
            Helpers.ArrayFields(longs, longHeader, numChild);
            Helpers.ArrayFields(strings, stringHeader, strChild);
        }
    }
}