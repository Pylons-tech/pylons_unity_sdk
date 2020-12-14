using UnityEditor;
using UnityEngine;

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
            using (var refLists = new GUILayout.VerticalScope())
            {
                using (var coinInputScope = new GUILayout.VerticalScope())
                {
                    using (var header = new GUILayout.HorizontalScope())
                    {
                        EditorGUILayout.LabelField(new GUIContent(coinInputs.name, "List of all coins that this recipe consumes when run."));
                        if (GUILayout.Button(new GUIContent("+", "Add a new coin input to the recipe"))) coinInputs.InsertArrayElementAtIndex(0);
                    }
                    for (int i = 0; i < coinInputs.arraySize; i++)
                    {
                        if (EditorGUILayout.PropertyField(coinInputs.GetArrayElementAtIndex(i), GUIContent.none, true) && coinInputs.GetArrayElementAtIndex(i) == null)
                            coinInputs.DeleteArrayElementAtIndex(i);
                    }
                }
                EditorGUILayout.PropertyField(itemInputs);
                EditorGUILayout.PropertyField(outputs);
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}