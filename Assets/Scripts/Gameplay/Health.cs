using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IGetHit
{
    [SerializeField] uint _maxHealthPoints;
    [SerializeField] uint _currentHealth;

    public delegate void OnPointDownCallback(uint damageAmount);
    public event OnPointDownCallback OnDeath;
    public event OnPointDownCallback OnPointDown;

    private bool _isDead;

    public bool IsDead => _isDead;

    void Start()
    {
        _currentHealth = _maxHealthPoints;
    }

    public uint GetCurrentHealth()
    {
        return _currentHealth;
    }

    public uint GetMaxHealthPoints()
    {
        return _maxHealthPoints;
    }

    public void SetCurrentHealth(uint amount)
    {
        _currentHealth = amount;
    }

    public void SetMaxHealthPoints(uint amount)
    {
        _maxHealthPoints = amount;
    }

    public void AddHealth(uint amount)
    {
        _currentHealth += amount;
        if (_currentHealth > _maxHealthPoints) _currentHealth = _maxHealthPoints;
    }

    public void LoseHealth(uint amount)
    {
        _currentHealth -= amount;

        if (!_isDead && _currentHealth <= 0) // what is dead may never die (again)
        {
            _isDead = true;
            _currentHealth = 0;
            OnDeath?.Invoke(_currentHealth);
        }

        OnPointDown?.Invoke(_currentHealth);
    }

    #region Interface Implementation
    void IGetHit.HandleDamage(IHit hitObject)
    {
        LoseHealth(hitObject.GetDamageAmount());
    }
    #endregion Interface Implementation
}