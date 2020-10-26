using UnityEditor;

namespace PylonsSdk.ProfileTools.Editor
{
    [CustomEditor(typeof(ProfileDef))]
    public class ProfileDefEditor : UnityEditor.Editor
    {
        SerializedProperty privateKey;
        SerializedProperty publicKey;
        SerializedProperty address;
        SerializedProperty profileName;

        void OnEnable()
        {
            privateKey = serializedObject.FindProperty("PrivateKey");
            publicKey = serializedObject.FindProperty("PublicKey");
            address = serializedObject.FindProperty("Address");
            profileName = serializedObject.FindProperty("Name");
        }

        void OnGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(privateKey);
            EditorGUILayout.PropertyField(publicKey);
            EditorGUILayout.PropertyField(address);
            EditorGUILayout.PropertyField(profileName);
            serializedObject.ApplyModifiedProperties();
        }
    }
}