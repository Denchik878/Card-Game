using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerzCard : Weapon
{
    protected override async void Damage(Card enemyCard)
    {
        if (enemyCard.GetComponent<BossCard>() != null)
        {
            CancelAttack();
            return;
        }
        enemyCard.ChangeHealth(-enemyCard.maxHealth);
        ChangeHealth(-enemyCard.damage);
        player.FinishTurn();
    }
}
