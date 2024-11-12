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
    public abstract void Turn();

    protected void DestroySelf()
    {
        OnDeath?.Invoke(this);
    }
}
