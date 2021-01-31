using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PulseUIColor : MonoBehaviour
{
    [SerializeField] float _speed = 2;

    private Image _image;

    void Start()
    {
        _image = GetComponent<Image>();
    }

    void Update()
    {
        _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, Mathf.PingPong(Time.time * _speed, 1));
    }
}
