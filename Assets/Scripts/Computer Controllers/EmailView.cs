using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DeleteAfterReading.Model;

namespace DeleteAfterReading
{
    public class EmailView : MonoBehaviour {

        private Disk currentDisk;

        public TextMeshPro to;
        public TextMeshPro from;
        public TextMeshPro body;

        public TextButton save;
        public TextButton eject;
        public TextButton close;
        public TextButton delete;

        // Use this for initialization
        void Start() {
            save.clickHandler += onSave;
            eject.clickHandler += onEject;
            close.clickHandler += onClose;
            delete.clickHandler += onDelete;
        }

        // Update is called once per frame
        void Update() {

        }

        public void LoadExternalDisk(Disk d)
        {
            to.text = "To: " + d.to;
            from.text = "From: "+d.from;
            body.text = d.text;

            save.gameObject.SetActive(true);
            eject.gameObject.SetActive(true);
            close.gameObject.SetActive(false);
            delete.gameObject.SetActive(false);

            currentDisk = d;
        }

        public void onSave()
        {
            Debug.Log("save");

            ComputerController.instance.SaveDisk(currentDisk);
        }

        public void onEject()
        {
            Debug.Log("eject");

            ComputerController.instance.ShowDesktop();
        }

        public void onClose()
        {
            Debug.Log("close");

            ComputerController.instance.ShowDesktop();
        }

        public void onDelete()
        {
            Debug.Log("delete");

            ComputerController.instance.DeleteDisk(currentDisk);
        }
    }
}