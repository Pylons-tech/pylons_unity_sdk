using PylonsSdk.Editor;
using UnityEditor;
using UnityEngine;

namespace PylonsSdk.RecipeBuilder.Editor
{
    [CustomEditor(typeof(ItemOutputSpec))]
    [CanEditMultipleObjects]
    public class ItemOutputSpecEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            HandleItemOutput(serializedObject);
            serializedObject.ApplyModifiedProperties();
        }

        public static void HandleItemOutput(SerializedObject so)
        {
            EditorGUILayout.PropertyField(so.FindProperty("TransferFee"));
            var doubles = so.FindProperty("Doubles");
            var longs = so.FindProperty("Longs");
            var strings = so.FindProperty("Strings");

            void wrHeader<T>(SerializedProperty arr) where T : ScriptableObject
            {
                EditorGUILayout.LabelField(new GUIContent(arr.name, "Weight ranges for values to be emitted"));
                if (GUILayout.Button(new GUIContent("+", "Add a new weight range"))) Helpers.AddEmbedded<T>(arr);
            }
            bool wrChild(SerializedProperty childProperty)
            {
                if (childProperty.objectReferenceValue == null) return false;
                var childObject = new SerializedObject(childProperty.objectReferenceValue);
                using (var horizontal = new EditorGUILayout.HorizontalScope())
                {
                    using (var vertical = new EditorGUILayout.VerticalScope())
                    {
                        EditorGUILayout.LabelField("Min", EditorStyles.miniLabel);
                        EditorGUILayout.PropertyField(childObject.FindProperty("Lower"), new GUIContent(""));
                    }
                    using (var vertical = new EditorGUILayout.VerticalScope())
                    {
                        EditorGUILayout.LabelField("Max", EditorStyles.miniLabel);
                        EditorGUILayout.PropertyField(childObject.FindProperty("Upper"), new GUIContent(""));
                    }
                }
                if (GUILayout.Button(new GUIContent("-", "Remove this range")))
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

            void doubleHeader(SerializedProperty arr)
            {
                EditorGUILayout.LabelField(new GUIContent(arr.name, "Potential double values on the item to be emitted"));
                if (GUILayout.Button(new GUIContent("+", "Add a new double value"))) Helpers.AddEmbedded<DoubleOutputParamSpec>(doubles);
            }
            void longHeader(SerializedProperty arr)
            {
                EditorGUILayout.LabelField(new GUIContent(arr.name, "Potential long values on the item to be emitted"));
                if (GUILayout.Button(new GUIContent("+", "Add a new long value"))) Helpers.AddEmbedded<LongOutputParamSpec>(longs);
            }
            void stringHeader(SerializedProperty arr)
            {
                EditorGUILayout.LabelField(new GUIContent(arr.name, "Potential string values on the item to be emitted"));
                if (GUILayout.Button(new GUIContent("+", "Add a new string value"))) Helpers.AddEmbedded<StringOutputParamSpec>(strings);
            }
            bool numChild<T>(SerializedProperty childProperty) where T : ScriptableObject
            {
                if (childProperty.objectReferenceInstanceIDValue != 0)
                {
                    using (var pane = new EditorGUILayout.VerticalScope("box"))
                    {
                        var childObject = new SerializedObject(childProperty.objectReferenceValue);
                        EditorGUILayout.PropertyField(childObject.FindProperty("Rate"));
                        EditorGUILayout.PropertyField(childObject.FindProperty("Key"));
                        Helpers.ArrayFields(childObject.FindProperty("WeightRanges"), wrHeader<T>, wrChild);

                        var expr = childObject.FindProperty("Expression");
                        EditorGUILayout.PropertyField(expr);
                        if (expr.objectReferenceValue != null) GoExpressionEditor.HandleGoExpression(new SerializedObject(expr.objectReferenceValue));

                        if (GUILayout.Button(new GUIContent("-", "Remove this value")))
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
                        EditorGUILayout.PropertyField(childObject.FindProperty("Rate"));
                        EditorGUILayout.PropertyField(childObject.FindProperty("Key"));
                        EditorGUILayout.PropertyField(childObject.FindProperty("Value"));

                        var expr = childObject.FindProperty("Expression");
                        EditorGUILayout.PropertyField(expr);
                        if (expr.objectReferenceValue != null) GoExpressionEditor.HandleGoExpression(new SerializedObject(expr.objectReferenceValue));

                        if (GUILayout.Button(new GUIContent("-", "Remove this value")))
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

            Helpers.ArrayFields(doubles, doubleHeader, numChild<DoubleWeightRangeSpec>);
            Helpers.ArrayFields(longs, longHeader, numChild<LongWeightRangeSpec>);
            Helpers.ArrayFields(strings, stringHeader, strChild);
        }
    }
}