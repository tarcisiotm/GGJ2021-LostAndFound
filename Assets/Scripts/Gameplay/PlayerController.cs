using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TG.Core;
using UnityEngine.InputSystem;
using System;

public class PlayerController : MonoBehaviour, IGetHit
{
    [Header("Settings")]
    [SerializeField] float _speed = 1;
    [SerializeField] float _angleSpeed = 2;

    [Header("Shoot")]
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _muzzle;
    [SerializeField] private float _bulletSpeed;

    private float _currentSpeed;

    private Vector3 _inputDirection;

    private Rigidbody _rigidBody;

    private PlayerControls _controls;

    private Health _health;

    [Header("Debug")]
    [SerializeField] private bool _canShoot = false;
    
    private void Awake()
    {
        _rigidBody = GetComponentInChildren<Rigidbody>();
        
        _controls = new PlayerControls();

        _health = GetComponentInChildren<Health>();
    }

    private void OnEnable()
    {
        _controls.Gameplay.Enable();
        _controls.Gameplay.Move.performed += OnMove;
        _controls.Gameplay.Move.canceled += OnCancelled;
        _controls.Gameplay.Shoot.performed += Shoot;
    }

    private void OnDisable()
    {
        _controls.Gameplay.Move.performed -= OnMove;
        _controls.Gameplay.Move.canceled -= OnCancelled;
        _controls.Gameplay.Shoot.performed -= Shoot;
        _controls.Gameplay.Disable();
    }

    private void OnMove(InputAction.CallbackContext obj)
    {
        _currentSpeed = _speed; // TODO Ease in speed?
        _inputDirection = obj.ReadValue<Vector2>();
    }

    private void OnCancelled(InputAction.CallbackContext obj)
    {
        _currentSpeed = 0; // TODO Ease out speed
    }

    private void Shoot(InputAction.CallbackContext ctx)
    {
        if (!_canShoot) return; // we could only subscribe to the shoot method when this is enabled but this is probably simpler

        var bullet = PoolingManager.I.GetPooledObject<Bullet>(_bulletPrefab);
        bullet.transform.position = _muzzle.position;
        bullet.transform.rotation = _muzzle.rotation;

        bullet.UpdateDirection(_muzzle.right);
        bullet.UpdateSpeed(_speed + _bulletSpeed);

        bullet.gameObject.SetActive(true);
    }

    private void Update()
    {
        HandleInput();
       
#if UNITY_EDITOR
        if (Keyboard.current.spaceKey.wasPressedThisFrame) _health.LoseHealth(1);
        if (_health.IsDead) gameObject.SetActive(false);
#endif
    }

    private void FixedUpdate()
    {
        HandleMovement();
        HandleRotation();
    }

    private void HandleInput()
    {
         _inputDirection = Vector3.ClampMagnitude(_inputDirection, 1f); // prevent diagonals from being faster
    }

    private void HandleMovement()
    {
        var pos = _rigidBody.position + _inputDirection * _currentSpeed * Time.deltaTime;
        _rigidBody.MovePosition(pos);
    }

    private void HandleRotation()
    {
        /* 
        Vector3 mousePos = _controls.Gameplay.Rotate.ReadValue<Vector2>();
        mousePos.z = 10;

        var objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;
        */

        float angle = Mathf.Atan2(_inputDirection.y, _inputDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(0, 0, angle)), _angleSpeed * Time.deltaTime);
    }

    #region Interface Implementation
    void IGetHit.HandleDamage(IHit hitObject)
    {
        _health.LoseHealth(hitObject.GetDamageAmount());
    }
    #endregion Interface Implementation

    public void SetCanShoot(bool canShoot)
    {
        _canShoot = canShoot;
    }
}