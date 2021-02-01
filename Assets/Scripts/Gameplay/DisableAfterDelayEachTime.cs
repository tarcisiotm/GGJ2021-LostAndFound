using UnityEngine;

public class DisableAfterDelayEachTime : MonoBehaviour
{
    [SerializeField] private float delay = 2;

    private float _currentTime = 0;

    private void OnEnable()
    {
        _currentTime = 0;
    }

    void Update()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime >= delay) gameObject.SetActive(false);
    }
}