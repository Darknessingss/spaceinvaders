using System;
using UnityEngine;

public sealed class EnemyHealth : MonoBehaviour
{
    public event Action<EnemyHealth> Died;

    [SerializeField] private int _maxHealth = 100;

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

        if (_currentHealth > 0)
        {
            return;
        }

        Kill();
    }

    public void Kill()
    {
        if (_isDead)
        {
            return;
        }

        _isDead = true;
        Died?.Invoke(this);
        Destroy(gameObject);
    }
}