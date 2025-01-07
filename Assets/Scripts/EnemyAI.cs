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

    private void Start()
    {
        foreach (Card card in activeCards)
        {
            card.OnDeath += DisposeCard;
        }
    }

    async void Update()
    {
        if (GameManager.Instance.State == GameState.EnemyTurn)
        {
            GameManager.Instance.ChangeState(GameState.EnemyAnimaton);
            
            foreach (Card card in activeCards)
            {
                await card.BaseTurn();
            }
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
    public async void DisposeCard(Card disposableCard)
    {
        while (GameManager.Instance.State != GameState.PlayerTurn &&
               GameManager.Instance.State != GameState.PlayerAnimation)
        {
            await Awaitable.NextFrameAsync();
        }
        activeCards.Remove(disposableCard);
        disposableCard.OnDeath -= DisposeCard;
        Destroy(disposableCard.gameObject);
    }
}

