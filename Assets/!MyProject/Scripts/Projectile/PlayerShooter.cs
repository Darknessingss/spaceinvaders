using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerController))]
public sealed class PlayerShooter : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private float _fireCooldown = 1.25f;

    private float _nextFireTime;

    private void Update()
    {
        if (Keyboard.current == null)
        {
            return;
        }

        if (!Keyboard.current.spaceKey.isPressed)
        {
            return;
        }

        if (Time.time < _nextFireTime)
        {
            return;
        }

        Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
        _nextFireTime = Time.time + _fireCooldown;
    }
}