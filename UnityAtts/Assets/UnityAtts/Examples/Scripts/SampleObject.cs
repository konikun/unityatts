using UnityEngine;
using System.Collections;

namespace UnityAtts.Examples
{
    public class SampleObject : MonoBehaviour
    {

        #region Unity Fields
        [SerializeField] [LayerSelector]
        private int singleLayer;
        #endregion Unity Fields

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}