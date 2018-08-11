using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Holoville.HOTween;
using Holoville.HOTween.Plugins;

namespace deleteAfterReading
{
    public class ScreenStateManager : MonoBehaviour
    {
        public enum ScreenState
        {
            LEFT,
            RIGHT
        }

        public Camera mainCamera;

        public Vector3 cameraPositionLeft;
        public Vector3 cameraPositionRight;

        public Button rightButton;
        public Button leftButton;
        private ScreenState screenState = ScreenState.LEFT;

        // Use this for initialization
        void Start()
        {
            SetButtonVisibility();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void MoveLeft()
        {
            //mainCamera.transform.position = cameraPositionLeft;
            HOTween.To(mainCamera.transform, .25f, "position", cameraPositionLeft);
            screenState = ScreenState.LEFT;
            SetButtonVisibility();
        }

        public void MoveRight()
        {
            //mainCamera.transform.position = cameraPositionRight;
            HOTween.To(mainCamera.transform, .25f, "position", cameraPositionRight);
            screenState = ScreenState.RIGHT;
            SetButtonVisibility();
        }

        void SetButtonVisibility()
        {
            if(screenState == ScreenState.LEFT)
            {
                leftButton.gameObject.SetActive(false);
                rightButton.gameObject.SetActive(true);
            }
            else if (screenState == ScreenState.RIGHT)
            {
                leftButton.gameObject.SetActive(true);
                rightButton.gameObject.SetActive(false);
            }
        }
    }
}

