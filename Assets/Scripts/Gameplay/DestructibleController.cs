using System.Collections;
using System.Collections.Generic;
using TG.Core;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class DestructibleController : MonoBehaviour
{
    [Header("Optional")]
    [SerializeField] private GameObject _onDeathPrefab;

    [SerializeField] private GameObject _pickupPrefab;
    [Tooltip("These will be clamped between 0 and 1 (100%)")]
    [SerializeField] [Range(0,1)] private float _minMaxSpawnProbability = 1f;

    private Health _health;

    void OnEnable()
    {
        _health = GetComponentInChildren<Health>();
        _health.OnDeath += HandleDeath;
        _health.OnPointDown += HandleDamageTaken;
    }

    void Start()
    {
    }

    void Update()
    {
    }

    private void HandleDeath(uint damageAmount)
    {
        if (_onDeathPrefab != null)
        {
            var obj = Instantiate(_onDeathPrefab);
            obj.transform.position = transform.position;
            obj.transform.rotation = transform.rotation;
        }

        if (_pickupPrefab != null && Random.Range(0, 1) <= _minMaxSpawnProbability)
        {
            var obj = PoolingManager.I.GetPooledObject(_pickupPrefab);
            if (obj == null) obj = Instantiate(_pickupPrefab);

            obj.transform.position = transform.position;
            obj.SetActive(true);
        }

        gameObject.SetActive(false);
    }

    private void HandleDamageTaken(uint damageAmount)
    {
        if (_health.IsDead) return;

        GetComponent<ChangeColorController>().TakeDamage();
    }
}