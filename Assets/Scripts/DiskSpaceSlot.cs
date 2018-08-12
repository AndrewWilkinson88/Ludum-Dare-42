using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DeleteAfterReading.Model;
using TMPro;

namespace DeleteAfterReading
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

        public Disk getDisk()
        {
            return currentDisk;
        }

        public void SetDisk(Disk d)
        {
            currentDisk = d;
            diskText.text  = d.title;
            diskImage.SetActive(true);
        }

        public void DeleteDisk()
        {
            currentDisk = null;
            diskImage.SetActive(false);
        }

        public bool IsAvailable()
        {
            return currentDisk != null;
        }
    }
}
