using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAfterDelay : MonoBehaviour
{
    [SerializeField] private float delay = 2;
    [SerializeField] private bool oneShot = false;
    private float _currentTime = 0;
    private bool _waiting = true;
    private void OnEnable()
    {
        if (oneShot && _currentTime != 0)
        {
            _waiting = false;
            return;
        }
        _currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_waiting) return;
        _currentTime += Time.deltaTime;
        if (_currentTime >= delay) gameObject.SetActive(false);
    }
}
