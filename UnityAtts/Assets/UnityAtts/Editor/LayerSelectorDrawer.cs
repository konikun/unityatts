using UnityEditor;
using UnityEngine;

namespace UnityAtts
{
    [CustomPropertyDrawer(typeof(LayerSelectorAttribute))]
    public class LayerSelectorDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.Integer)
                return;

            EditorGUI.BeginProperty(position, GUIContent.none, property);

            EditorGUI.BeginChangeCheck();

            var newValue = EditorGUI.LayerField(position, label, property.intValue);

            if (EditorGUI.EndChangeCheck())
                property.intValue = newValue;

            EditorGUI.EndProperty();
        }
    } 
}