using UnityEngine;
using System.Collections;

namespace UnityAtts
{
    public class MinAttribute : PropertyAttribute
    {
        public int MinValue { get; set; }

        public override bool Match(object obj)
        {
            return obj is int;
        }

        public MinAttribute(int min)
        {
            MinValue = min;
        }
    }

}