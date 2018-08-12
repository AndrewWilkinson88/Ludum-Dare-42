using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DeleteAfterReading.Model;
using TMPro;
using UnityEngine.EventSystems;

namespace DeleteAfterReading
{
    public class DiskSpaceSlot : MonoBehaviour, IPointerClickHandler    
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

        public void OnPointerClick(PointerEventData eventData)
        {
            if (currentDisk != null)
            {
                ComputerController.instance.LoadDesktopEmail(currentDisk);
            }
        }

        public Disk GetDisk()
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
            return currentDisk == null;
        }
    }
}
