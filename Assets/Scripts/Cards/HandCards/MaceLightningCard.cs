using UnityEngine;

public class MaceLightningCard : Weapon
{
    public int sideDamage;
    protected override async void Damage(Card enemyCard)
    {
        if (enemyCard.GetComponent<Card>().HPText == null)
        {
            CancelAttack();
            return;
        }
        enemyCard.ChangeHealth(-damage);
        DamageNeighbourCards(enemyCard);
        player.FinishTurn();
        ChangeCrystalAmount(-1);
        if (crystalAmount == 0)
        {
            DestroySelf();
            return;
        }
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
                neighbour.ChangeHealth(-sideDamage);
            }
        }
        if (point.index > 1)
        {
            Card neighbour = point.GetPointByIndex(point.index - 1).currentCard;
            if (neighbour.isEnemy)
            {
                neighbour.ChangeHealth(-sideDamage);
            }
        }
    }
}
