using System;
using TMPro;
using UnityEngine;

public class GameStateUI : MonoBehaviour
{
    public TMP_Text gameStateText;
    public TMP_Text valutaText;

    private void Update()
    {
        valutaText.text = GameManager.Instance.valuta.ToString();
    }

    private async void OnEnable()
    {
        await Awaitable.NextFrameAsync();
        GameManager.Instance.OnStateChanged += ChangeStateText;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnStateChanged -= ChangeStateText;
    }

    private void ChangeStateText()
    {
        switch (GameManager.Instance.State)
        {
            case GameState.EnemyTurn:
                gameStateText.text = "Enemy Turn";
                break;
            case GameState.PlayerTurn:
                gameStateText.text = "Player Turn";
                break;
            case GameState.EnemyAnimaton:
                gameStateText.text = "Enemy Animaton";
                break;
            case GameState.PlayerAnimation:
                gameStateText.text = "Player Animation";
                break;
            default:
                gameStateText.text = "no known state";
                break;
        }
    }
}
