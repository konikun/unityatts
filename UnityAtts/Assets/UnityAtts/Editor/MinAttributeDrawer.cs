using UnityEngine;
using UnityEditor;
using System.Collections;

namespace UnityAtts.EditorTools
{
    [CustomPropertyDrawer(typeof(MinAttribute))]
    public class MinAttributeDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.PropertyField(position, property, label);
            var min = attribute as MinAttribute;
            if (property.intValue < min.MinValue)
            {
                property.intValue = min.MinValue;
            }
            EditorGUI.EndProperty();
        }
    }

}