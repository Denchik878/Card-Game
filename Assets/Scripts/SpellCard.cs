using UnityEngine;

public class SpellCard : Weapon
{
    protected override async void Damage(Card enemyCard)
    {
        enemyCard.GetComponent<PoisonEffect>().enabled = true;
        enemyCard.GetComponent<PoisonEffect>().turns = 3;
        await DestroySelf();
        player.FinishTurn();
    }
}
