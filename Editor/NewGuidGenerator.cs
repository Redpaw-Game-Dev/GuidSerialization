using UnityEditor;
using UnityEngine;

namespace LazyRedpaw.GuidSerialization
{
    public static class NewGuidGenerator
    {
        [MenuItem("GUID/Get New GUID Parts")]
        public static void GetNewGuidParts()
        {
            SerializableGuid guid = SerializableGuid.NewGuid();
            EditorGUIUtility.systemCopyBuffer = guid.ToString();
            Debug.Log($"New GUID parts copied to clipboard: {guid.ToString()}");
        }
        
    }
}