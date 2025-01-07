using UnityEngine;

public class HealCard : Weapon
{
    public int healAmount;
    
    protected override async void Damage(Card enemyCard)
    {
        enemyCard.ChangeHealth(healAmount);
        DestroySelf();
        player.FinishTurn();
    }
}
