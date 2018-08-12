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
        public LevelData curLevelData;

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

            curLevelData = JsonUtility.FromJson<LevelData>(levelJsonText.text);

            Debug.Log(curLevelData.puzzle.prompt);

            ComputerController.instance.desktopView.CreateOpenSlots(curLevelData.availableSpace);            

            foreach(Disk d in curLevelData.disks)
            {
                Debug.Log(d.text);
                StartCoroutine(SpawnDisk(d));
            }
        }

        public IEnumerator SpawnDisk(Disk d)
        {
            yield return new WaitForSeconds(d.start);
            PhysicalDisk pd = GameObject.Instantiate<PhysicalDisk>(diskPrefab);
            pd.transform.parent = diskSpawner.transform;
            pd.transform.localPosition = Vector3.zero;
            pd.SetDisk(d);
        }
    }
}
