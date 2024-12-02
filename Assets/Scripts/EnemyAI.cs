using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyAI : MonoBehaviour
{
    public Spawner spawner;
    public List<Card> activeCards = new();
    private List<Card> disposeCards = new();
    async void Update()
    {
        if (GameManager.Instance.State == GameState.EnemyTurn)
        {
            GameManager.Instance.ChangeState(GameState.EnemyAnimaton);
            foreach (Card card in activeCards)
            {
                await card.Turn();
            }
            activeCards.RemoveAll(card => disposeCards.Contains(card));
            foreach (Card card in disposeCards)
            {
                Destroy(card.gameObject);
            }
            disposeCards.Clear();
            var newCards = spawner.Spawn();
            foreach(Card card in newCards)
            {
                card.OnDeath += DisposeCard;
            }
            activeCards.AddRange(newCards);
            GameManager.Instance.ChangeState(GameState.PlayerTurn);
        }
    }

    private void OnEnable()
    {
        foreach (Card card in activeCards)
        {
            card.OnDeath += DisposeCard;
        }
    }

    private void OnDisable()
    {
        foreach (var card in activeCards)
        {
            card.OnDeath -= DisposeCard;
        }
    }

    public void DisposeCard(Card disposableCard)
    {
        disposeCards.Add(disposableCard);
        disposableCard.OnDeath -= DisposeCard;
    }
}

