using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UnityAtts
{
    [CustomPropertyDrawer(typeof(FalseIfAttribute))]
    public class FalseIfDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.Boolean)
                return;

            var fieldName = ((FalseIfAttribute)attribute).OtherBool;
            var otherProperty = property.serializedObject.FindProperty(fieldName);
            if (otherProperty != null)
            {
                var otherValue = otherProperty.boolValue;
                if (otherValue)
                {
                    property.boolValue = false;
                }
            }else
            {
                Debug.LogWarningFormat("Property is trying to access unexisting attribute: {0}", fieldName);
            }

            EditorGUI.PropertyField(position, property, label);
        }
    }

}