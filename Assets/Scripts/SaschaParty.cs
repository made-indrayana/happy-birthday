using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using DG.Tweening;
using NaughtyAttributes;

public class SaschaParty : MonoBehaviour
{
    [Header("Party Music")]
    public StudioEventEmitter emitter;

    [Header("Tween Paths")]
    public GameObject raisePath;
    public GameObject[] paths;

    [Header("Tween Speed")]
    public float speed = 1f;

    public Ease easing = Ease.Linear;

    private Sequence animationSequence;

    [Button]
    public void StartParty()
    {
        emitter.Play();
        StartCoroutine(StartPartyCoroutine());

    }

    IEnumerator StartPartyCoroutine()
    {
        animationSequence = DOTween.Sequence();

        foreach (GameObject go in paths)
        {
            animationSequence.Append(transform.DOMove(go.transform.position, speed).SetEase(easing));
        }

        yield return animationSequence.Pause();

        yield return transform.DOMove(raisePath.transform.position, speed).SetEase(Ease.InSine).WaitForCompletion();

        yield return animationSequence.SetLoops(-1).Play();
    }
}
