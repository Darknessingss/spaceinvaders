using System;

public sealed class ScoreService
{
    public event Action<int> ScoreChanged;

    private int _score;

    public void Add(int amount)
    {
        _score += amount;
        ScoreChanged?.Invoke(_score);
    }

    public int GetScore()
    {
        return _score;
    }
}