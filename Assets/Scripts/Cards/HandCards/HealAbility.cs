using UnityEngine;

public class HealAbility : Weapon
{
    public int healAmount;

    protected override void Damage(Card enemyCard)
    {
        if (enemyCard.HPText == null)
        {
            CancelAttack();
            return;
        }
        enemyCard.ChangeHealth(healAmount);
        player.FinishTurn();
    }
}
