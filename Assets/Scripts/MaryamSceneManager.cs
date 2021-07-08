using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;
using NaughtyAttributes;
using FMODUnity;

public class MaryamSceneManager : MonoBehaviour
{
    [Header("Sound")]
    public StudioEventEmitter emitter;

    [Header("Text")]
    public Text buttonText;
    public string[] strings;

    [Header("Color")]
    public Image button;
    public Material material;
    public Color[] colors;
    public Color[] invertedColors;

    [Header("Duration")]
    public float tweenDuration = 3f;

    [Header("Paths")]
    public GameObject stoneGO;
    public GameObject sinkingPath;

    [Header("Debug")]
    [SerializeField] int _clicked = 0;

    private Sequence rainbowSequence;
    private Sequence textColorSequence;
    [SerializeField] private float rainbowSpeed;

    [Header("Unity Events")]
    public UnityEvent OnChangeText;
    public UnityEvent OnPlayIstEgal;
    public UnityEvent OnStopIstEgal;
    public UnityEvent OnShowHintLouder;
    public UnityEvent OnEraseHint;
    public UnityEvent OnShowHintMath;
    public UnityEvent OnEnterAnswer;
    public UnityEvent DisableButtonPermanently;
    public UnityEvent OnEndReached;


    private void Awake()
    {
        rainbowSequence = DOTween.Sequence();
        textColorSequence = DOTween.Sequence();

        foreach (Color c in colors)
        {
            rainbowSequence.Append(button.DOColor(c, rainbowSpeed)
                .SetEase(Ease.Linear));
        }

        foreach (Color c in invertedColors)
        {
            textColorSequence.Append(buttonText.DOColor(c, rainbowSpeed)
                .SetEase(Ease.Linear));
        }

        material.color = new Color(1, 1, 1);
    }

    private void Start()
    {
        rainbowSequence.SetLoops(-1).Play();
        textColorSequence.SetLoops(-1).Play();
    }

    void PlayRainbow()
    {
        rainbowSequence.Play();
        textColorSequence.Play();
    }

    void SetRainbowSpeedImmediate(float timeScale)
    {
        rainbowSequence.timeScale = timeScale;
        textColorSequence.timeScale = timeScale;
    }

    void SetRainbowSpeedWithTween(float timeScale)
    {
        DOTween.To(() => .1f, x => rainbowSequence.timeScale = x, timeScale, 2f);
        DOTween.To(() => .1f, x => rainbowSequence.timeScale = x, timeScale, 2f);
    }

    void StopRainbow()
    {
        rainbowSequence.Pause();
        textColorSequence.Pause();
    }


    [Button]
    public void OnClick()
    {
        switch (_clicked)
        {
            case 0:
                rainbowSequence.Pause();
                ChangeTextAndIncrementClicked();
                break;
            case 9:
                ChangeTextAndIncrementClicked();
                OnPlayIstEgal?.Invoke();
                break;
            case 10:
                ChangeTextAndIncrementClicked();
                OnStopIstEgal?.Invoke();
                break;
            case 21:
                ChangeTextAndIncrementClicked();
                OnShowHintLouder?.Invoke();
                break;
            case 22:
                ChangeTextAndIncrementClicked();
                OnEraseHint?.Invoke();
                break;
            case 29:
                ChangeTextAndIncrementClicked();
                OnShowHintMath?.Invoke();
                break;
            case 44:
                ChangeTextAndIncrementClicked();
                OnEraseHint?.Invoke();
                break;
            case 45:
                ChangeTextAndIncrementClicked();
                OnEnterAnswer?.Invoke();
                break;
            case 58:
                DisableButtonPermanently?.Invoke();
                StartCoroutine(EndReached());
                break;
            case 100:
                break;
            default:
                ChangeTextAndIncrementClicked();
                break;

        }
    }

    IEnumerator EndReached()
    {
        yield return buttonText.DOText(strings[_clicked], tweenDuration, true, ScrambleMode.All)
            .WaitForCompletion();

        PlayRainbow();
        SetRainbowSpeedWithTween(1f);
        DiscoTween();
        yield return stoneGO.transform.DOMove(sinkingPath.transform.position, 2f)
            .WaitForCompletion();

        _clicked = 100;
        OnEndReached?.Invoke();
    }

    void DiscoTween()
    {
        Sequence disco = DOTween.Sequence();
        foreach (Color c in colors)
        {
            disco.Append(material.DOColor(c, rainbowSpeed / 10)
                .SetEase(Ease.Linear));
        }
        disco.SetLoops(-1)
            .Play();

        DOTween.To(() => disco.timeScale, x => disco.timeScale = x, rainbowSpeed, 2f);
    }

    private void ChangeTextAndIncrementClicked()
    {
        buttonText.DOText(strings[_clicked], tweenDuration, true, ScrambleMode.All)
            .OnPlay(() => { SetRainbowSpeedImmediate(.1f); PlayRainbow();  })
            .OnComplete(StopRainbow);

        _clicked += 1;
        OnChangeText?.Invoke();
    }
}
