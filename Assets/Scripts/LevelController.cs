using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using DeleteAfterReading.Model;
using DG.Tweening;

namespace DeleteAfterReading
{
    public enum GameState
    {
        TITLE_SCREEN,
        MISSION_SELECT,
        MISSION_LOADING,
        MISSION_RUNNING,
        MISSION_PAUSED,
        MISSION_SOLUTION,
        END_LEVEL
    }

    public class LevelController : MonoBehaviour
    {
        public MainMenuView mainMenuView;

        public GameObject diskSpawner;
        public PhysicalDisk diskPrefab;
        public ResultScreenController resultScreen;
        public LevelData currentLevel;
        public SpriteRenderer fader;
        public ComputerController computerController;

        public GameState state = GameState.TITLE_SCREEN;

        public int currentLevelUnlocked = 1;
        public int levelBeingPlayed = 0;

        private float missionTime = 0.0f;
        private Dictionary<Disk, float> diskSpawnTimes = new Dictionary<Disk, float>();

        private List<PhysicalDisk> diskList = new List<PhysicalDisk>();
        
        private void Awake()
        {
            fader.gameObject.SetActive(true);
        }

        // Use this for initialization
        void Start()
        {
            state = GameState.TITLE_SCREEN;
            mainMenuView.mission1.clickHandler += SelectLevelOne;
            mainMenuView.mission2.clickHandler += SelectLevelTwo;

            fader.DOFade(0.0f, 2.0f).OnComplete( ()=>mainMenuView.StartTitleScreenSequence());
        }

        // Update is called once per frame
        void Update()
        {
            switch (state)
            {
                case GameState.TITLE_SCREEN:
                    break;
                case GameState.MISSION_SELECT:
                    break;
                case GameState.MISSION_LOADING:
                    break;
                case GameState.MISSION_RUNNING:
                    missionTime += Time.deltaTime;
                    CheckSpawnDisk();
                    CheckMissionEnd();

                    float remainingTime = currentLevel.timeToSolve - missionTime;
                    int minutes = Mathf.FloorToInt(remainingTime / 60.0f);
                    int seconds = Mathf.FloorToInt(remainingTime - minutes * 60);
                    computerController.UpdateTimer(string.Format("{0:0}:{1:00}", minutes, seconds));
                    break;
                case GameState.MISSION_PAUSED:
                    break;
                case GameState.MISSION_SOLUTION:
                    break;
                case GameState.END_LEVEL:
                    break;
            }
        }

        public void SelectLevelOne(string data)
        {
            mainMenuView.HideTitleScreen();
            LoadLevel(1);
        }

        public void SelectLevelTwo(string data)
        {
            mainMenuView.HideTitleScreen();
            LoadLevel(2);
        }

        /// <summary>
        /// Loads a Level based on it's Json
        /// </summary>
        void LoadLevel(int levelNum)
        {
            levelBeingPlayed = levelNum;
            missionTime = 0.0f;

            TextAsset levelJsonText = Resources.Load<TextAsset>("level" + levelNum);
            Debug.Log(levelJsonText.text);

            currentLevel = JsonUtility.FromJson<LevelData>(levelJsonText.text);
            ComputerController.instance.desktopView.CreateOpenSlots(currentLevel.availableSpace);            

            foreach(Disk d in currentLevel.disks)
            {
                diskSpawnTimes.Add(d, d.start);
            }

            computerController.ShowDesktop();
            state = GameState.MISSION_RUNNING;
        }

        public void CheckLevelUnlock()
        {
            if (levelBeingPlayed + 1 > currentLevelUnlocked)
                currentLevelUnlocked = levelBeingPlayed + 1;
        }
        
        /// <summary>
        /// Checks if a Disk should be spawned, and spawns it
        /// </summary>
        bool CheckSpawnDisk()
        {
            bool spawnedDisk = false;

            //Spawn disks that have exceeded their start time
            List<Disk> diskAdded = new List<Disk>();
            foreach (var pair in diskSpawnTimes)
            {
                if (missionTime >= (pair.Value))
                {
                    SpawnDisk(pair.Key);
                    diskAdded.Add(pair.Key);
                    spawnedDisk = true;
                }
            }

            //Remove disks that have been added to the scene
            if (spawnedDisk)
            {
                foreach (var disk in diskAdded)
                {
                    diskSpawnTimes.Remove(disk);
                }
            }

            return spawnedDisk;
        }

        /// <summary>
        /// Spawns a Physical Disk into the scene
        /// </summary>
        void SpawnDisk(Disk d)
        {
            PhysicalDisk pd = Instantiate<PhysicalDisk>(diskPrefab);
            pd.transform.parent = diskSpawner.transform;
            pd.transform.localPosition = Vector3.zero;
            pd.SetDisk(d);
            diskList.Add(pd);
        }

        /// <summary>
        /// Checks if the mission time has exceeded the time defined in levelData
        /// </summary>
        bool CheckMissionEnd()
        {
            bool missionEnd = false;
            if(missionTime >= currentLevel.timeToSolve )
            {
                missionEnd = true;
                state = GameState.MISSION_SOLUTION;
                ComputerController.instance.OpenSolver();
            }

            return missionEnd;
        }

        public void ShowResult(bool didSucceed, string message)
        {
            Sequence resultSequence = DOTween.Sequence();

            resultSequence.Append(fader.DOFade(1.0f, 0.5f));
            resultSequence.AppendCallback(() =>
               {
                   if(didSucceed)
                   {
                       resultScreen.ShowNewspaperSuccess(message);
                   }
                   else
                   {
                       resultScreen.ShowNewspaperFailure(message);
                   }
               }
            );
            resultSequence.Append(fader.DOFade(0.0f, 0.5f));
        }

        public void ResetLevel()
        {
            for (int i = diskList.Count - 1; i >= 0; i--)
            {
                GameObject.Destroy(diskList[i].gameObject);
            }
            diskList = new List<PhysicalDisk>();
            state = GameState.MISSION_SELECT;
        }
    }
}
