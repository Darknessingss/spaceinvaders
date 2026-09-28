using System.Collections.Generic;
using UnityEngine;

public sealed class EnemyFireCoordinator : MonoBehaviour
{
    [SerializeField] private float _minInterval = 2f;
    [SerializeField] private float _maxInterval = 6f;

    private readonly List<EnemyShooter> _shooters = new();
    private float _timer;
    private GameStateMachine _stateMachine;

    public void Initialize(GameStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        ResetTimer();
    }

    public void Register(EnemyShooter shooter)
    {
        if (shooter == null)
        {
            return;
        }

        _shooters.Add(shooter);
    }

    public void Unregister(EnemyShooter shooter)
    {
        if (shooter == null)
        {
            return;
        }

        _shooters.Remove(shooter);
    }

    private void Awake()
    {
        ResetTimer();
    }

    private void Update()
    {
        if (_stateMachine != null && !_stateMachine.IsPlaying())
        {
            return;
        }

        Cleanup();

        if (_shooters.Count == 0)
        {
            return;
        }

        _timer -= Time.deltaTime;

        if (_timer > 0f)
        {
            return;
        }

        FireRandom();
        ResetTimer();
    }

    private void Cleanup()
    {
        for (int i = _shooters.Count - 1; i >= 0; i--)
        {
            if (_shooters[i] == null)
            {
                _shooters.RemoveAt(i);
            }
        }
    }

    private void FireRandom()
    {
        if (_shooters.Count == 0)
        {
            return;
        }

        int index = Random.Range(0, _shooters.Count);
        _shooters[index].Fire();
    }

    private void ResetTimer()
    {
        _timer = Random.Range(_minInterval, _maxInterval);
    }
}