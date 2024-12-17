using System;
using UnityEngine;

public abstract class CardEffect : MonoBehaviour
{
    protected Card baseCard;
    private void Start()
    {
        baseCard = GetComponent<Card>();
    }

    public abstract void TakeEffect();
}
