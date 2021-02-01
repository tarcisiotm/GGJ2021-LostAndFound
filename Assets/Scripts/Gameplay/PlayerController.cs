using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TG.Core;
using UnityEngine.InputSystem;
using System;
using DG.Tweening;

public class PlayerController : MonoBehaviour, IGetHit
{
    [Header("Settings")]
    [SerializeField] float _speed = 1;
    [SerializeField] float _angleSpeed = 2;

    [Header("Shoot")]
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _muzzle;
    [SerializeField] private float _bulletSpeed;

    [Header("Audio")]
    [SerializeField] private AudioClip _engineSound;
    [SerializeField] private float _engineSoundMinVolume = 0;
    [SerializeField] private float _engineSoundMaxVolume = .3f;
    [Space]
    [SerializeField] private CanvasGroup _endCanvas;


    public bool _isMoving = false;

    private float _currentSpeed;

    private AudioSource _audioSource;

    private Vector3 _inputDirection;

    private Rigidbody _rigidBody;

    private PlayerControls _controls;

    private Health _health;

    [Header("Debug")]
    [SerializeField] private bool _canShoot = false;
    [SerializeField] private int _mineral = 0;

    private void Awake()
    {
        _rigidBody = GetComponentInChildren<Rigidbody>();
        
        _controls = new PlayerControls();

        _health = GetComponentInChildren<Health>();

        _audioSource = GetComponentInChildren<AudioSource>();
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
        _inputDirection = Vector3.ClampMagnitude(_inputDirection, 1f); // prevent diagonals from being faster
        _audioSource.DOKill(false);
        _audioSource.DOFade(_engineSoundMaxVolume, .3f);

        _isMoving = true;
    }

    private void OnCancelled(InputAction.CallbackContext obj)
    {
        _currentSpeed = 0; // TODO Ease out speed
        _audioSource.DOKill(false);
        _audioSource.DOFade(_engineSoundMinVolume, .3f);

        _rigidBody.angularVelocity = Vector3.zero;
        _rigidBody.velocity = Vector3.zero;

        _isMoving = false;
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
    }

    private void FixedUpdate()
    {
        HandleRotation();
        HandleMovement();
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
        if (!_isMoving) return;

        float angle = Mathf.Atan2(_inputDirection.y, _inputDirection.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(0, 0, angle)), _angleSpeed * Time.deltaTime);
    }

    public void HandleMineral()
    {
        _mineral++;

        if(_mineral >= 3)
        {
            _endCanvas.gameObject.SetActive(true);
            _endCanvas.DOFade(1, 3);
        }
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