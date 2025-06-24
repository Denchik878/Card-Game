using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnDeath : MonoBehaviour
{
    private Card card;
    public UnityEvent[] onDeathEffects;
    public int onDeathDamage;
    public int onDeathValute;
    private void Awake()
    {
        card = GetComponent<Card>();
    }

    private void OnEnable()
    {
        card.OnDeath += OnDeathEffect;
    }

    private void OnDisable()
    {
        card.OnDeath -= OnDeathEffect;
    }

    private void OnDeathEffect(Card card)
    {
        foreach (var onDeathEffect in onDeathEffects)
        {
            onDeathEffect?.Invoke();
        }
    }

    public void DamagePlayer()
    {
        FindAnyObjectByType<HerzCard>().ChangeHealth(-onDeathDamage);
    }
    public void AddValuta()
    {
        GameManager.Instance.valuta += onDeathValute;
    }
}
