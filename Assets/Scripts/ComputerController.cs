using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace deleteAfterReading
{
    public class ComputerController : MonoBehaviour
    {

        //public LevelSelect levelSelect; 
        public DesktopView desktopView;

        public static ComputerController instance;

        enum Mode
        {
            LEVEL_SELECT,
            DESKTOP,
            EMAIL,
        }

        private Mode curMode = Mode.LEVEL_SELECT;

        // Use this for initialization
        void Start()
        {
            instance = this;

        }

        // Update is called once per frame
        void Update()
        {

        }


    }
}