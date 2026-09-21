using UnityEngine;

public sealed class EnemyDescent : MonoBehaviour
{
    [SerializeField] private float _interval = 3f;
    [SerializeField] private float _step = 0.25f;

    private float _timer;

    public void Initialize()
    {
        _timer = _interval;
    }

    private void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer > 0f)
        {
            return;
        }

        _timer = _interval;
        transform.position += new Vector3(0f, -_step, 0f);
    }
}