using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using deleteAfterReading.Model;

namespace deleteAfterReading
{
    public class LevelController : MonoBehaviour
    {
        DiskSpaceSlot diskSpacePrefab;
        PhysicalDisk diskPrefab;

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

            for(int i = 0; i < levelData.availableSpace; i++)
            {
                //TODO create the spaces on the disk to store disks
                Debug.Log("CREATING DISK SPACE");
            }

            foreach(Disk d in levelData.disks)
            {
                Debug.Log(d.text);
            }
        }
    }
}
