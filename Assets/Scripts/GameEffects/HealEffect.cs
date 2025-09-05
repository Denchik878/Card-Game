using UnityEngine;

public class HealEffect : CardEffect
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
