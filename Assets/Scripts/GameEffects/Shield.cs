using System;
using UnityEngine;

public class Shield : CardEffect
{
    public override void TakeEffect()
    {
        
    }

    private void OnEnable()
    {
        baseCard.OnDamageTaken += DamageTaken;
    }

    private void OnDisable()
    {
        baseCard.OnDamageTaken -= DamageTaken;
    }

    private void DamageTaken(int a)
    {
        if (a >= 0) return;
        var healAmount = 0;
        foreach (var args in argsList)
        {
            args.turnsDura--;
            a -= args.damage;
            if (a >= 0)
            {
                healAmount += args.damage;
            }
            else
            {
                healAmount = args.damage - a;
            }
        }
    }
}
