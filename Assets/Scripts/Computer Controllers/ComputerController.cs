using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DeleteAfterReading.Model;

namespace DeleteAfterReading
{
    public class ComputerController : MonoBehaviour
    {
        //public LevelSelect levelSelect; 
        public DesktopView desktopView;

        public EmailView emailView;

        public static ComputerController instance;

        public PhysicalDisk diskInDrive;

        enum Mode
        {
            LEVEL_SELECT,
            DESKTOP,
            EXTERNAL_EMAIL,
            DESKTOP_EMAIL
        }

        private Mode curMode = Mode.LEVEL_SELECT;

        // Use this for initialization
        void Start()
        {
            instance = this;

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void LoadExternalEmail(PhysicalDisk pd)
        {
            diskInDrive = pd;
            curMode = Mode.EXTERNAL_EMAIL;
            desktopView.gameObject.SetActive(false);
            emailView.gameObject.SetActive(true);
            emailView.LoadExternalDisk(pd.diskData);
        }

        public void ShowDesktop()
        {
            desktopView.gameObject.SetActive(true);
            emailView.gameObject.SetActive(false);
        }

        public void SaveDisk(Disk d)
        {
            desktopView.SaveDisk(d);
            ShowDesktop();
        }

        public void DeleteDisk(Disk d)
        {
            desktopView.DeleteDisk(d);
            ShowDesktop();
        }
    }
}