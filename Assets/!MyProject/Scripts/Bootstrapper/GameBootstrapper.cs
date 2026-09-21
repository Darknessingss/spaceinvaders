using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-1000)]
public sealed class GameBootstrapper : MonoBehaviour
{
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private PlayerHealthView _playerHealthView;

    [SerializeField] private EnemyGridSpawner _enemySpawner;
    [SerializeField] private KillZone _killZone;

    [SerializeField] private WinScreen _winScreen;
    [SerializeField] private LoseScreen _loseScreen;

    private void Start()
    {
        _playerHealth.HealthChanged += _playerHealthView.SetHealth;
        _playerHealth.Died += OnPlayerDied;

        _playerHealthView.Build(_playerHealth.GetMaxHealth());
        _playerHealthView.SetHealth(_playerHealth.GetCurrentHealth());

        _enemySpawner.AllEnemiesDied += OnAllEnemiesDied;
        _enemySpawner.SpawnGrid();

        _winScreen.Hide();
        _loseScreen.Hide();

        _winScreen.RestartRequested += RestartGame;
        _loseScreen.RestartRequested += RestartGame;

        _killZone.Initialize(_playerHealth);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    private void OnPlayerDied()
    {
        _loseScreen.Show();
    }

    private void OnAllEnemiesDied()
    {
        _winScreen.Show();
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}