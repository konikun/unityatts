using UnityEngine;
using System.Collections;
using UnityEditor;

namespace UnityAtts
{
    [CustomPropertyDrawer(typeof(ForceCaseAttribute))]
    public class ForceCaseDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.String)
            {
                Debug.Log("property is not a string" + property.propertyType);
                return;
            }
            EditorGUI.BeginProperty(position, label, property);
            var value = EditorGUI.TextField(position, label, property.stringValue);
            if (!string.IsNullOrEmpty(value))
            {
                property.stringValue = value.ToLower();
            }else
            {
                property.stringValue = value;
            }
            EditorGUI.EndProperty();
        }
    }

}