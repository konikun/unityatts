using UnityEngine;
using UnityEditor;
using System.Collections;

namespace UnityAtts
{
    [CustomPropertyDrawer(typeof(MinAttribute))]
    public class MinAttributeDrawer : IntConstraintDrawer
    {
        
    }

}