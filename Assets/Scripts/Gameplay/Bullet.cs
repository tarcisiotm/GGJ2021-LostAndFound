using System;
using System.Collections;
using System.Collections.Generic;
using TG.Core;
using UnityEngine;

public class Bullet : MonoBehaviour, IHit, IPoolingItem
{
    [SerializeField] private GameObject _onImpactPrefab;

    private uint _damage = 1;
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
        var target = collision.gameObject.GetComponentInParent<IGetHit>();

        if (_onImpactPrefab != null)
        {
            var obj = Instantiate(_onImpactPrefab);
            obj.transform.position = collision.contacts[0].point;
            obj.transform.rotation = Quaternion.LookRotation(collision.contacts[0].normal);
        }

        if (target != null)
        {
            target.HandleDamage(this);
        }
        gameObject.SetActive(false);
    }
    
    void IPoolingItem.Reset()
    {
        UpdateSpeed(_speed);
    }

    uint IHit.GetDamageAmount()
    {
        return _damage;
    }
}