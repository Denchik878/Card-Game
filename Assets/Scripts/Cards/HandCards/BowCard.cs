using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BowCard : Weapon
{
    private int delay;
    private List<Card> cardsToDamage = new();

    protected async override Awaitable Turn()
    {
        if (cardsToDamage.Count != 0)
        {
            foreach (Card card in cardsToDamage)
            {
                card.ChangeHealth(-damage);
            }
            cardsToDamage.Clear();
            
        }
    }
    
    protected override async void Damage(Card enemyCard)
    {
        DelayedDamage(enemyCard);
        player.FinishTurn();
        ChangeCrystalAmount(-1);
        if (crystalAmount == 0)
        {
            currentPoint.currentCard = null;
            var sprites = GetComponentsInChildren<SpriteRenderer>();
            foreach (var sprite in sprites)
            {
                sprite.enabled = false;
            }
            GetComponent<Collider2D>().enabled = false;
            GetComponentInChildren<TMP_Text>().enabled = false;
            return;
        }
    }
    private async void DelayedDamage(Card card)
    {
        while (GameManager.Instance.State == GameState.PlayerTurn || GameManager.Instance.State == GameState.PlayerAnimation)
        {
            await Awaitable.NextFrameAsync();
        }
        
        while (GameManager.Instance.State == GameState.EnemyTurn || GameManager.Instance.State == GameState.EnemyAnimaton)
        {
            await Awaitable.NextFrameAsync();
        }
        cardsToDamage.Add(card);
        if (crystalAmount == 0)
        {
            DestroySelf();
        }
    }
}
