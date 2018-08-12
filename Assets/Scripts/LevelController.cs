using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using DeleteAfterReading.Model;

namespace DeleteAfterReading
{
    public class LevelController : MonoBehaviour
    {
        public GameObject diskSpawner;
        public PhysicalDisk diskPrefab;

        // Use this for initialization
        void Start()
        {
            LoadLevel(1);
        }

        // Update is called once per frame
        void Update()
        {

        }

        void LoadLevel(int levelNum)
        {
            TextAsset levelJsonText = Resources.Load<TextAsset>("level" + levelNum);
            Debug.Log(levelJsonText.text);

            LevelData levelData = JsonUtility.FromJson<LevelData>(levelJsonText.text);

            ComputerController.instance.desktopView.CreateOpenSlots(levelData.availableSpace);            

            foreach(Disk d in levelData.disks)
            {
                Debug.Log(d.text);
                PhysicalDisk pd = GameObject.Instantiate<PhysicalDisk>(diskPrefab);
                pd.transform.parent = diskSpawner.transform;
                pd.transform.localPosition = Vector3.zero;
                pd.SetDisk(d);
            }
        }
    }
}
