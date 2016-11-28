using UnityEngine;
using UnityEditor;
using System.Collections;
using Manifesto.Util;

namespace Manifesto.EditorTools
{
    [CustomPropertyDrawer(typeof(LayerAttribute))]
    public class LayerAttributeDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            property.intValue = EditorGUI.LayerField(position, label, property.intValue);
            EditorGUI.EndProperty();
        }
    }

}