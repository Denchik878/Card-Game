using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Point[] points;
    public List<Card> activeCards = new();
    private void Start()
    {
        foreach (Card card in activeCards)
        {
            card.OnDeath += DisposeCard;
        }
        points = GetComponentsInChildren<Point>();
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
        foreach (Card card in activeCards)
        {
            card.OnDeath -= DisposeCard;
        }
    }

    public void CreateWeapon(Card weapon)
    {
        foreach (Point point in points)
        {
            if (!point.currentCard)
            {
                Card newCard = Instantiate(weapon, point.transform.position, Quaternion.identity);
                newCard.OnDeath += DisposeCard;
                activeCards.Add(newCard);
                point.currentCard = newCard;
            }
        }
    }

    public async void FinishTurn()
    {
        foreach (Card card in activeCards)
        {
            await card.BaseTurn();
            GameManager.Instance.ChangeState(GameState.EnemyTurn);
        }
    }

    private async void DisposeCard(Card disposableCard)
    {
        while (GameManager.Instance.State != GameState.EnemyTurn && GameManager.Instance.State != GameState.EnemyAnimaton)
        {
            await Awaitable.NextFrameAsync();
        }
        activeCards.Remove(disposableCard);
        disposableCard.OnDeath -= DisposeCard;
        Destroy(disposableCard.gameObject);
    }
}