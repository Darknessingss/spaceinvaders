using UnityEngine;

public sealed class EnemyDescent : MonoBehaviour
{
    [SerializeField] private float _interval = 3f;
    [SerializeField] private float _step = 0.25f;

    [SerializeField] private float _sideInterval = 2f;
    [SerializeField] private float _sideStep = 0.25f;

    private float _downTimer;
    private float _sideTimer;
    private int _sideDirection = -1;
    private GameStateMachine _stateMachine;

    public void Initialize(GameStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        _downTimer = _interval;
        _sideTimer = _sideInterval;
    }

    private void Awake()
    {
        _downTimer = _interval;
        _sideTimer = _sideInterval;
    }

    private void Update()
    {
        if (_stateMachine != null && !_stateMachine.IsPlaying())
        {
            return;
        }

        TickDown();
        TickSide();
    }

    private void TickDown()
    {
        _downTimer -= Time.deltaTime;

        if (_downTimer > 0f)
        {
            return;
        }

        _downTimer = _interval;
        transform.position += new Vector3(0f, -_step, 0f);
    }

    private void TickSide()
    {
        _sideTimer -= Time.deltaTime;

        if (_sideTimer > 0f)
        {
            return;
        }

        _sideTimer = _sideInterval;

        transform.position += new Vector3(_sideDirection * _sideStep, 0f, 0f);
        _sideDirection = -_sideDirection;
    }
}