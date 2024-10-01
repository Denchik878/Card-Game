using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState state = GameState.Pause;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }
}
public enum GameState
{
    EnemyTurn,
    PlayerTurn,
    Pause,
}