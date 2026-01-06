using UnityEngine;

public class Marked : CardEffect
{
    public override void TakeEffect()
    {
        foreach (var effect in argsList)
        {
            if (effect.turnsDura == 0)
            {
                baseCard.ChangeHealth(-effect.damage);
                RemoveEffect(effect);
                continue;
            }
            effect.turnsDura--;
        }
    }
}
