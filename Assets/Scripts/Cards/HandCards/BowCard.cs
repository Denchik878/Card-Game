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
        }
    }
    private async void DelayedDamage(Card card)
    {
        int currentTurn = GameManager.Instance.enemyTurnCount;
        while (GameManager.Instance.enemyTurnCount != currentTurn + 2 && (GameManager.Instance.State != GameState.PlayerTurn || 
               GameManager.Instance.State != GameState.PlayerAnimation))
        {
            await Awaitable.NextFrameAsync();
        }

        if (card != null)
        {
            card.ChangeHealth(-damage);
        }
        if (crystalAmount == 0)
        {
            await DestroySelf();
        }
    }
}
