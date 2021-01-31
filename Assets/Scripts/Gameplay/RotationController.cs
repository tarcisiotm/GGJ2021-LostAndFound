using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour
{
    [SerializeField] private Vector2 _minMaxSpeed;
    [SerializeField] private bool _startWithRandomRotation = true;

    [Header("Debug")]
    [SerializeField] private Vector3 _randomDir;

    void Start()
    {
        _randomDir = new Vector3
            (
                Random.Range(_minMaxSpeed.x, _minMaxSpeed.y),
                Random.Range(_minMaxSpeed.x, _minMaxSpeed.y),
                Random.Range(_minMaxSpeed.x, _minMaxSpeed.y)
            );

        if (_startWithRandomRotation) transform.rotation = Random.rotation;
    }

    void Update()
    {
        transform.Rotate(_randomDir * Time.deltaTime);
    }
}