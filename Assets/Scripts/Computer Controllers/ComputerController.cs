using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DeleteAfterReading.Model;

namespace DeleteAfterReading
{
    public class ComputerController : MonoBehaviour
    {
        //public LevelSelect levelSelect; 
        public LevelController levelController;

        public DesktopView desktopView;

        public EmailView emailView;

        public Solver solverView;

        public static ComputerController instance;

        public PhysicalDisk diskInDrive;

        public TextButton timerText;

        enum Mode
        {
            LEVEL_SELECT,
            DESKTOP,
            EXTERNAL_EMAIL,
            DESKTOP_EMAIL,
            SOLVER
        }

        private Mode curMode = Mode.LEVEL_SELECT;

        // Use this for initialization
        void Start()
        {
            instance = this;
            timerText.clickHandler += HandleTimerClick;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void LoadDesktopEmail(Disk d)
        {
            curMode = Mode.DESKTOP_EMAIL;
            SetActiveScreen(emailView.gameObject);
            emailView.LoadDesktopEmail(d);
        }

        public void LoadExternalEmail(PhysicalDisk pd)
        {
            diskInDrive = pd;
            curMode = Mode.EXTERNAL_EMAIL;
            SetActiveScreen(emailView.gameObject);
            emailView.LoadExternalEmail(pd.diskData);
        }

        public void ShowDesktop()
        {
            curMode = Mode.DESKTOP;
            SetActiveScreen(desktopView.gameObject);
            timerText.gameObject.SetActive(true);
        }

        public void OpenSolver()
        {
            curMode = Mode.SOLVER;
            SetActiveScreen(solverView.gameObject);
            timerText.gameObject.SetActive(false);
            solverView.SetupSolver(levelController.curLevelData.puzzle, desktopView.GetKeywords());
        }

        public void SetActiveScreen(GameObject g)
        {
            desktopView.gameObject.SetActive(false);
            emailView.gameObject.SetActive(false);
            solverView.gameObject.SetActive(false);            
            g.SetActive(true);
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

        public void HandleTimerClick(string s)
        {
            OpenSolver();
        }
    }
}