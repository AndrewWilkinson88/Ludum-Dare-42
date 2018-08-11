﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace deleteAfterReading
{
    public class DesktopView : MonoBehaviour {

        private int maxRows = 2;
        private int maxColumns = 3;

        private float xSpacing = 3.5f;
        private float ySpacing = 3.5f;

        public DiskSpaceSlot diskSpacePrefab;

        private List<DiskSpaceSlot> diskSpaceSlots;

        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }

        public void CreateOpenSlots(int numSlots)
        {
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
    }
}