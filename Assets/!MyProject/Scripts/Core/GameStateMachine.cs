using System;

public sealed class GameStateMachine
{
    public event Action<GameState> StateChanged;

    private GameState _current = GameState.Playing;

    public GameState Current => _current;

    public void SetState(GameState state)
    {
        if (_current == state)
        {
            return;
        }

        _current = state;
        StateChanged?.Invoke(state);
    }

    public bool IsPlaying()
    {
        return _current == GameState.Playing;
    }
}