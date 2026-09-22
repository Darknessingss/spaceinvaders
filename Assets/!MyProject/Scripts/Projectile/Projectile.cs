using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public sealed class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed = 8f;
    [SerializeField] private int _damage = 100;
    [SerializeField] private float _lifetime = 3f;
    [SerializeField] private Vector2 _direction = Vector2.up;

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
        if (!other.TryGetComponent<EnemyHealth>(out var enemy))
        {
            return;
        }

        enemy.TakeDamage(_damage);
        Destroy(gameObject);
    }
}