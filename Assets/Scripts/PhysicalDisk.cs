using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DeleteAfterReading.Model;
using DG.Tweening;

namespace DeleteAfterReading
{
    public class PhysicalDisk : MonoBehaviour
    {
        private bool mouseIsDown = false;
        private bool inDrive = false;
        private Collider2D curCollision;
        
        public Disk diskData;
        public SpriteRenderer diskImage;
        public TextMeshPro diskTitle;

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
                diskImage.sortingOrder = 11;
                diskTitle.sortingOrder = 12;
                inDrive = false;

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
                inDrive = true;
                InsertDisk();
            }
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.name == "DriveTrigger" && !inDrive)
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

        void InsertDisk()
        {
            DOTween.Kill("EjectSequence");

            //Turn physics off
            this.GetComponent<Rigidbody2D>().isKinematic = true;
            this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            this.GetComponent<Rigidbody2D>().rotation = 0;
            this.GetComponent<Rigidbody2D>().angularVelocity = 0;
            GetComponent<SpriteRenderer>().color = Color.white;

            //Transform to position to be inserted
            transform.position = new Vector3(1.64f, -4.0f, 0.0f);
            diskImage.sortingOrder = 8;
            diskTitle.sortingOrder = 9;

            Sequence insertSequence = DOTween.Sequence();
            insertSequence.Append(transform.DOMoveY(-0.75f, .5f).SetEase(Ease.InCubic));
            insertSequence.SetId("InsertSequence");

            ComputerController.instance.LoadExternalEmail(this);
        }

        public void EjectDisk()
        {
            DOTween.Kill("InsertSequence");
            ComputerController.instance.diskInDrive = null;

            Sequence ejectSequence = DOTween.Sequence();
            ejectSequence.Append(transform.DOMoveY(-4.0f, .5f).SetEase(Ease.OutCubic));
            ejectSequence.AppendCallback(() =>
                {
                    //Turn Physics on
                    this.GetComponent<Rigidbody2D>().isKinematic = true;
                    this.GetComponent<Rigidbody2D>().velocity = Vector3.down * 10.0f;
                    diskImage.sortingOrder = 11;
                    diskTitle.sortingOrder = 12;
                    inDrive = false;
                }
            );
            ejectSequence.SetId("EjectSequence");
        }
    }
}
