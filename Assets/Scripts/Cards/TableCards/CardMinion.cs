using UnityEngine;

public class CardMinion : Card
{
    protected override async Awaitable Turn()
    {
        if(crystalAmount <= 1)
        {
            FindAnyObjectByType<HerzCard>().ChangeHealth(-damage);
            await DestroySelf();
        }
        else
        {
            ChangeCrystalAmount(-1);
        }
    }
}
