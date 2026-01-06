using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BowCard : Weapon
{
    public int turnsDelay;
    protected override async void Damage(Card enemyCard)
    {
        if (enemyCard.GetComponent<Card>().HPText == null)
        {
            CancelAttack();
            return;
        }
        var args = new EffectArgs();
        args.damage = damage;
        args.turnsDelay = turnsDelay;
        enemyCard.GetComponent<Marked>().AddEffect(args);
        await ChangeCrystalAmount(-1);
        player.FinishTurn();
        if (crystalAmount == 0)
        {
            await DestroySelf();
        }
    }
}
