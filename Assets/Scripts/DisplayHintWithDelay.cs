using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using NaughtyAttributes;

public class DisplayHintWithDelay : MonoBehaviour
{
    [Header("Text")]
    public Text text;

    [Header("Text To Change To")]
    public string hintOne;
    public string hintTwo;
    public float textSpeed = 1f;

    [Header("Delays")]
    public float delayOne = 10f;
    public float delayTwo = 10f;

    [Button]
    public void ShowOnlyFirstHint()
    {
        text.DOText(hintOne, textSpeed, true, ScrambleMode.All);
    }

    [Button]
    public void ShowHints()
    {
        StartCoroutine(ShowHintCoroutine());
    }

    IEnumerator ShowHintCoroutine()
    {
        yield return new WaitForSecondsRealtime(delayOne);
        text.DOText(hintOne, textSpeed, true, ScrambleMode.All);
        yield return new WaitForSecondsRealtime(delayTwo);
        text.DOText(hintTwo, textSpeed, true, ScrambleMode.All);
    }

    [Button]
    public void EraseHint()
    {
        text.DOText("                   ", textSpeed, true, ScrambleMode.All);
    }
}
