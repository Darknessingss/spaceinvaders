using System;
using UnityEngine;

public sealed class PlayerHealth : MonoBehaviour
{
    public event Action<int> HealthChanged;
    public event Action Died;

    [SerializeField] private int _maxHealth = 300;

    private int _currentHealth;
    private bool _isDead;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (_isDead)
        {
            return;
        }

        _currentHealth -= amount;

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            _isDead = true;
            HealthChanged?.Invoke(_currentHealth);
            Died?.Invoke();
            return;
        }

        HealthChanged?.Invoke(_currentHealth);
    }

    public int GetMaxHealth()
    {
        return _maxHealth;
    }

    public int GetCurrentHealth()
    {
        return _currentHealth;
    }
}