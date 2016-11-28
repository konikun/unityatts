using UnityEngine;

namespace UnityAtts
{
    public class EnumFlagAttribute : PropertyAttribute
    {
        public string EnumLabel;

        public EnumFlagAttribute(string name)
        {
            EnumLabel = name;
        }
    } 
}