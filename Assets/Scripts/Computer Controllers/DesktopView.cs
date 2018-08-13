﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DeleteAfterReading.Model;
using DG.Tweening;
using TMPro;

namespace DeleteAfterReading
{
    public class DesktopView : MonoBehaviour {

        private int maxRows = 2;
        private int maxColumns = 3;

        private float xSpacing = 3.5f;
        private float ySpacing = 3.5f;

        public DiskSpaceSlot diskSpacePrefab;
        private List<DiskSpaceSlot> diskSpaceSlots;

        private int openDiskSpace;

        public void CreateOpenSlots(int numSlots)
        {
            openDiskSpace = numSlots;
            if (diskSpaceSlots != null)
                DestroyCurrentSlots();
            diskSpaceSlots = new List<DiskSpaceSlot>();

            for(int i =0; i < numSlots; i++)
            {
                int x = i % maxColumns;
                int y = (int)(i / maxColumns);
                diskSpaceSlots.Add(CreateDiskSpaceSlot(x, y));
            }
        }

        public void SaveDisk(Disk d)
        {
            for(int i = 0; i < diskSpaceSlots.Count; i++)
            {
                if(diskSpaceSlots[i].IsAvailable())
                {
                    diskSpaceSlots[i].SetDisk(d);
                    openDiskSpace--;
                    return;
                }
            }
        }

        public void DeleteDisk(Disk d)
        {
            for (int i = 0; i < diskSpaceSlots.Count; i++)
            {
                if(diskSpaceSlots[i].GetDisk() == d)
                {
                    diskSpaceSlots[i].DeleteDisk();
                    openDiskSpace++;
                    return;
                }
            }
        }

        public bool IsDiskFull()
        {
            return openDiskSpace <= 0;
        }

        DiskSpaceSlot CreateDiskSpaceSlot(int x, int y)
        {
            DiskSpaceSlot newSlot = GameObject.Instantiate<DiskSpaceSlot>(diskSpacePrefab);
            newSlot.transform.parent = transform;
            newSlot.transform.localPosition = new Vector3(x * xSpacing, -y * ySpacing, 0);
            return newSlot;
        }

        void DestroyCurrentSlots()
        {
            for (int i = diskSpaceSlots.Count-1; i >= 0; i--)
            {
                GameObject.Destroy(diskSpaceSlots[i].gameObject);                
            }
        }

        public List<string> GetKeywords()
        {
            List<string> keywords = new List<string>();
            for (int i = 0; i < diskSpaceSlots.Count; i++)
            {
                if (!diskSpaceSlots[i].IsAvailable())
                {
                    keywords.AddRange(diskSpaceSlots[i].GetDisk().keywords);
                }
            }
            return keywords;
        }
    }
}