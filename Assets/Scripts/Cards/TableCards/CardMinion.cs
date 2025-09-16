using UnityEngine;

public class CardMinion : Card
{
    private bool isKilled = true;
    protected override async Awaitable Turn()
    {
        if(crystalAmount <= 1)
        {
            FindAnyObjectByType<HerzCard>().ChangeHealth(-damage);
            isKilled = false;
            await DestroySelf();
        }
        else
        {
            ChangeCrystalAmount(-1);
        }
    }

    protected async override Awaitable DestroySelf()
    {
        if (isKilled)
        {
            GameManager.Instance.valuta += valutaErned;
        }
        base.DestroySelf();
    }
}
