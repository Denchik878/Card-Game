using UnityEngine;

public class AddCrystal : Weapon
{
    public int abilityCost;
    public int addCrystalAmount;
    protected override void Damage(Card enemyCard)
    {
        GameManager.Instance.valuta -= abilityCost;
        enemyCard.ChangeCrystalAmount(addCrystalAmount);
        player.FinishTurn();
    }
    protected void OnMouseDown()
    {
        if(GameManager.Instance.valuta >= abilityCost)
        {
            base.OnMouseDown();
        }
    }
}
