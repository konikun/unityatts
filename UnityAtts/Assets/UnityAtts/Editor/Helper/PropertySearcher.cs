using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace UnityAtts.Internal
{
    public static class PropertySearcher
    {
        public static FieldInfo FindFieldInPath(FieldInfo propertyField, string pathToField)
        {
            var fieldType = propertyField.FieldType;
            if (typeof(IList).IsAssignableFrom(fieldType))
            {
                return FindFieldInList(propertyField, pathToField);
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

        public static FieldInfo FindFieldInList(FieldInfo propertyField, string pathToField)
        {
            var actualType = propertyField.FieldType.GetGenericArguments()[0];
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
    }

}