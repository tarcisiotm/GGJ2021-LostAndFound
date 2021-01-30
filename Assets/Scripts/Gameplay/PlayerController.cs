﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float _speed = 1;
    [SerializeField] float _angleSpeed = 2;

    private Vector3 _inputDirection;

    private Rigidbody _rigidBody;

    private void Awake()
    {
        _rigidBody = GetComponentInChildren<Rigidbody>();
    }

    private void Update()
    {
        HandleInput();
        HandleMovement();
        HandleRotation();
    }

    private void HandleInput()
    {
        var hor = Input.GetAxisRaw("Horizontal");
        var ver = Input.GetAxisRaw("Vertical");

        _inputDirection = Vector3.zero;
        _inputDirection.x = Input.GetAxis("Horizontal");
        _inputDirection.y = Input.GetAxis("Vertical");
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
        var mousePos = Input.mousePosition;
        mousePos.z = 10;

        var objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(0, 0, angle)), _angleSpeed * Time.deltaTime);
    }
}