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
        public GameObject diskSpawner;
        public PhysicalDisk diskPrefab;

        public SpriteRenderer fader;
        public SpriteRenderer computerBackground;
        public SpriteRenderer titleScreen;
        public Color OnColor;

        public GameState state = GameState.TITLE_SCREEN;

        private float missionTime = 0.0f;
        private Dictionary<Disk, float> diskSpawnTimes = new Dictionary<Disk, float>();
        private LevelData currentLevel;

        private void Awake()
        {
            fader.gameObject.SetActive(true);
        }

        // Use this for initialization
        void Start()
        {
            state = GameState.TITLE_SCREEN;
            StartTitleScreenSequence();
            //LoadLevel(1);
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
                    break;
                case GameState.MISSION_PAUSED:
                    break;
                case GameState.MISSION_SOLUTION:
                    break;
                case GameState.END_LEVEL:
                    break;
            }
        }

        void StartTitleScreenSequence()
        {
            Sequence startSequence = DOTween.Sequence();
            startSequence.Append(fader.DOFade(0.0f, 2.0f));
            startSequence.Append(computerBackground.DOColor(OnColor, 1.0f).SetEase(Ease.OutFlash, 15, 1));
            startSequence.Append(titleScreen.transform.DOLocalMoveY(3.0f, 1.0f));
            startSequence.Play();
        }

        /// <summary>
        /// Loads a Level based on it's Json
        /// </summary>
        void LoadLevel(int levelNum)
        {
            missionTime = 0.0f;

            TextAsset levelJsonText = Resources.Load<TextAsset>("level" + levelNum);
            //Debug.Log(levelJsonText.text);

            currentLevel = JsonUtility.FromJson<LevelData>(levelJsonText.text);
            ComputerController.instance.desktopView.CreateOpenSlots(currentLevel.availableSpace);            

            foreach(Disk d in currentLevel.disks)
            {
                diskSpawnTimes.Add(d, d.start);
            }

            state = GameState.MISSION_RUNNING;
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
                if (pair.Value >= missionTime)
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
        }

        /// <summary>
        /// Checks if the mission time has exceeded the time defined in levelData
        /// </summary>
        bool CheckMissionEnd()
        {
            bool missionEnd = false;
            if(currentLevel.timeToSolve >= missionTime )
            {
                missionEnd = true;
                state = GameState.MISSION_SOLUTION;
            }

            return missionEnd;
        }
    }
}
