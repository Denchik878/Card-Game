using System;
using UnityEngine;

public abstract class CardEffect : MonoBehaviour
{
    public int turns;
    protected Card baseCard;
    private void Awake()
    {
        baseCard = GetComponent<Card>();
    }

    public abstract void TakeEffect();
}
