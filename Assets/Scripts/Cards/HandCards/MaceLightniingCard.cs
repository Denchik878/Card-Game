using UnityEngine;

public class MaceLightniingCard : Weapon
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
        DamageNeighbourCards(enemyCard);
        player.FinishTurn();
        attacksLeft--;
        if (attacksLeft == 0)
        {
            DestroySelf();
            return;
        }
        await FadeAndDestroy(elemets[attacksLeft]);
    }

    private void DamageNeighbourCards(Card enemyCard)
    {
        Point point = enemyCard.currentPoint;
        if(point == null)
            return;
        if (point.index < point.length)
        {
            Card neighbour = point.GetPointByIndex(point.index + 1).currentCard;
            if (neighbour.isEnemy)
            {
                neighbour.ChangeHealth(-damage/2);
            }
        }
        if (point.index > 1)
        {
            Card neighbour = point.GetPointByIndex(point.index - 1).currentCard;
            if (neighbour.isEnemy)
            {
                neighbour.ChangeHealth(-damage/2);
            }
        }
    }
}
