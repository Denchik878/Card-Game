using UnityEngine;

public class AxeCard : Weapon
{
    private int attacksLeft;
    public GameObject[] elemets;
    private void Start()
    {
        base.Start();
        attacksLeft = elemets.Length;
    }
    public override async Awaitable Turn()
    {
        attacksLeft--;
        if (attacksLeft == 0)
        {
            DestroySelf();
        }
        await FadeAndDestroy(elemets[attacksLeft]);
        GameManager.Instance.ChangeState(GameState.EnemyTurn);
    }    
    
}
