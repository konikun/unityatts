using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Reflection;
using System.Linq;

namespace UnityAtts.EditorTools
{
    [CustomPropertyDrawer(typeof(BoxedAttribute))]
    public class BoxedAttributeDrawer : PropertyDrawer
    {
        private float? defaultHeight;
        private const float verticalPadding = 5f;
        private const float borderX = 12f;
        private static float headerHeight = EditorGUIUtility.singleLineHeight + 10f;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (!defaultHeight.HasValue)
            {
                defaultHeight = base.GetPropertyHeight(property, label);
            }
            if (property.isExpanded)
            {
                return TotalHeight(property, property.depth) + (property.CountInProperty() * EditorGUIUtility.standardVerticalSpacing)
                    + (verticalPadding * 2f);
            }
            else
            {
                return defaultHeight.Value + (verticalPadding * 2f);
            }
        }

        private FieldInfo FindField(string pathToField)
        {
            var fieldType = fieldInfo.FieldType;
            if (typeof(IList).IsAssignableFrom(fieldType))
            {
                return FindFieldInList(pathToField);
            }
            //Debug.LogFormat("Checking field: {0}" , pathToField);
            var pathLevels = pathToField.Split('.');
            for (int i = 1; i < pathLevels.Length; i++)
            {
                var subFields = fieldType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
                foreach (var item in subFields)
                {
                    if (item.Name == pathLevels[i])
                    {
                        if (i == pathLevels.Length - 1)
                        {
                            return item;
                        }
                        else
                        {// go deeper
                            fieldType = item.FieldType;
                            break;
                        }
                    }
                }
            }
            return null;
        }

        private FieldInfo FindFieldInList(string pathToField)
        {
            var actualType = fieldInfo.FieldType.GetGenericArguments()[0];
            var pathLevels = pathToField.Split('.').ToList();
            pathLevels.Remove("Array");
            var removeIndex = pathLevels.FindIndex(x => x.StartsWith("data["));
            pathLevels.RemoveAt(removeIndex);
            for (int i = 1; i < pathLevels.Count; i++)
            {
                var subFields = actualType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
                foreach (var item in subFields)
                {
                    if (item.Name == pathLevels[i])
                    {
                        if (i == pathLevels.Count - 1)
                        {
                            return item;
                        }
                        else
                        {// go deeper
                            actualType = item.FieldType;
                            break;
                        }
                    }
                }
            }
            return null;
        }

        private float TotalHeight(SerializedProperty property, int originalDepth)
        {
            var iterator = property.Copy();

            var originalHeight = base.GetPropertyHeight(property, new GUIContent(property.displayName));

            var endProp = property.GetEndProperty();

            while (iterator.NextVisible(true) && !SerializedProperty.EqualContents(iterator, endProp))
            {
                if (iterator.depth != originalDepth + 1)
                    continue;

                if (iterator.hasVisibleChildren && iterator.isExpanded)
                {
                    originalHeight += TotalHeight(iterator, iterator.depth);
                }
                else
                {
                    originalHeight += base.GetPropertyHeight(iterator, new GUIContent(iterator.displayName));
                    var fieldInfo = FindField(iterator.propertyPath);
                    if (fieldInfo != null)
                    {
                        var atts = fieldInfo.GetCustomAttributes(typeof(HeaderAttribute), false);
                        if (atts.Length > 0)
                        {// this field has a header attribute, so it occupies more space
                            originalHeight += headerHeight;
                        }
                        atts = fieldInfo.GetCustomAttributes(typeof(TextAreaAttribute), false);
                        if (atts.Length > 0)
                        {// this field has a text area attribute, so it occupies more space
                            originalHeight += EditorGUIUtility.singleLineHeight * 2f + EditorGUIUtility.standardVerticalSpacing * 3f;
                        }
                    }
                }
            }
            return originalHeight;
        }

        //private static int CountChildren(SerializedProperty property, int visibleCount, int originalDepth)
        //{
        //    var iterator = property.Copy();

        //    var endProp = property.GetEndProperty();

        //    while (iterator.NextVisible(true) && !SerializedProperty.EqualContents(iterator, endProp))
        //    {
        //        if (iterator.depth != originalDepth + 1)
        //            continue;

        //        if (iterator.hasVisibleChildren && iterator.isExpanded)
        //        {
        //            visibleCount += CountChildren(iterator, visibleCount, iterator.depth);
        //        }
        //        else
        //        {
        //            visibleCount++;
        //        }
        //    }
        //    return visibleCount;
        //}

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.Box(position, GUIContent.none);
            var adjustedRect = new Rect(position.x + borderX, position.y + verticalPadding, position.width - (borderX * 2f), position.height);
            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.PropertyField(adjustedRect, property, property.isExpanded);
            EditorGUI.EndProperty();
        }
    }

}