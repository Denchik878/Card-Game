using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableCard : Card
{
    public GameObject[] elements;
    public int turnsToAttack;
    public Card weaponPrefab;
    public override async Awaitable Turn()
    {
        if (turnsToAttack <= 1)
        {
            await DestroySelf();
        }
        else
        {
            turnsToAttack--;
            await FadeAndDestroy(elements[turnsToAttack]);
        }
    }
    private async void OnMouseDown()
    {
        if (GameManager.Instance.State == GameState.PlayerTurn)
        {
            FindAnyObjectByType<Player>().CreateWeapon(weaponPrefab);
            GameManager.Instance.ChangeState(GameState.EnemyTurn);
            await DestroySelf();
        }
    }
}
