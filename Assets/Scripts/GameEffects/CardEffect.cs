using System;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public abstract class CardEffect : MonoBehaviour
{
    protected Card baseCard;
    protected List<EffectArgs> argsList = new();
    public GameObject effectIconPrefab;
    private void Awake()
    {
        baseCard = GetComponent<Card>();
    }
    public void AddEffect(EffectArgs args)
    {
        if (effectIconPrefab != null)
        {
            args.icon = Instantiate(effectIconPrefab, baseCard.transform);
            
            baseCard.AddEffectIcon(args.icon);
            argsList.Add(args);
        }
    }

    public void RemoveEffect(EffectArgs args)
    {
        if (args.icon != null)
        {
            baseCard.RemoveEffectIcon(args.icon);
            argsList.Remove(args);
        }
    }

    public abstract void TakeEffect();
}

public class EffectArgs
{
    public int damage;
    public int turnsDura;
    public int turnsDelay;
    public GameObject icon;
}
