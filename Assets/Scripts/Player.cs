using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Point[] points;
    public List<Card> activeCards = new();
    private HerzCard herz;
    private void Start()
    {
        points = GetComponentsInChildren<Point>();
        foreach (Card card in activeCards)
        {
            card.OnDeath += DisposeCard;
        }

        herz = activeCards.OfType<HerzCard>().FirstOrDefault();
        herz.OnDeath += GameOver;
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
        herz.OnDeath -= GameOver;
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
                newCard.currentPoint = point;
                break;
            }
        }
    }
    public async void FinishTurn()
    {
        foreach (Card card in activeCards)
        {
            await card.BaseTurn();
        }
        GameManager.Instance.ChangeState(GameState.EnemyTurn);
        GameManager.Instance.playerTurnCount++;
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

    private void GameOver(Card stalin)
    {
        print("Ты лох");
        SceneManager.LoadScene("Sucker");
    }
}