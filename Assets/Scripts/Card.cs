using System.Collections;
using System.Collections.Generic;
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
    public abstract void Turn();
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
            Destroy(gameObject);
        }
        HPText.text = currentHealth.ToString();
    }
    void Start()
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

}
