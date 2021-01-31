using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] uint _maxHealthPoints;
    [SerializeField] uint _currentHealth;

    // Start is called before the first frame update
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
        if (amount > _currentHealth)
            _currentHealth = 0;
        else
            _currentHealth -= amount;
    }

    public bool IsDead()
    {
        return _currentHealth == 0;
    }
}
