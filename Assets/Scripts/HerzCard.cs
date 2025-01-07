using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerzCard : Weapon
{
    protected override async void Damage(Card enemyCard)
    {
        enemyCard.ChangeHealth(-enemyCard.maxHealth);
        ChangeHealth(-enemyCard.damage);
        player.FinishTurn();
    }
}
