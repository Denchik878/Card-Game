using UnityEngine;

public class SpellCard : Weapon
{
    protected override async void Damage(Card enemyCard)
    {
        if (enemyCard.GetComponent<Card>().HPText == null)
        {
            CancelAttack();
            return;
        }
        enemyCard.GetComponent<PoisonEffect>().enabled = true;
        enemyCard.GetComponent<PoisonEffect>().turns = 3;
        ChangeCrystalAmount(-1);
        player.FinishTurn();
        if (crystalAmount == 0)
        {
            await DestroySelf();
        }
    }
}
