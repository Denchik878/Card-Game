using System;
using UnityEngine;

public abstract class CardEffect : MonoBehaviour
{
    public int turns;
    protected Card baseCard;
    public GameObject effectIconPrefab;
    private GameObject currentEffectIcon;

    private void OnEnable()
    {
        if (effectIconPrefab != null)
        {
            currentEffectIcon = Instantiate(effectIconPrefab);
        }
    }

    private void OnDisable()
    {
        if (currentEffectIcon != null)
        {
            Destroy(currentEffectIcon);
        }
    }

    private void Awake()
    {
        baseCard = GetComponent<Card>();
    }

    public abstract void TakeEffect();
}
