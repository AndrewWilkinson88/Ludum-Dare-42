using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DeleteAfterReading.Model;

namespace DeleteAfterReading
{
    public class PhysicalDisk : MonoBehaviour
    {
        private bool mouseIsDown = false;
        private Collider2D curCollision;

        public Disk diskData;

        public TextMeshPro diskTitle;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (mouseIsDown)
            {
                transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
            }
        }

        public void SetDisk(Disk d)
        {
            diskData = d;
            diskTitle.text = d.title;
        }

        void OnMouseDown()
        {
            mouseIsDown = true;
        }

        void OnMouseUp()
        {
            mouseIsDown = false;
            //TODO checking colliders name is a terrible way to do this.  Fix it at some point if you have time!
            if(curCollision != null && curCollision.name == "DriveTrigger")
            {
                ComputerController.instance.LoadExternalEmail(diskData);
            }
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            curCollision = col;
        }

        void OnTriggerExit2D(Collider2D col)
        {
            curCollision = null;
        }
    }
}
