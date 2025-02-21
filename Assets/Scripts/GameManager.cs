using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int enemyTurnCount;
    public int playerTurnCount;
    public GameState State = GameState.PlayerTurn;
    public event Action OnStateChanged;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    public void ChangeState(GameState newState)
    {
        State = newState;
        OnStateChanged?.Invoke();
    }
}
public enum GameState
{
    EnemyTurn,
    PlayerTurn,
    EnemyAnimaton,
    PlayerAnimation,
    Pause,
}