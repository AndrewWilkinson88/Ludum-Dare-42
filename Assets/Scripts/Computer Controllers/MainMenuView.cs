﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainMenuView : MonoBehaviour
{
    public SpriteRenderer computerBackground;
    public SpriteRenderer titleScreen;
    public Color OnColor;

    public TextButton mission1;
    public TextButton mission2;

    public void StartTitleScreenSequence()
    {
        Sequence startSequence = DOTween.Sequence();
 
        startSequence.Append(computerBackground.DOColor(OnColor, 1.0f).SetEase(Ease.OutFlash, 15, 1));
        startSequence.Append(titleScreen.transform.DOLocalMoveY(3.0f, 1.0f));
        startSequence.AppendCallback(() => mission1.gameObject.SetActive(true));
        startSequence.Play();
    }

    public void ShowTitleScreen()
    {
        titleScreen.gameObject.SetActive(true);
        mission1.gameObject.SetActive(true);
        mission2.gameObject.SetActive(true);
    }

    public void HideTitleScreen()
    {
        titleScreen.gameObject.SetActive(false);
        mission1.gameObject.SetActive(false);
        mission2.gameObject.SetActive(false);
    }

}
