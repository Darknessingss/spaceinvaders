using System;
using UnityEngine;

public sealed class EnemyHealth : MonoBehaviour
{
    public event Action<EnemyHealth> Died;

    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _scoreMin = 20;
    [SerializeField] private int _scoreMax = 50;

    private ScoreService _scoreService;
    private int _currentHealth;
    private bool _isDead;

    public void Initialize(ScoreService scoreService)
    {
        _scoreService = scoreService;
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
        _scoreService.Add(UnityEngine.Random.Range(_scoreMin, _scoreMax + 1));
        Died?.Invoke(this);
        Destroy(gameObject);
    }
}