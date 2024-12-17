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

    public virtual async Awaitable Turn()
    {
        effects = GetComponents<CardEffect>();
        foreach (var effect in effects)
        {
            if (effect.enabled)
            {
                effect.TakeEffect();
            }
        }
    }

    protected async Awaitable DestroySelf()
    {
        await Awaitable.NextFrameAsync();
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
            alpha -= Time.deltaTime;
            color.a = alpha;
            renderer.color = color;
        }
        Destroy(element);
    }
}
