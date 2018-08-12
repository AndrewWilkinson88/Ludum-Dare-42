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
                this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
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

            if(this == ComputerController.instance.diskInDrive)
            {
                ComputerController.instance.diskInDrive = null;
                ComputerController.instance.ShowDesktop();
            }
            this.GetComponent<Rigidbody2D>().isKinematic = false;
        }

        void OnMouseUp()
        {
            mouseIsDown = false;
            //TODO checking colliders name is a terrible way to do this.  Fix it at some point if you have time!
            if(curCollision != null && curCollision.name == "DriveTrigger" && ComputerController.instance.diskInDrive == null)
            {
                this.GetComponent<Rigidbody2D>().isKinematic = true;
                this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                this.GetComponent<Rigidbody2D>().rotation = 0;
                this.GetComponent<Rigidbody2D>().angularVelocity = 0;
                this.transform.position = curCollision.gameObject.transform.position;
                ComputerController.instance.LoadExternalEmail(this);
            }
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.name == "DriveTrigger")
            {
                if (ComputerController.instance.diskInDrive == null)
                    GetComponent<SpriteRenderer>().color = Color.green;
                else
                    GetComponent<SpriteRenderer>().color = Color.red;
            }
            curCollision = col;
        }

        void OnTriggerExit2D(Collider2D col)
        {
            if (col.name == "DriveTrigger")
            {
                GetComponent<SpriteRenderer>().color = Color.white;
            }

            curCollision = null;
        }
    }
}
