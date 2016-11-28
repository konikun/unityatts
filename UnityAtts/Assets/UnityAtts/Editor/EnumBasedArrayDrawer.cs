using UnityEngine;
using System.Collections;
using UnityEditor;
using System;

namespace UnityAtts
{
    [CustomPropertyDrawer(typeof(EnumBasedArrayAttribute))]
    public class EnumBasedArrayEditor : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, property.isExpanded);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var displayName = property.displayName;
            if (!displayName.StartsWith("Element"))
            {
                return;
            }
            var split = displayName.Split(' ');
            var index = Convert.ToInt32(split[1]);

            var enumType = (attribute as EnumBasedArrayAttribute).EnumType;
            var enumNames = Enum.GetNames(enumType);
            if (index < enumNames.Length)
            {
                var enumValueName = enumNames[index];
                label = new GUIContent(enumValueName);
            }
            EditorGUI.PropertyField(position, property, label, true);
        }
    }

}