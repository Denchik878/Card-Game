using System;
using UnityEngine;

public class PoisonEffect : CardEffect
{
    public int damagePerTurn;


    public override void TakeEffect()
    {
        if(turns == 0)
        {
            this.enabled = false;
            return;
        }
        baseCard.ChangeHealth(-damagePerTurn);
        turns--;
    }
}
