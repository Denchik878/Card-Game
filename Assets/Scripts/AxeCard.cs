using UnityEngine;

public class AxeCard : Weapon
{
    private int attacksLeft;
    public GameObject[] elemets;
    private void Start()
    {
        base.Start();
        attacksLeft = elemets.Length;
    }
    public override async Awaitable Turn()
    {
        base.Turn();
        attacksLeft--;
        if (attacksLeft == 0)
        {
            DestroySelf();
        }
        await FadeAndDestroy(elemets[attacksLeft]);
    }

    protected override void Damage(Card enemyCard)
    {
        enemyCard.ChangeHealth(-damage);
    }
}
