using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-1000)]
public sealed class GameBootstrapper : MonoBehaviour
{
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private PlayerHealthView _playerHealthView;

    [SerializeField] private EnemyGridSpawner _enemySpawner;
    [SerializeField] private KillZone _killZone;

    [SerializeField] private PlayerController _playerController;
    [SerializeField] private PlayerShooter _playerShooter;

    [SerializeField] private ScoreView _scoreView;
    [SerializeField] private WinScreen _winScreen;
    [SerializeField] private LoseScreen _loseScreen;

    private ScoreService _scoreService;
    private GameStateMachine _stateMachine;

    private void Awake()
    {
        _scoreService = new ScoreService();
        _stateMachine = new GameStateMachine();
    }

    private void Start()
    {
        _playerHealth.HealthChanged += _playerHealthView.SetHealth;
        _playerHealth.Died += OnPlayerDied;

        _playerHealthView.Build(_playerHealth.GetMaxHealth());
        _playerHealthView.SetHealth(_playerHealth.GetCurrentHealth());

        _scoreView.Initialize(_scoreService);
        _winScreen.Initialize(_scoreService);
        _loseScreen.Initialize(_scoreService);

        _enemySpawner.Initialize(_scoreService, _stateMachine);
        _enemySpawner.AllEnemiesDied += OnAllEnemiesDied;
        _enemySpawner.SpawnGrid();

        _playerController.Initialize(_stateMachine);
        _playerShooter.Initialize(_stateMachine);

        _winScreen.Hide();
        _loseScreen.Hide();

        _winScreen.RestartRequested += RestartGame;
        _loseScreen.RestartRequested += RestartGame;

        _killZone.Initialize(_playerHealth, _stateMachine);
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
        if (!_stateMachine.IsPlaying())
        {
            return;
        }

        _stateMachine.SetState(GameState.Lose);
        _loseScreen.Show();
    }

    private void OnAllEnemiesDied()
    {
        if (!_stateMachine.IsPlaying())
        {
            return;
        }

        _stateMachine.SetState(GameState.Win);
        _winScreen.Show();
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}