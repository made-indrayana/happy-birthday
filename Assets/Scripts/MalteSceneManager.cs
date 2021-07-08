using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using NaughtyAttributes;
using FMODUnity;

public class MalteSceneManager : MonoBehaviour
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
    public GameObject raisePath;
    public GameObject firstPath;
    public GameObject[] gos;

    [Header("Debug")]
    [SerializeField] int _clicked = 0;

    private Sequence rainbowSequence;
    private Sequence textColorSequence;
    [SerializeField]private float rainbowSpeed;

    private void Awake()
    {
        rainbowSequence = DOTween.Sequence();
        textColorSequence = DOTween.Sequence();
        
        foreach(Color c in colors)
        {
            rainbowSequence.Append(button.DOColor(c, rainbowSpeed).SetEase(Ease.Linear));
        }

        foreach(Color c in invertedColors)
        {
            textColorSequence.Append(buttonText.DOColor(c, rainbowSpeed).SetEase(Ease.Linear));
        }

        material.color = new Color(1, 1, 1);
    }

    private void Start()
    {
        rainbowSequence.SetLoops(-1).Play();
        textColorSequence.SetLoops(-1).Play();
    }

    void PlayRainbow(float timeScale)
    {
        rainbowSequence.Play();
        textColorSequence.Play();
        rainbowSequence.timeScale = timeScale;
        textColorSequence.timeScale = timeScale;
    }

    void StopRainbow()
    {
        rainbowSequence.Pause();
        textColorSequence.Pause();
    }


    [Button]
    public void OnClick()
    {
        if (_clicked == 0)
        {
            rainbowSequence.Pause();
            ChangeTextAndIncrementClicked();
        }
        else if (_clicked < 44)
        {
            ChangeTextAndIncrementClicked();
        }

        else if (_clicked == 44)
        {
            buttonText.DOText(strings[_clicked], tweenDuration, true, ScrambleMode.All).OnPlay(() => { PlayRainbow(1f); DiscoTween(); }).OnComplete(()=> emitter.Play());
            stoneGO.transform.DOMove(raisePath.transform.position, 2f).OnComplete(MoveToFirst);
            _clicked = 100;
        }
    }

    void MoveToFirst()
    {
        stoneGO.transform.DOMove(firstPath.transform.position, 2f).SetEase(Ease.Linear).OnComplete(StoneTween);
    }

    void StoneTween()
    {
        Sequence stone = DOTween.Sequence();

        foreach (GameObject go in gos)
        {
            stone.Append(stoneGO.transform.DOMove(go.transform.position, 2f).SetEase(Ease.Linear));
        }
        stone.SetLoops(-1).Play();
    }

    void DiscoTween()
    {
        Sequence disco = DOTween.Sequence();
        foreach (Color c in colors)
        {
            disco.Append(material.DOColor(c, rainbowSpeed).SetEase(Ease.Linear));
        }
        disco.SetLoops(-1).Play();
    }

    private void ChangeTextAndIncrementClicked()
    {
        buttonText.DOText(strings[_clicked], tweenDuration, true, ScrambleMode.All).OnPlay(() => PlayRainbow(.1f)).OnComplete(StopRainbow);
        _clicked += 1;
    }
}
