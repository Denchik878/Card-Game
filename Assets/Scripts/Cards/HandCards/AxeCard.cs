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
    protected override async void Damage(Card enemyCard)
    {
        enemyCard.ChangeHealth(-damage);
        player.FinishTurn();
        attacksLeft--;
        if (attacksLeft == 0)
        {
            DestroySelf();
            return;
        }
        await FadeAndDestroy(elemets[attacksLeft]);
    }
}
