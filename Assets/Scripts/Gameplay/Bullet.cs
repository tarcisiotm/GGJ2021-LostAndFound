using System;
using System.Collections;
using System.Collections.Generic;
using TG.Core;
using UnityEngine;

public class Bullet : MonoBehaviour, IHit, IPoolingItem
{
    private int damage;
    private float _speed;
    private Vector3 _direction;

    private Rigidbody _rigidBody;

    private Vector3 moveVector;

    void Start()
    {
        _rigidBody = GetComponentInChildren<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    // Start is called before the first frame update
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
        if(!collision.gameObject.CompareTag("Player"))
            Destroy(gameObject);
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
