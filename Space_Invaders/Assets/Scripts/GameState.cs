using System;
using UniRx;
using UnityEngine;

public enum GameStateType
{
    None = 0,
    Menu = 1,
    Gameplay = 2,
    Pause = 3,
    LoseGame = 4
}

public class GameState
{
    public ReactiveProperty<GameStateType> State = new ReactiveProperty<GameStateType>();

    public event Action OnGameRestarted;

    public GameState()
    {
        State.Subscribe(_ => Debug.Log($"<color=red> Change state to {_} </color>"));
    }
    public void RestartLevel()
    {
        OnGameRestarted?.Invoke();
    }
    
}
