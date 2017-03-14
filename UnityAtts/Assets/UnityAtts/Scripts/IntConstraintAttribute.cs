using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityAtts
{
    public abstract class IntConstraintAttribute : PropertyAttribute
    {
        public override bool Match(object obj)
        {
            return obj is int;
        }

        public abstract int ConstrainValue(int value);
    }

}