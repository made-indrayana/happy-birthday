using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;

public class MoveYTween : MonoBehaviour
{
    [Header("Positions")]
    public float initPos;
    public float targetPos;

    [Header("Speed")]
    public float toTarget = 2f;
    public float fromTarget = 2f;

    [Header("Easing")]
    public Ease easingTo;
    public Ease easingFrom;

    public bool startAnimationOnEnable = true;

    public Sequence animationSequence;

    private void Awake()
    {
        ResetPositionImmediate();
    }

    private void Start()
    {
        if (startAnimationOnEnable)
            StartAnimation();
    }

    [Button]
    public void StartAnimation()
    {
        animationSequence = DOTween.Sequence();

        animationSequence.Append(transform.DOMoveY(targetPos, toTarget).SetEase(easingTo));
        animationSequence.Append(transform.DOMoveY(initPos, fromTarget).SetEase(easingFrom));

        animationSequence.SetLoops(-1).Play();
    }
    [Button]
    public void ResetPositionWithTween()
    {
        animationSequence.Kill();
        transform.DOMoveY(initPos, fromTarget).SetEase(easingFrom);
    }

    [Button]
    public void ResetPositionImmediate()
    {
        transform.position = new Vector3(transform.position.x, initPos, transform.position.z);
    }

    private void OnDisable()
    {
        ResetPositionImmediate();
    }
}
