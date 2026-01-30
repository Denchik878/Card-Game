using System;
using System.Linq;
using UnityEngine;

public class PoisonEffect : CardEffect
{
    public override void TakeEffect()
    {
        foreach (var effect in argsList)
        {
            baseCard.ChangeHealth(-effect.damage);
            effect.turnsDura--;
        }
        argsList.Where(x => x.turnsDura == 0).ToList().ForEach(RemoveEffect);
    }
}
