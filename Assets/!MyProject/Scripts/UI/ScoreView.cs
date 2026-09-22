using TMPro;
using UnityEngine;

public sealed class ScoreView : MonoBehaviour
{
    [SerializeField] private TMP_Text textScore;

    private ScoreService _scoreService;

    public void Initialize(ScoreService scoreService)
    {
        _scoreService = scoreService;
        _scoreService.ScoreChanged += OnScoreChanged;
        textScore.text = $"{_scoreService.GetScore()}";
    }

    private void OnDestroy()
    {
        if (_scoreService != null)
        {
            _scoreService.ScoreChanged -= OnScoreChanged;
        }
    }

    private void OnScoreChanged(int score)
    {
        textScore.text = $"{score}";
    }
}