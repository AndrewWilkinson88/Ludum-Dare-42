using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace DeleteAfterReading
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

        public void MoveLeft()
        {
            mainCamera.transform.DOMove(cameraPositionLeft, 0.25f);
            screenState = ScreenState.LEFT;
            SetButtonVisibility();
        }

        public void MoveRight()
        {
            mainCamera.transform.DOMove(cameraPositionRight, 0.25f);
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

