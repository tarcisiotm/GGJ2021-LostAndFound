using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAfterDelay : MonoBehaviour
{
    [SerializeField] private float delay = 2;
    private float _currentTime = 0;

    private void OnEnable()
    {
        _currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime >= delay) gameObject.SetActive(false);
    }
}
