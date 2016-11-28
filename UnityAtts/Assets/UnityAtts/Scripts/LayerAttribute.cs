using UnityEngine;
using System.Collections;

namespace Manifesto.Util
{
    public class LayerAttribute : PropertyAttribute
    {
        public override bool Match(object obj)
        {
            return obj is int;
        }
    }

}