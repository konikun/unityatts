using UnityEngine;
using System.Collections;

namespace UnityAtts
{
    public class ImportComponentPropertiesAttribute : PropertyAttribute
    {
        /// <summary>
        /// Use this property only when a single type of component is used.
        /// </summary>
        public System.Type ComponentType { get { return ComponentTypes[0]; } }

        /// <summary>
        /// Use this property only when multiple types of component are used.
        /// </summary>
        public System.Type[] ComponentTypes { get; set; }

        public string[] Properties { get; set; }

        public bool MultipleTypes { get; private set; }

        /// <summary>
        /// The constructor to use when importing from a single component.
        /// </summary>
        /// <param name="componentType">The type of the component</param>
        /// <param name="properties"></param>
        public ImportComponentPropertiesAttribute(System.Type componentType, params string[] properties)
        {
            ComponentTypes = new System.Type[] { componentType };
            Properties = properties;
            MultipleTypes = false;
        }
        /// <summary>
        /// The constructor to use when importing from multiple components.
        /// </summary>
        /// <param name="componentTypes"></param>
        /// <param name="properties"></param>
        public ImportComponentPropertiesAttribute(System.Type[] componentTypes, params string[] properties)
        {
            ComponentTypes = componentTypes;
            Properties = properties;
            MultipleTypes = true;
        }

        public override bool Match(object obj)
        {
            return obj is Object;
        }
    }
}