using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
                if(card == null)
                {
                    cards.Remove(card);
                    continue;
                }
                card.Turn();
            }
            GameManager.Instance.state = GameState.PlayerTurn;
        }
    }

    private void OnEnable()
    {
        foreach (Card card in cards)
        {
            card.OnDeath += DisposeCard;
        }
    }

    private void OnDisable()
    {
        foreach (var card in cards)
        {
            card.OnDeath -= DisposeCard;
        }
    }

    public void DisposeCard(Card disposableCard)
    {
        cards.Remove(disposableCard);
        disposableCard.OnDeath -= DisposeCard;
        Destroy(disposableCard.gameObject);
    }
}

