using UnityEngine;
using System.Collections;
using System;

namespace UnityAtts
{
    /// <summary>
    /// Constrains an int field to a minimum value.
    /// </summary>
    public class MinAttribute : IntConstraintAttribute
    {
        public int MinValue { get; set; }

        public MinAttribute(int min)
        {
            MinValue = min;
        }

        public override int ConstrainValue(int value)
        {
            return Mathf.Max(value, MinValue);
        }
    }

}