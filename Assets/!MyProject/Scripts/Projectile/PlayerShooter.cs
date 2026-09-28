using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerController))]
public sealed class PlayerShooter : MonoBehaviour
{

    [SerializeField] private Transform _firepoint;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private float _fireCooldown = 1f;

    private float _nextFireTime;
    private GameStateMachine _stateMachine;

    public void Initialize(GameStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    private void Update()
    {
        if (_stateMachine != null && !_stateMachine.IsPlaying())
        {
            return;
        }

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

        Instantiate(_projectilePrefab, _firepoint.position, Quaternion.identity);
        _nextFireTime = Time.time + _fireCooldown;
    }
}