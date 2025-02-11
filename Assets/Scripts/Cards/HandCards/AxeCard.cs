using UnityEngine;

public class AxeCard : Weapon
{
    protected override async void Damage(Card enemyCard)
    {
        enemyCard.ChangeHealth(-damage);
        player.FinishTurn();
        ChangeCrystalAmount(-1);
        if (crystalAmount == 0)
        {
            DestroySelf();
            return;
        }
    }
}
