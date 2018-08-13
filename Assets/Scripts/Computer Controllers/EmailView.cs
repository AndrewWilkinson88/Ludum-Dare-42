using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DeleteAfterReading.Model;
using DG.Tweening;
using DG.DemiLib.External;

namespace DeleteAfterReading
{
    public class EmailView : MonoBehaviour {

        private Disk currentDisk;
        private PhysicalDisk currentPhysicalDisk;

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

        public void LoadExternalEmail(Disk d)
        {
            initText();
            Sequence FillText = DOTween.Sequence();

            FillText.AppendInterval(0.5f);
            FillText.Append(to.DOText("To: " + d.to, d.to.Length / 25.0f, true).SetEase(Ease.Linear));
            FillText.AppendInterval(0.5f);
            FillText.Append(from.DOText("From: " + d.from, d.from.Length / 25.0f, true).SetEase(Ease.Linear));
            FillText.AppendInterval(0.5f);
            FillText.Append(body.DOText(d.text, d.text.Length / 60.0f, true).SetEase(Ease.Linear));

            FillText.AppendCallback(() =>
                {
                    save.gameObject.SetActive(true);
                    eject.gameObject.SetActive(true);
                }
            );
            FillText.SetId("FillText");

            currentDisk = d;


            if (ComputerController.instance.IsDiskFull())
            {
                save.text.color = Color.gray;
            }
            else
            {
                save.text.color = eject.text.color;
            }
        }

        void initText()
        {
            DOTween.Kill("FillText");

            to.text = "";
            from.text = "";
            body.text = "";

            save.gameObject.SetActive(false);
            eject.gameObject.SetActive(false);
            close.gameObject.SetActive(false);
            delete.gameObject.SetActive(false);
        }

        public void LoadDesktopEmail(Disk d)
        {
            to.text = "To: " + d.to;
            from.text = "From: " + d.from;
            body.text = d.text;

            save.gameObject.SetActive(false);
            eject.gameObject.SetActive(false);
            close.gameObject.SetActive(true);
            delete.gameObject.SetActive(true);

            currentDisk = d;
        }

        public void onSave(string s)
        {
            if (!ComputerController.instance.IsDiskFull())
            {
                ComputerController.instance.buttonPress.Play();
                ComputerController.instance.SaveDisk(currentDisk);
                ComputerController.instance.diskInDrive.EjectDisk();
            }
        }

        public void onEject(string s)
        {
            Debug.Log("eject");
            ComputerController.instance.buttonPress.Play();
            ComputerController.instance.ShowDesktop();
            ComputerController.instance.diskInDrive.EjectDisk();
        }

        public void onClose(string s)
        {
            Debug.Log("close");
            ComputerController.instance.buttonPress.Play();
            ComputerController.instance.ShowDesktop();
        }

        public void onDelete(string s)
        {
            Debug.Log("delete");
            ComputerController.instance.buttonPress.Play();
            ComputerController.instance.DeleteDisk(currentDisk);
        }
    }
}