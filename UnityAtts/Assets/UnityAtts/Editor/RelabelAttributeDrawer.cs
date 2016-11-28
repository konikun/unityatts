using UnityEngine;
using UnityEditor;
using System.Collections;

namespace UnityAtts
{
    [CustomPropertyDrawer(typeof(RelabelAttribute))]
    public class RelabelAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            label.text = (attribute as RelabelAttribute).NewLabel;
            EditorGUI.PropertyField(position, property, label, true);
        }
    }
}