using UnityEngine;

public class SpellCard : Weapon
{
    public int poisonDuration;
    protected override async void Damage(Card enemyCard)
    {
        if (enemyCard.GetComponent<Card>().HPText == null)
        {
            CancelAttack();
            return;
        }

        var args = new EffectArgs();
        args.damage = damage;
        args.turnsDura = poisonDuration;
        enemyCard.GetComponent<PoisonEffect>().AddEffect(args);
        await ChangeCrystalAmount(-1);
        player.FinishTurn();
        if (crystalAmount == 0)
        {
            await DestroySelf();
        }
    }
}
