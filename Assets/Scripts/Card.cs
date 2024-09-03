using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int damage;
    public int maxHealth;
    private int currentHealth;
    public TMP_Text HPText;
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
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
