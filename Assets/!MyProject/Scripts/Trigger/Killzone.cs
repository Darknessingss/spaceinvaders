using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public sealed class KillZone : MonoBehaviour
{
    [SerializeField] private int _damageToPlayer = 1;

    private PlayerHealth _playerHealth;
    private GameStateMachine _stateMachine;

    public void Initialize(PlayerHealth playerHealth, GameStateMachine stateMachine)
    {
        _playerHealth = playerHealth;
        _stateMachine = stateMachine;
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_stateMachine != null && !_stateMachine.IsPlaying())
        {
            return;
        }

        if (!other.TryGetComponent<EnemyHealth>(out var enemy))
        {
            return;
        }

        if (_playerHealth != null)
        {
            _playerHealth.TakeDamage(_damageToPlayer);
        }

        enemy.KillByZone();
    }
}