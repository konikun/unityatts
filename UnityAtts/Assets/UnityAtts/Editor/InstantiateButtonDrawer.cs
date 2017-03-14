using UnityEngine;
using UnityEditor;
using System.Collections;

namespace UnityAtts.EditorTools
{
    [CustomPropertyDrawer(typeof(InstantiateButtonAttribute))]
    public class InstantiateButtonDrawer : PropertyDrawer
    {
        private const float buttonWidth = 50f;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.hasMultipleDifferentValues ||
                property.propertyType != SerializedPropertyType.ObjectReference ||
                !property.objectReferenceValue)
            {
                DrawDefaultProperty(position, property, label);
                return;
            }

            var valueIsPrefab = PrefabUtility.GetPrefabType(property.objectReferenceValue) == PrefabType.Prefab || PrefabUtility.GetPrefabType(property.objectReferenceValue) == PrefabType.ModelPrefab;

            var targetObject = property.serializedObject.targetObject as MonoBehaviour;
            var prefabType = PrefabUtility.GetPrefabType(targetObject);
            if (prefabType == PrefabType.Prefab)
            {
                // dont draw buttons if we are inspecting the prefab
                DrawDefaultProperty(position, property, label);
            }
            else
            {
                var hasHideButton = (attribute as InstantiateButtonAttribute).HideButton && valueIsPrefab;

                var propPosition = position;
                propPosition.width -= buttonWidth;
                if (hasHideButton)
                {
                    propPosition.width -= buttonWidth;
                }

                EditorGUI.BeginProperty(propPosition, label, property);

                EditorGUI.PropertyField(propPosition, property, label, true);

                var buttonPos = position;
                buttonPos.x = propPosition.x + propPosition.width;
                buttonPos.width = buttonWidth;

                if (GUI.Button(buttonPos, "Show"))
                {
                    var objectPrefab = property.objectReferenceValue as GameObject;
                    var objectInstance = PrefabUtility.InstantiatePrefab(objectPrefab) as GameObject;
                    if (!valueIsPrefab)
                    {
                        objectInstance = Object.Instantiate(objectPrefab) as GameObject;
                    }
                    objectInstance.transform.SetParent(targetObject.transform, false);
                }
                if (hasHideButton)
                {
                    buttonPos.x = buttonPos.x + buttonPos.width;

                    if (GUI.Button(buttonPos, "Hide"))
                    {
                        var objectPrefab = property.objectReferenceValue as GameObject;
                        for (int i = targetObject.transform.childCount - 1; i >= 0; i--)
                        {
                            var child = targetObject.transform.GetChild(i);
                            if (PrefabUtility.GetPrefabParent(child.gameObject) == objectPrefab)
                            {
                                Object.DestroyImmediate(child.gameObject);
                            }
                        }
                    }
                }
                EditorGUI.EndProperty();
            }
        }

        private static void DrawDefaultProperty(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            EditorGUI.PropertyField(position, property, label, true);

            EditorGUI.EndProperty();
        }
    }

}