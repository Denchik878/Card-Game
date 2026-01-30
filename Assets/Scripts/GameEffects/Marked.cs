using System.Linq;
using UnityEngine;

public class Marked : CardEffect
{
    public override void TakeEffect()
    {
        foreach (var effect in argsList)
        {
            if (effect.turnsDelay == 1)
            {
                baseCard.ChangeHealth(-effect.damage);
            }
            effect.turnsDelay--;
        }
        argsList.Where(x => x.turnsDelay == 0).ToList().ForEach(RemoveEffect);
    }
}
