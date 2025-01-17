using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TableCard : Card
{
    public GameObject[] elements;
    public int turnsToDestroy;
    public Card weaponPrefab;

    private void Awake()
    {
        turnsToDestroy = elements.Length;
    }

    protected override async Awaitable Turn()
    {
        if (turnsToDestroy <= 1)
        {
            await DestroySelf();
        }
        else
        {
            turnsToDestroy--;
            await FadeAndDestroy(elements[turnsToDestroy]);
        }
    }
    private async void OnMouseDown()
    {
        if (GameManager.Instance.State == GameState.PlayerTurn)
        {
            var player = FindAnyObjectByType<Player>();
            player.CreateWeapon(weaponPrefab);
            player.FinishTurn();
            await DestroySelf();
        }
    }
}
