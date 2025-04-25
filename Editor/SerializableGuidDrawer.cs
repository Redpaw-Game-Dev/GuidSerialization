using System;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using static LazyRedpaw.GuidSerialization.SerializableGuidEditorHelper;

namespace LazyRedpaw.GuidSerialization
{
    [CustomPropertyDrawer(typeof(SerializableGuid))]
    public class SerializableGuidDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            if (GetGuidParts(property).All(x => x != null))
            {
                GUI.enabled = false;
                EditorGUI.TextField(position, label,
                    $"{BuildGuidHexString(GetGuidParts(property))}");
                GUI.enabled = true;
            }
            else
            {
                EditorGUI.SelectableLabel(position, "GUID Not Initialized");
            }

            bool hasClicked = Event.current.type == EventType.MouseUp && Event.current.button == 1;
            if (hasClicked && position.Contains(Event.current.mousePosition))
            {
                ShowContextMenu(property);
                Event.current.Use();
            }

            EditorGUI.EndProperty();
        }

        private void ShowContextMenu(SerializedProperty property)
        {
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("Copy GUID parts"), false, () => CopyGuidParts(property));
            menu.AddItem(new GUIContent("Copy GUID"), false, () => CopyGuid(property));
            menu.AddItem(new GUIContent("Reset GUID"), false, () => ResetGuid(property));
            menu.AddItem(new GUIContent("Regenerate GUID"), false, () => RegenerateGuid(property));
            menu.ShowAsContext();
        }

        private void CopyGuidParts(SerializedProperty property)
        {
            SerializedProperty[] guidParts = GetGuidParts(property);
            if (guidParts.Any(x => x == null)) return;
            string guid = BuildGuidString(GetGuidParts(property));
            EditorGUIUtility.systemCopyBuffer = guid;
            Debug.Log($"GUID parts copied to clipboard: {guid}");
        }
        
        private void CopyGuid(SerializedProperty property)
        {
            SerializedProperty[] guidParts = GetGuidParts(property);
            if (guidParts.Any(x => x == null)) return;
            string guid = BuildGuidHexString(GetGuidParts(property));
            EditorGUIUtility.systemCopyBuffer = guid;
            Debug.Log($"GUID copied to clipboard: {guid}");
        }

        private void ResetGuid(SerializedProperty property)
        {
            const string warning = "Are you sure you want to reset the GUID?";
            if (!EditorUtility.DisplayDialog("Reset GUID", warning, "Yes", "No")) return;

            foreach (var part in GetGuidParts(property))
            {
                part.uintValue = 0;
            }

            property.serializedObject.ApplyModifiedProperties();
            Debug.Log("GUID has been reset.");
        }

        private void RegenerateGuid(SerializedProperty property)
        {
            const string warning = "Are you sure you want to regenerate the GUID?";
            if (!EditorUtility.DisplayDialog("Reset GUID", warning, "Yes", "No")) return;

            byte[] bytes = Guid.NewGuid().ToByteArray();
            SerializedProperty[] guidParts = GetGuidParts(property);

            for (int i = 0; i < GuidParts.Length; i++)
            {
                guidParts[i].uintValue = BitConverter.ToUInt32(bytes, i * 4);
            }

            property.serializedObject.ApplyModifiedProperties();
            Debug.Log("GUID has been regenerated.");
        }
    }
}
