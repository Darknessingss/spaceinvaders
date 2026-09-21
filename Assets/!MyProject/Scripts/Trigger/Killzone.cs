using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public sealed class KillZone : MonoBehaviour
{
    [SerializeField] private int _damageToPlayer = 100;

    private PlayerHealth _playerHealth;

    public void Initialize(PlayerHealth playerHealth)
    {
        _playerHealth = playerHealth;
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent<EnemyHealth>(out var enemy))
        {
            return;
        }

        if (_playerHealth != null)
        {
            _playerHealth.TakeDamage(_damageToPlayer);
        }

        enemy.Kill();
    }
}