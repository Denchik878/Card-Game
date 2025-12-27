using System;
using UnityEngine;

public class PoisonEffect : CardEffect
{
    public int damagePerTurn;
    

    public override void TakeEffect()
    {
        foreach (var effect in argsList)
        {
            if (effect.turnsDura == 0)
            {
                RemoveEffect(effect);
                return;
            }
            baseCard.ChangeHealth(-effect.damage);
            effect.turnsDura--;
        }
    }
}
