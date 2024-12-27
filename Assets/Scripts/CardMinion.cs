using UnityEngine;

public class CardMinion : Card
{
    public GameObject[] elements;
    private int turnsToAttack;

    private void Awake()
    {
        turnsToAttack = elements.Length;
    }
    public override async Awaitable Turn()
    {
        base.Turn();
        if(turnsToAttack <= 1)
        {
            FindAnyObjectByType<HerzCard>().ChangeHealth(-damage);
            await DestroySelf();
        }
        else
        {
            turnsToAttack--;
            await FadeAndDestroy(elements[turnsToAttack]);
        }
    }
}
