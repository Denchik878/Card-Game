using UnityEngine;

public class AddCrystal : Weapon
{
    public int abilityCost;
    protected override void Damage(Card enemyCard)
    {
        GameManager.Instance.valuta -= abilityCost;
        enemyCard.ChangeCrystalAmount(enemyCard.crystalsAddedByAbility);
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
