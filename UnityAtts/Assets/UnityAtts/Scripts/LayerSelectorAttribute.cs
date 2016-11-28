using UnityEngine;

namespace UnityAtts
{
    public class LayerSelectorAttribute : PropertyAttribute
    {
        public override bool Match(object obj)
        {
            return obj is int;
        }
    }

}