using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Spawner spawner;
    public List<Card> cards;
    async void Update()
    {
        if (GameManager.Instance.state == GameState.EnemyTurn)
        {
            GameManager.Instance.state = GameState.EnemyAnimaton;
            foreach (Card card in cards)
            {
                if(card == null)
                {
                    cards.Remove(card);
                    continue;
                }
                await card.Turn();
            }
            var newCards = spawner.Spawn();
            foreach(Card card in newCards)
            {
                card.OnDeath += DisposeCard;
            }
            cards.AddRange(newCards);
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

