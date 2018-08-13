using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DeleteAfterReading.Model;
using DG.Tweening;
using TMPro;

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

        public SpriteRenderer stickyNote;
        public SpriteRenderer stickyArm;
        public TextMeshPro stickyText;

        public static ComputerController instance;
        public PhysicalDisk diskInDrive;
        public TextButton timerText;

        public AudioSource disketteInsert;
        public AudioSource disketteEject;
        public AudioSource stickyNoteSound;
        public AudioSource buttonPress;

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

            disketteInsert.Play();

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

        public void ShowStickyNote(string promptText)
        {
            stickyText.text = promptText;

            Sequence stickySequence = DOTween.Sequence();
            stickySequence.Append(stickyNote.transform.DOLocalMoveY(2.5f, 0.5f).SetEase(Ease.InExpo));
            stickySequence.Join(stickyArm.transform.DOLocalMoveY(-4f, 0.5f).SetEase(Ease.InExpo));
            stickySequence.AppendCallback(() => stickyNoteSound.Play());
            stickySequence.Append(stickyArm.transform.DOLocalMoveY(-13f, 0.5f).SetEase(Ease.InExpo));
            stickySequence.SetId("StickySequence");            
        }

        public void ResetStickyNote()
        {
            DOTween.Kill("StickySequence");
            stickyNote.transform.position = new Vector3(0.82f, -7.5f, 0.0f);
            stickyArm.transform.position = new Vector3(1.71f, -14.22f, 0.0f);
        }

        public void UpdateTimer(string time)
        {
            timerText.text.text = time;
        }

        public void OpenSolver()
        {
            if (curMode == Mode.SOLVER)
                return;

            List<string> keywords = desktopView.GetKeywords();
            if (keywords.Count > 0)
            {
                curMode = Mode.SOLVER;
                SetActiveScreen(solverView.gameObject);
                timerText.gameObject.SetActive(false);
                solverView.SetupSolver(levelController.currentLevel.puzzle, keywords);
            }
            else
            {
                levelController.ShowResult(false, "CIA somehow manages to find no clues!");
            }
        }

        public void ShowTitleScreen()
        {
            SetActiveScreen(mainMenuView.gameObject);
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
            ComputerController.instance.buttonPress.Play();
            OpenSolver();
        }

        public bool IsDiskFull()
        {
            return desktopView.IsDiskFull();
        }
    }
}