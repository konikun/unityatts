using UnityEngine;
using System.Collections;

namespace UnityAtts
{
    public class InstantiateButtonAttribute : PropertyAttribute
    {
        public bool HideButton { get; set; }

        public override bool Match(object obj)
        {
            return obj is GameObject;
        }
    }

}