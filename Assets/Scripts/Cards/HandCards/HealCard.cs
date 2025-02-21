using UnityEngine;

public class HealCard : Weapon
{
    public int healAmount;
    
    protected override async void Damage(Card enemyCard)
    {
        ChangeCrystalAmount(-1);
        enemyCard.ChangeHealth(healAmount);
        player.FinishTurn();
        if (crystalAmount == 0)
        {
            await DestroySelf();
        }
    }
}
