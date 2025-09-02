using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TableCard : Card
{
    public Card weaponPrefab;
    public AudioClip clip;

    protected override async Awaitable Turn()
    {
        if (crystalAmount <= 1)
        {
            DestroySelf();
        }
        else
        {
            ChangeCrystalAmount(-1);
        }
    }
    private async void OnMouseDown()
    {
        if (GameManager.Instance.State == GameState.PlayerTurn)
        {
            var player = FindAnyObjectByType<Player>();
            player.CreateWeapon(weaponPrefab);
            player.FinishTurn();
            AudioManager.Instance.PlayClip(clip);
            await DestroySelf();
        }
    }
}
