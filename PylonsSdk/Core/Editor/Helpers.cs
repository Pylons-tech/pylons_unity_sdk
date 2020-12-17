using System;
using UnityEditor;
using UnityEngine;

namespace PylonsSdk.Editor
{
    public static class Helpers
    {
        public static void ArrayFields(SerializedProperty arr, Action<SerializedProperty> headerFunc, Func<SerializedProperty, bool> childFunc)
        {
            using (var coinInputScope = new GUILayout.VerticalScope())
            {
                using (var header = new GUILayout.HorizontalScope()) headerFunc(arr);
                for (int i = 0; i < arr.arraySize; i++) if (!childFunc(arr.GetArrayElementAtIndex(i))) arr.DeleteArrayElementAtIndex(i);
            }
        }

        public static void AddEmbedded<T>(SerializedProperty property) where T : ScriptableObject
        {
            var obj = ScriptableObject.CreateInstance<T>();
            AssetDatabase.AddObjectToAsset(obj, AssetDatabase.GetAssetPath(property.serializedObject.targetObject));
            if (property.isArray)
            {
                property.InsertArrayElementAtIndex(0);
                property.GetArrayElementAtIndex(0).objectReferenceInstanceIDValue = obj.GetInstanceID();
            }
            else property.objectReferenceInstanceIDValue = obj.GetInstanceID();
        }
    }
}