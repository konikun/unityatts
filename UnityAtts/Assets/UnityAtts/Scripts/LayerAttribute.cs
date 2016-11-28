﻿using UnityEngine;
using System.Collections;

namespace UnityAtts
{
    public class LayerAttribute : PropertyAttribute
    {
        public override bool Match(object obj)
        {
            return obj is int;
        }
    }

}