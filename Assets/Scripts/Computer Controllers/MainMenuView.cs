using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace DeleteAfterReading
{
    public class MainMenuView : MonoBehaviour
    {
        public SpriteRenderer computerBackground;
        public SpriteRenderer titleScreen;
        public SpriteRenderer stickyNote;
        public SpriteRenderer stickyArm;
        public SpriteRenderer powerButtonOn;
        public SpriteRenderer powerButtonOff;

        public Color OnColor;

        public TextButton mission1;
        public TextButton mission2;

        public void StartTitleScreenSequence()
        {
            Sequence startSequence = DOTween.Sequence();

            powerButtonOff.gameObject.SetActive(false);
            startSequence.Append(computerBackground.DOColor(OnColor, 1.0f).SetEase(Ease.OutFlash, 15, 1));
            startSequence.Append(titleScreen.transform.DOLocalMoveY(3.0f, 1.0f));
            startSequence.Append(stickyNote.transform.DOLocalMoveY(2.5f, 0.5f).SetEase(Ease.InExpo));
            startSequence.Join(stickyArm.transform.DOLocalMoveY(-4, 0.5f).SetEase(Ease.InExpo));
            startSequence.Append(stickyArm.transform.DOLocalMoveY(-13, 0.5f).SetEase(Ease.InExpo));
            startSequence.AppendCallback(() => mission1.transform.parent.gameObject.SetActive(true));
            startSequence.Play();
        }

        public void ShowTitleScreen()
        {
            titleScreen.gameObject.SetActive(true);
            mission1.transform.parent.gameObject.SetActive(true);
            int levelUnlocked = ComputerController.instance.levelController.currentLevelUnlocked;
            mission2.transform.parent.gameObject.SetActive(levelUnlocked >= 2);
            //mission3.gameObject.SetActive(levelUnlocked >= 3);
        }

        public void HideTitleScreen()
        {
            titleScreen.gameObject.SetActive(false);
            mission1.transform.parent.gameObject.SetActive(false);
            mission2.transform.parent.gameObject.SetActive(false);
        }

    }
}
