using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerzCard : Weapon
{
    public override async Awaitable Turn()
    {
        base.Turn();
        GameManager.Instance.ChangeState(GameState.EnemyTurn);
    }

    protected override void Damage(Card enemyCard)
    {
        enemyCard.ChangeHealth(-enemyCard.maxHealth);
        ChangeHealth(-enemyCard.damage);
    }
}
