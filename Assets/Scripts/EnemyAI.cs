using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (GameManager.Instance.state == GameState.EnemyTurn)
        {
            //Что то там
            GameManager.Instance.state = GameState.PlayerTurn;
        }
    }
}
