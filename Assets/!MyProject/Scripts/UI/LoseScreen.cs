using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class LoseScreen : MonoBehaviour
{
    public event Action RestartRequested;

    [SerializeField] private GameObject _root;
    [SerializeField] private TMP_Text _scoreLabel;
    [SerializeField] private Button _restartButton;

    private ScoreService _scoreService;

    private void Awake()
    {
        _restartButton.onClick.AddListener(() => RestartRequested?.Invoke());
        _root.SetActive(false);
    }

    public void Initialize(ScoreService scoreService)
    {
        _scoreService = scoreService;
    }

    public void Show()
    {
        _scoreLabel.text = $"Score: {_scoreService.GetScore()}";
        _root.SetActive(true);
    }

    public void Hide()
    {
        _root.SetActive(false);
    }
}