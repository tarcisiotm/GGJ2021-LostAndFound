using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FadeLight : MonoBehaviour
{
    [SerializeField] private float _duration;

    public void DoFadeLight()
    {
        var light = GetComponent<Light>();
        var intensity = light.intensity;
        light.intensity = 0;
        gameObject.SetActive(true);
        light.DOIntensity(intensity, _duration);
    }
}
