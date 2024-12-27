using UnityEngine;

public class HealCard : Weapon
{
    public int healAmount;
    public async override Awaitable Turn()
    {
        base.Turn();
        await DestroySelf();
    }
    protected override void Damage(Card enemyCard)
    {
        enemyCard.ChangeHealth(healAmount);
    }
}
