using UnityEngine;
using System.Collections;

namespace UnityAtts.Examples
{
    public class SampleObject : MonoBehaviour
    {

        #region Unity Fields
        [SerializeField] [LayerSelector]
        private int singleLayer;

        [SerializeField] [Min(10)]
        private int minIs10;

        [SerializeField] [Max(10)]
        private int maxIs10;

        [SerializeField] [InstantiateButton(HideButton =true)]
        private GameObject toBeInstantiated;
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