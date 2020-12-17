using System;
using UnityEditor;
using UnityEngine;
using PylonsSdk.Editor;

namespace PylonsSdk.RecipeBuilder.Editor
{
    [CustomEditor(typeof(RecipeSpec))]
    [CanEditMultipleObjects]
    public class RecipeSpecEditor : UnityEditor.Editor
    {
        SerializedProperty cookbook;
        SerializedProperty recipeName;
        SerializedProperty description;
        SerializedProperty enabled;
        SerializedProperty executionDelay;
        SerializedProperty coinInputs;
        SerializedProperty itemInputs;
        SerializedProperty outputs;

        void OnEnable()
        {
            cookbook = serializedObject.FindProperty("Cookbook");
            recipeName = serializedObject.FindProperty("Name");
            description = serializedObject.FindProperty("Description");
            enabled = serializedObject.FindProperty("Enabled");
            executionDelay = serializedObject.FindProperty("ExecutionDelay");
            coinInputs = serializedObject.FindProperty("CoinInputs");
            itemInputs = serializedObject.FindProperty("ItemInputs");
            outputs = serializedObject.FindProperty("Outputs");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            using (var header = new GUILayout.VerticalScope())
            {
                EditorGUILayout.LabelField(new GUIContent("Recipe Builder (experimental)", "Recipe Builder is still undergoing active development. Correctness of generated recipes is not yet guaranteed."));
                EditorGUILayout.Separator();
            }
            using (var recipeProps = new GUILayout.VerticalScope()) {
                EditorGUILayout.PropertyField(cookbook, new GUIContent(cookbook.name,
                    "CookbookSpec for the Recipe Builder to use to manage the cookbook that this recipe is associated with."));
                EditorGUILayout.PropertyField(recipeName, new GUIContent(recipeName.name,
                    "The name of this recipe. Alphanumeric characters, hyphens, and underscores only."));
                EditorGUILayout.PropertyField(description, new GUIContent(description.name,
                    "A succinct human-readable description of what this recipe is and what its intended use is."));
                EditorGUILayout.PropertyField(enabled, new GUIContent(enabled.name, "" +
                    "Should it be possible to execute this recipe at present?"));
                EditorGUILayout.PropertyField(executionDelay, new GUIContent(executionDelay.name,
                    "The amount of time, in seconds, that it should take to execute this recipe. " +
                    "This is an approximate value; the smallest possible time interval not less than" +
                    "this will be used to set the recipe's on-chain execution delay value."));
            }
            using (var inputs = new GUILayout.VerticalScope("box"))
            {
                EditorGUILayout.LabelField("Inputs");
                EditorGUILayout.Separator();
                void coinInputHeader (SerializedProperty arr)
                {
                    EditorGUILayout.LabelField(new GUIContent(arr.name, "List of all coins that this recipe consumes when run."));
                    if (arr.arraySize == 0 || arr.GetArrayElementAtIndex(0).objectReferenceValue != null)
                    {
                        arr.InsertArrayElementAtIndex(0);
                        arr.GetArrayElementAtIndex(0).objectReferenceValue = null;
                    }
                    EditorGUILayout.PropertyField(arr.GetArrayElementAtIndex(0), new GUIContent());
                }
                bool coinInputChild (SerializedProperty childProperty)
                {
                    if (childProperty.objectReferenceInstanceIDValue != 0)
                    {
                        using (var pane = new EditorGUILayout.VerticalScope("box"))
                        {
                            var so = new SerializedObject(childProperty.objectReferenceValue);
                            using (var miniPropField = new EditorGUILayout.VerticalScope())
                                EditorGUILayout.PropertyField(childProperty, new GUIContent());
                            CoinInputSpecEditor.HandleCoinInput(so);
                            if (GUILayout.Button("-")) return false;
                            else
                            {
                                so.ApplyModifiedProperties();
                                return true;
                            }
                        }
                    }
                    else return false;
                }
                void itemInputHeader(SerializedProperty arr)
                {
                    EditorGUILayout.LabelField(new GUIContent(arr.name, "List of all items that this recipe consumes when run."));
                    if (arr.arraySize == 0 || arr.GetArrayElementAtIndex(0).objectReferenceValue != null)
                    {
                        arr.InsertArrayElementAtIndex(0);
                        arr.GetArrayElementAtIndex(0).objectReferenceValue = null;
                    }
                    EditorGUILayout.PropertyField(arr.GetArrayElementAtIndex(0), new GUIContent());
                }
                bool itemInputChild(SerializedProperty childProperty)
                {
                    if (childProperty.objectReferenceInstanceIDValue != 0)
                    {
                        using (var pane = new EditorGUILayout.VerticalScope("box"))
                        {
                            var so = new SerializedObject(childProperty.objectReferenceValue);
                            using (var miniPropField = new EditorGUILayout.VerticalScope())
                                EditorGUILayout.PropertyField(childProperty, new GUIContent());
                            ItemInputSpecEditor.HandleItemInput(so);
                            if (GUILayout.Button("-")) return false;
                            else
                            {
                                so.ApplyModifiedProperties();
                                return true;
                            }
                        }
                    }
                    else return false;
                }
                Helpers.ArrayFields(coinInputs, coinInputHeader, coinInputChild);
                Helpers.ArrayFields(itemInputs, itemInputHeader, itemInputChild);
            }

            using (var outputScope = new GUILayout.VerticalScope("box"))
            {
                EditorGUILayout.LabelField("Outputs");
                EditorGUILayout.Separator();

                void outputHeader(SerializedProperty arr)
                {
                    EditorGUILayout.LabelField(new GUIContent(arr.name, "List of all output tables that this recipe can call."));
                    if (arr.arraySize == 0 || arr.GetArrayElementAtIndex(0).objectReferenceValue != null)
                    {
                        arr.InsertArrayElementAtIndex(0);
                        arr.GetArrayElementAtIndex(0).objectReferenceValue = null;
                    }
                    EditorGUILayout.PropertyField(arr.GetArrayElementAtIndex(0), new GUIContent());
                }

                bool outputChild(SerializedProperty childProperty)
                {
                    if (childProperty.objectReferenceInstanceIDValue != 0)
                    {
                        using (var pane = new EditorGUILayout.VerticalScope("box"))
                        {
                            var so = new SerializedObject(childProperty.objectReferenceValue);
                            using (var miniPropField = new EditorGUILayout.VerticalScope())
                                EditorGUILayout.PropertyField(childProperty, new GUIContent());
                            OutputTableSpecEditor.HandleOutput(so);
                            if (GUILayout.Button("-")) return false;
                            else
                            {
                                so.ApplyModifiedProperties();
                                return true;
                            }
                        }
                    }
                    else return false;
                }
                Helpers.ArrayFields(outputs, outputHeader, outputChild);
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}