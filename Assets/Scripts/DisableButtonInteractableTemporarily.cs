using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

public class DisableButtonInteractableTemporarily : MonoBehaviour
{
    public Button button;
    public float delayToEnable = 2f;

    public void ChangeDelay(float newValue)
    {
        delayToEnable = newValue;
    }

    [Button]
    public void DisableButtonTemporarily()
    {
        StartCoroutine(DisableWithDelay(delayToEnable));
    }

    [Button]
    public void DisableButtonPermanently()
    {
        button.interactable = false;
    }

    IEnumerator DisableWithDelay(float delay)
    {
        button.interactable = false;
        yield return new WaitForSecondsRealtime(delay);
        button.interactable = true;
    }
}
