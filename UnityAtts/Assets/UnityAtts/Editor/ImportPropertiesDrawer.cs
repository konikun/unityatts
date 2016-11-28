using UnityEngine;
using System.Collections;
using UnityEditor;

namespace Manifesto.EditorTools
{
    [CustomPropertyDrawer(typeof(ImportComponentPropertiesAttribute))]
    public class ImportComponentPropertiesDrawer : PropertyDrawer
    {
        private float defHeight = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

        private ImportComponentPropertiesAttribute importData;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            importData = attribute as ImportComponentPropertiesAttribute;

            var propertyCount = importData.Properties != null ? importData.Properties.Length : 0;

            return (defHeight * (1 + propertyCount)) + EditorGUIUtility.standardVerticalSpacing;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            
            // first draw the actual object
            var objRect = new Rect(position.x, position.y, position.width, defHeight);
            EditorGUI.PropertyField(objRect, property);

            // then draw each of the properties of the component(s)
            var gameObj = property.objectReferenceValue as GameObject;
            if (importData.MultipleTypes)
            {
                DrawMultipleComponentProperties(position, objRect, gameObj);
            }
            else
            {
                DrawComponentProperties(position, objRect, gameObj);
            }

            EditorGUI.EndProperty();
        }

        private void DrawComponentProperties(Rect position, Rect objRect, GameObject gameObj)
        {
            var comp = gameObj.GetComponent(importData.ComponentType);
            var serializedComp = new SerializedObject(comp);
            foreach (var item in importData.Properties)
            {
                var rect = new Rect(position.x, objRect.y + objRect.height + EditorGUIUtility.standardVerticalSpacing, position.width, EditorGUIUtility.singleLineHeight);
                var prop = serializedComp.FindProperty(item);
                EditorGUI.PropertyField(rect, prop);
                objRect = rect;
            }
        }

        private void DrawMultipleComponentProperties(Rect position, Rect objRect, GameObject gameObj)
        {
            var componentTypes = importData.ComponentTypes;
            for (int i = 0; i < componentTypes.Length; i++)
            {
                var comp = gameObj.GetComponent(componentTypes[i]);
                // we create a SerializedObject to handle this component's properties
                var container = new SerializedObject(comp);
                var propertyPath = importData.Properties[i];

                var prop = GetProperty(ref container, propertyPath);

                var rect = new Rect(position.x, objRect.y + objRect.height + EditorGUIUtility.standardVerticalSpacing, position.width, EditorGUIUtility.singleLineHeight);
                EditorGUI.PropertyField(rect, prop);

                objRect = rect;
                container.ApplyModifiedProperties();
            }
        }

        private SerializedProperty GetProperty(ref SerializedObject obj, string path)
        {
            //Debug.Log(obj.targetObject.name + "Path is " + path);
            if (path.Contains("/"))
            {
                var pathParts = path.Split(new char[]{'/'}, 2);
                obj = new SerializedObject(obj.FindProperty(pathParts[0]).objectReferenceValue);
                return GetProperty(ref obj, pathParts[1]);
            }
            else
            {
                return obj.FindProperty(path);
            }
        }
    }
}