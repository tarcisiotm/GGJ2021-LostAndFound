using System;
using System.Collections;
using System.Collections.Generic;
using TG.Core;
using UnityEngine;

public class Bullet : MonoBehaviour, IHit, IPoolingItem
{
    private int _damage;
    private float _speed;
    private Vector3 _direction;
    private Rigidbody _rigidBody;
    private Vector3 moveVector;

    void Start()
    {
        _rigidBody = GetComponentInChildren<Rigidbody>();
    }
    
    void Update()
    {
        Movement();
    }
    
    private void Movement()
    {
        var pos = _rigidBody.position + _direction * _speed * Time.deltaTime;
        _rigidBody.MovePosition(pos);
    }

    public void UpdateDirection(Vector3 p_dir)
    {
        _direction = p_dir;
    }

    public void UpdateSpeed(float p_speed)
    {
        _speed = p_speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            gameObject.SetActive(false);
    }
    
    void IPoolingItem.Reset()
    {
        UpdateSpeed(_speed);
    }

    int IHit.GetDamageAmmount()
    {
        return 0;
    }
}
