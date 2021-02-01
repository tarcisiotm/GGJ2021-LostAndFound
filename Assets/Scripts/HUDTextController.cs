using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class HUDTextController : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] TextMeshProUGUI m_text;
    [SerializeField] float _fadeDuration = .3f;
    [SerializeField] float _duration = 5f;

    public void SetText(string text)
    {
        m_text.text = text;
        canvasGroup.DOFade(1, _fadeDuration).OnComplete(FadeOut);
    }

    void FadeOut()
    {
        canvasGroup.DOFade(0, _fadeDuration).SetDelay(_duration);
    }
}
