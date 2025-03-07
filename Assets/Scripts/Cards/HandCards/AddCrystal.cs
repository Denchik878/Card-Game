using UnityEngine;

public class AddCrystal : Weapon
{
    public int addCrystalAmount;
    protected override void Damage(Card enemyCard)
    {
        enemyCard.ChangeCrystalAmount(addCrystalAmount);
        player.FinishTurn();
    }
}
