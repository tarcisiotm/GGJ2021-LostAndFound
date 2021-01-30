﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IHit, IGetHit
{
    [Header("Settings")]
    [SerializeField] float _speed = 1;
    [SerializeField] float _angleSpeed = 2;

    [Header("Shoot")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform muzzle;
    [SerializeField] private float bulletSpeed;

    private Vector3 _inputDirection;
    
    private Rigidbody _rigidBody;

    private PlayerControls _controls;

    

    private void Awake()
    {
        _rigidBody = GetComponentInChildren<Rigidbody>();
        
        _controls = new PlayerControls();

        _controls.Gameplay.Move.performed += context => _inputDirection = context.ReadValue<Vector2>();
        _controls.Gameplay.Move.canceled += context => _inputDirection = Vector3.zero;

        _controls.Gameplay.Shoot.performed += context => Shoot();
        
        
    }

    private void OnEnable()
    {
        _controls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        _controls.Gameplay.Disable();
    }

    private void Update()
    {
        HandleInput();
        HandleMovement();
        HandleRotation();
    }

    private void HandleInput()
    {
         _inputDirection = Vector3.ClampMagnitude(_inputDirection, 1f); // prevent diagonals from being faster
    }

    private void HandleMovement()
    {
        var pos = _rigidBody.position + _inputDirection * _speed * Time.deltaTime;
        _rigidBody.MovePosition(pos);
        // TODO ease out movement - no input
    }

    private void HandleRotation()
    {
        Vector3 mousePos = _controls.Gameplay.Rotate.ReadValue<Vector2>();
        mousePos.z = 10;

        var objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(0, 0, angle)), _angleSpeed * Time.deltaTime);
    }

    private void Shoot()
    {
        GameObject g_bullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
        Bullet bullet = g_bullet.GetComponent<Bullet>();
        bullet.UpdateDirection(muzzle.right);
        bullet.UpdateSpeed(bulletSpeed);

    }

    #region Interface Implementation
    void IGetHit.HandleDamage(IHit hitObject)
    {
        // healthPoints - hitObject.GetDamageAmmount();
    }

    int IHit.GetDamageAmmount()
    {
        // return current damage ammount
        return 0;
    }
    #endregion Interface Implementation
}