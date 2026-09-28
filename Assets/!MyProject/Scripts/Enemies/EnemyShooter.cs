using UnityEngine;

public sealed class EnemyShooter : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _firePoint;

    private GameStateMachine _stateMachine;

    public void Initialize(GameStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    private void Awake()
    {
        if (_firePoint == null)
        {
            _firePoint = transform;
        }
    }

    public void Fire()
    {
        if (_stateMachine != null && !_stateMachine.IsPlaying())
        {
            return;
        }

        if (_projectilePrefab == null)
        {
            return;
        }

        Instantiate(_projectilePrefab, _firePoint.position, _projectilePrefab.transform.rotation);
    }
}