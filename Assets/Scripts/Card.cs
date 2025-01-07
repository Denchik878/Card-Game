using System;
using TMPro;
using UnityEngine;

public abstract class Card : MonoBehaviour
{
    public int damage;
    public int maxHealth;
    private int currentHealth;
    public TMP_Text HPText;
    public TMP_Text damageText;
    public bool isEnemy;
    private CardEffect[] effects;
    public event Action<Card> OnDeath;
    public float animationSpeed = 1;
    protected void Start()
    {
        if (damageText != null)
        {
            damageText.text = damage.ToString();
        }
        if (HPText != null)
        {
            ChangeHealth(maxHealth);
        }        
    }    
    public void ChangeHealth(int a)
    {
        currentHealth += a;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            OnDeath?.Invoke(this);
        }
        HPText.text = currentHealth.ToString();
    }

    public async Awaitable BaseTurn()
    {
        effects = GetComponents<CardEffect>();
        foreach (var effect in effects)
        {
            if (effect.enabled)
            {
                effect.TakeEffect();
            }
        }
        await Turn();
    }
    protected virtual async Awaitable Turn()
    {
        
    }

    protected async Awaitable DestroySelf()
    {
        OnDeath?.Invoke(this);
    }

    protected async Awaitable FadeAndDestroy(GameObject element)
    {
        SpriteRenderer renderer = element.GetComponent<SpriteRenderer>();
        Color color = renderer.color;
        float alpha = color.a;
        while(alpha > 0)
        {
            await Awaitable.NextFrameAsync();
            alpha -= Time.deltaTime * animationSpeed;
            color.a = alpha;
            renderer.color = color;
        }
        Destroy(element);
    }
}
