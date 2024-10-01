using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public List<Card> cards;
    void Update()
    {
        if (GameManager.Instance.state == GameState.EnemyTurn)
        {
            foreach(Card card in cards)
            {
                card.Turn();
            }
            GameManager.Instance.state = GameState.PlayerTurn;
        }
    }
}
