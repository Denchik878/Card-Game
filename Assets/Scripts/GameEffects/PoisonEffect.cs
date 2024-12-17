using System;
using UnityEngine;

public class PoisonEffect : CardEffect
{
    public int damagePerTurn;


    public override void TakeEffect()
    {
        baseCard.ChangeHealth(-damagePerTurn);
    }
}
