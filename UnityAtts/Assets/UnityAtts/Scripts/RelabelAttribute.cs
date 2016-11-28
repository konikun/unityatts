using UnityEngine;
using System.Collections;

namespace UnityAtts
{
    public class RelabelAttribute : PropertyAttribute
    {
        public string NewLabel { get; set; }

        public RelabelAttribute(string label)
        {
            NewLabel = label;
        }

        public override bool Match(object obj)
        {
            return true;
        }
    }
}