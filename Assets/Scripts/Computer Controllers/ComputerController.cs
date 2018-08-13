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

        public MainMenuView mainMenuView;

        public DesktopView desktopView;

        public EmailView emailView;

        public Solver solverView;

        public static ComputerController instance;
        public PhysicalDisk diskInDrive;
        public TextButton timerText;

        private float remainingTime;

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

        public void LoadDesktopEmail(Disk disk)
        {
            if (curMode == Mode.SOLVER)
                return;

            curMode = Mode.DESKTOP_EMAIL;
            SetActiveScreen(emailView.gameObject);
            emailView.LoadDesktopEmail(disk);
        }

        public void LoadExternalEmail(PhysicalDisk physicalDisk)
        {
            if (curMode == Mode.SOLVER)
                return;

            curMode = Mode.EXTERNAL_EMAIL;
            diskInDrive = physicalDisk;
            SetActiveScreen(emailView.gameObject);
            emailView.LoadExternalEmail(physicalDisk.diskData);
        }

        public void ShowDesktop()
        {
            if (curMode == Mode.SOLVER)
                return;

            curMode = Mode.DESKTOP;
            SetActiveScreen(desktopView.gameObject);
            timerText.gameObject.SetActive(true);
        }

        public void UpdateTimer(string time)
        {
            timerText.text.text = time;
        }

        public void OpenSolver()
        {
            if (curMode == Mode.SOLVER)
                return;
            curMode = Mode.SOLVER;
            SetActiveScreen(solverView.gameObject);
            timerText.gameObject.SetActive(false);
            solverView.SetupSolver(levelController.currentLevel.puzzle, desktopView.GetKeywords());
        }

        public void ShowTitleScreen()
        {
            SetActiveScreen(mainMenuView.gameObject);
            levelController.ResetLevel();
            mainMenuView.ShowTitleScreen();
            curMode = Mode.LEVEL_SELECT;
        }

        public void SetActiveScreen(GameObject activeView)
        {
            desktopView.gameObject.SetActive(false);
            emailView.gameObject.SetActive(false);
            solverView.gameObject.SetActive(false);
            mainMenuView.gameObject.SetActive(false);

            activeView.SetActive(true);
        }

        public void SaveDisk(Disk disk)
        {
            desktopView.SaveDisk(disk);
            ShowDesktop();
        }

        public void DeleteDisk(Disk disk)
        {
            desktopView.DeleteDisk(disk);
            ShowDesktop();
        }

        public void HandleTimerClick(string s)
        {
            OpenSolver();
        }

        public bool IsDiskFull()
        {
            return desktopView.IsDiskFull();
        }
    }
}