using UnityEngine;

public class HealCard : Weapon
{
    public int healAmount;
    
    protected override async void Damage(Card enemyCard)
    {
        if (enemyCard.GetComponent<Card>().HPText == null)
        {
            CancelAttack();
            return;
        }
        await ChangeCrystalAmount(-1);
        enemyCard.ChangeHealth(healAmount);
        player.FinishTurn();
        if (crystalAmount == 0)
        {
            await DestroySelf();
        }
    }
}
