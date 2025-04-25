using System.Text;
using UnityEditor;

namespace LazyRedpaw.GuidSerialization
{
    public static class SerializableGuidEditorHelper
    {
        public static readonly string[] GuidParts = { "Part1", "Part2", "Part3", "Part4" };
        
        public static SerializedProperty[] GetGuidParts(SerializedProperty property)
        {
            var values = new SerializedProperty[GuidParts.Length];
            for (int i = 0; i < GuidParts.Length; i++) {
                values[i] = property.FindPropertyRelative(GuidParts[i]);
            }
            return values;
        }
        
        public static string BuildGuidHexString(SerializedProperty[] guidParts)
        {
            return new StringBuilder()
                .AppendFormat("{0:X8}", guidParts[0].uintValue)
                .AppendFormat("{0:X8}", guidParts[1].uintValue)
                .AppendFormat("{0:X8}", guidParts[2].uintValue)
                .AppendFormat("{0:X8}", guidParts[3].uintValue)
                .ToString();
        }
        
        public static string BuildGuidString(SerializedProperty[] guidParts)
        {
            return new StringBuilder()
                .Append($"{guidParts[0].uintValue} ")
                .Append($"{guidParts[1].uintValue} ")
                .Append($"{guidParts[2].uintValue} ")
                .Append(guidParts[3].uintValue)
                .ToString();
        }
    }
}