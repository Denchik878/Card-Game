using UnityEngine;

public class Heal : CardEffect
{
    public int healingPerTurn;
    public BossCard boss;

    public override void TakeEffect()
    {
        boss = FindAnyObjectByType<BossCard>();
        if (boss != null)
        {
            boss.ChangeHealth(healingPerTurn);
        }
    }
}
