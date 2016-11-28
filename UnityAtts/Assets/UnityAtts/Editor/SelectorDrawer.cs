using UnityEngine;
using UnityEditor;
using System.Collections;
using System;
using System.Linq;

namespace UnityAtts.EditorTools
{
    public abstract class SelectorDrawer : PropertyDrawer
    {
        private string[] categoryNames;
        private int[] categoryIds;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.Integer)
                return;

            if (property.hasMultipleDifferentValues)
                return;

            if (TryFetchCategoryNames())
            {
                int oldId = property.intValue;
                int oldSelected = System.Array.FindIndex(categoryIds, x => x == oldId);
                int selected = EditorGUI.Popup(position, label.text, oldSelected, categoryNames);
                if (selected >= 0)
                {
                    property.intValue = categoryIds[selected];
                }
            }
            else
            {
                GUI.Label(position, GetEmptyOrNullMessage(), "box");
            }
        }

        protected virtual string GetEmptyOrNullMessage()
        {
            return "There are no elements created.";
        }

        protected virtual bool TryFetchCategoryNames()
        {
            if (IsEmptyOrNull())
            {
                return false;
            }

            categoryNames = FetchCategoryNames();

            categoryIds = FetchCategoryIDs();

            return true;
        }

        protected abstract bool IsEmptyOrNull();

        protected abstract string[] FetchCategoryNames();

        protected abstract int[] FetchCategoryIDs();
    } 
}
