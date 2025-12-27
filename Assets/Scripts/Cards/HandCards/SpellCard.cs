using UnityEngine;

public class SpellCard : Weapon
{
    protected override async void Damage(Card enemyCard)
    {
        if (enemyCard.GetComponent<Card>().HPText == null)
        {
            CancelAttack();
            return;
        }

        var args = new EffectArgs();
        args.damage = damage;
        args.turnsDura = 3;
        enemyCard.GetComponent<PoisonEffect>().AddEffect(args);
        await ChangeCrystalAmount(-1);
        player.FinishTurn();
        if (crystalAmount == 0)
        {
            await DestroySelf();
        }
    }
}
