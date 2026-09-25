using UnityEngine;

public sealed class EnemyDescent : MonoBehaviour
{
    [SerializeField] private float _interval = 3f;
    [SerializeField] private float _step = 0.25f;

    private float _timer;
    private GameStateMachine _stateMachine;

    public void Initialize(GameStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        _timer = _interval;
    }

    private void Awake()
    {
        _timer = _interval;
    }

    private void Update()
    {
        if (_stateMachine != null && !_stateMachine.IsPlaying())
        {
            return;
        }

        _timer -= Time.deltaTime;

        if (_timer > 0f)
        {
            return;
        }

        _timer = _interval;
        transform.position += new Vector3(0f, -_step, 0f);
    }
}