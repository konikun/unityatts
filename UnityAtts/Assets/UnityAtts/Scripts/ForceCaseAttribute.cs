using UnityEngine;
using System.Collections;

namespace SmoothieBlast.Util
{
    public class ForceCaseAttribute : PropertyAttribute
    {
        public bool LowerCase { get; set; }

        public override bool Match(object obj)
        {
            return obj is string;
        }
    }

}