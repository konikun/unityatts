using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityAtts
{
    public class FalseIfAttribute : PropertyAttribute
    {
        public string OtherBool { get; set; }

        public FalseIfAttribute(string otherBool)
        {
            OtherBool = otherBool;
        }

        public override bool Match(object obj)
        {
            return obj is bool;
        }
    }

}