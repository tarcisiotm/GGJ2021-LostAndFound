using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarObjectController : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _target;

    [SerializeField] private float _deviationFromCenter;
    [SerializeField] private float _sonarSize = 64;
    [SerializeField] private float _maxPlayerDist = 50;

    float _distance;
    float _unitToPixel;

    public Vector3 dir;
    public Vector3 pos;

    private RectTransform _rectTransform;

    void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _unitToPixel = _sonarSize / _maxPlayerDist;
    }

    void Update()
    {
        dir = _target.position - _player.position;
        _distance = Vector3.Distance(_target.position, _player.position);

        if (_distance > _maxPlayerDist) _distance = _maxPlayerDist;

        _rectTransform.anchoredPosition = _distance * _unitToPixel * new Vector2(dir.normalized.x, dir.normalized.y);
        pos = _deviationFromCenter * new Vector2(dir.normalized.x, dir.normalized.y);
    }
}