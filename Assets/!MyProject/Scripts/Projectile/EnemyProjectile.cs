using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public sealed class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private float _speed = 6f;
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _lifetime = 5f;
    [SerializeField] private Vector2 _direction = Vector2.down;

    private void Start()
    {
        Destroy(gameObject, _lifetime);
    }

    private void Update()
    {
        transform.position += (Vector3)(_direction.normalized * (_speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent<PlayerHealth>(out var playerHealth))
        {
            return;
        }

        playerHealth.TakeDamage(_damage);
        Destroy(gameObject);
    }
}