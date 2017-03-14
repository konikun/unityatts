using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UnityAtts
{
    [CustomPropertyDrawer(typeof(MaxAttribute))]
    public class MaxAttributeDrawer : IntConstraintDrawer
    {
        
    }

}