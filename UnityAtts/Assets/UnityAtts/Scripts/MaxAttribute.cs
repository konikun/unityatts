using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityAtts
{
    /// <summary>
    /// Constrains an int field to a maximum value.
    /// </summary>
    public class MaxAttribute : IntConstraintAttribute
    {
        public int MaxValue { get; set; }

        public override int ConstrainValue(int value)
        {
            return Mathf.Min(value, MaxValue);
        }

        public MaxAttribute(int max)
        {
            MaxValue = max;
        }
    }

}