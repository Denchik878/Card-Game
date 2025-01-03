using UnityEngine;

public class SpellCard : Weapon
{
    public async override Awaitable Turn()
    {
        base.Turn();
        await DestroySelf();
    }

    protected override void Damage(Card enemyCard)
    {
        enemyCard.GetComponent<PoisonEffect>().enabled = true;
        enemyCard.GetComponent<PoisonEffect>().turns = 3;
    }
}
