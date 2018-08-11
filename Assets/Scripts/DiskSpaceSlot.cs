using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using deleteAfterReading.Model;
using TMPro;

namespace deleteAfterReading
{
    public class DiskSpaceSlot : MonoBehaviour
    {
        public GameObject diskImage;
        public TextMeshPro diskText;
        private Disk currentDisk;

        // Use this for initialization
        void Start()
        {
            diskImage.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {

        }

        Disk getDisk()
        {
            return currentDisk;
        }

        void SetDisk(Disk d)
        {
            currentDisk = d;
            diskText.text  = d.title;

        }

        void DeleteDisk()
        {
            currentDisk = null;
            diskImage.SetActive(false);
        }
    }
}
