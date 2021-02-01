using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    [SerializeField] float _maxDistance = 0.5f;

    Vector3 _pos;
    PlayerController _player;

    private void OnEnable()
    {
        _player = FindObjectOfType<PlayerController>();
        _pos = transform.position;
    }

    void Update()
    {
        if (_player == null) return;

        transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, _maxDistance * Time.deltaTime);
    }
}
