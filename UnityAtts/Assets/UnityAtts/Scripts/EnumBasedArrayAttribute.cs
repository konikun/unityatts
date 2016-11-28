using UnityEngine;
using System.Collections;

namespace UnityAtts
{
    public class EnumBasedArrayAttribute : PropertyAttribute
    {
        public System.Type EnumType { get; set; }

        public EnumBasedArrayAttribute(System.Type enumType)
        {
            EnumType = enumType;
        }

        public override bool Match(object obj)
        {
            return base.Match(obj);
        }
    }

}