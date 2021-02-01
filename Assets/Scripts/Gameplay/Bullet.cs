using System;
using System.Collections;
using System.Collections.Generic;
using TG.Core;
using UnityEngine;

public class Bullet : MonoBehaviour, IHit, IPoolingItem
{
    [SerializeField] private GameObject _onImpactPrefab;
    [SerializeField] private float _onFireAudioVolume = .3f;
    [SerializeField] private float _onImpactAudioVolume = .3f;
    [SerializeField] private AudioClip _onFireAudio;
    [SerializeField] private AudioClip _onImpactAudio;
    
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
            var obj = PoolingManager.I.GetPooledObject(_onImpactPrefab); //Instantiate(_onImpactPrefab);
            obj.transform.position = collision.contacts[0].point;
            obj.transform.rotation = Quaternion.LookRotation(collision.contacts[0].normal);
            obj.SetActive(true);
        }

        if (target != null)
        {
            target.HandleDamage(this);
        }

        if (_onImpactAudio != null) AudioManager.I.CreateOneShot(_onImpactAudio, collision.contacts[0].point, _onImpactAudioVolume);
                
        gameObject.SetActive(false);
    }
    
    void IPoolingItem.Reset()
    {
        UpdateSpeed(_speed);
        if (_onFireAudio != null) AudioManager.I.CreateOneShot(_onFireAudio, transform.position + UnityEngine.Random.insideUnitSphere * .05f, _onFireAudioVolume);
    }

    uint IHit.GetDamageAmount()
    {
        return _damage;
    }
}