using System;
using UnityEngine;

public sealed class EnemyGridSpawner : MonoBehaviour
{
    public event Action AllEnemiesDied;

    [SerializeField] private BoxCollider2D _spawnArea;
    [SerializeField] private EnemyHealth _enemyPrefab;
    [SerializeField] private int _columns = 8;
    [SerializeField] private int _rows = 4;

    private int _aliveCount;
    private ScoreService _scoreService;

    public void Initialize(ScoreService scoreService)
    {
        _scoreService = scoreService;
    }

    public void SpawnGrid()
    {
        Bounds bounds = _spawnArea.bounds;
        Vector3 center = bounds.center;
        float areaWidth = bounds.size.x;
        float areaHeight = bounds.size.y;

        float stepX = _columns > 1 ? areaWidth / (_columns - 1) : 0f;
        float stepY = _rows > 1 ? areaHeight / (_rows - 1) : 0f;

        Vector3 start = center - new Vector3(areaWidth * 0.5f, areaHeight * 0.5f, 0f);

        for (int row = 0; row < _rows; row++)
        {
            for (int col = 0; col < _columns; col++)
            {
                Vector3 position = start + new Vector3(col * stepX, row * stepY, 0f);

                var enemy = Instantiate(_enemyPrefab, position, Quaternion.identity);
                enemy.Initialize(_scoreService);
                enemy.Died += OnEnemyDied;
                enemy.GetComponent<EnemyDescent>().Initialize();

                _aliveCount++;
            }
        }
    }

    private void OnEnemyDied(EnemyHealth enemy)
    {
        enemy.Died -= OnEnemyDied;
        _aliveCount--;

        if (_aliveCount <= 0)
        {
            AllEnemiesDied?.Invoke();
        }
    }
}