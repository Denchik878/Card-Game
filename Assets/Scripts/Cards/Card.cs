using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Card : MonoBehaviour
{
    public Point currentPoint;
    public int damage;
    public int maxHealth;
    private int currentHealth;
    public TMP_Text HPText;
    public TMP_Text damageText;
    public bool isEnemy;
    private CardEffect[] effects;
    public event Action<Card> OnDeath;
    public float animationSpeed = 1;
    public GameObject crystal;
    public int crystalAmount;
    private List<GameObject> crystals = new();
    public AudioClip takeDamageSound;
    public AudioClip healingSound;
    public int valutaErned;
    public int crystalsAddedByAbility;
    protected void Start()
    {
        ChangeCrystalAmount(0);
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
            DestroySelf();
        }

        if (a < 0)
        {
            AudioManager.Instance.PlayClip(takeDamageSound);
        }
        else
        {
            AudioManager.Instance.PlayClip(healingSound);
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
    public void ChangeCrystalAmount(int amount)
    {
        crystalAmount += amount;
        Vector3[] positions = CrystalsPositions(crystalAmount);
        foreach(GameObject crystal in crystals)
        {
            Destroy(crystal);
        }
        crystals.Clear();
        foreach(Vector3 position in positions)
        {
            GameObject newCrystal = Instantiate(crystal, transform.TransformPoint(position),
                Quaternion.identity, transform);
            crystals.Add(newCrystal);
        }
    }
    protected virtual async Awaitable DestroySelf()
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
    private Vector3[] CrystalsPositions(int count)
    {
        Vector3[] positions = new Vector3[count];
        if (count == 1)
        {
            positions[0] = new Vector3(0, 0.78f, 0);
            return positions;
        }
        if (count == 2)
        {
            positions[0] = new Vector3(-0.25f, 0.78f, 0);
            positions[1] = new Vector3(0.25f, 0.78f, 0);
            return positions;
        }
        float step = 1 / ((float)count - 1);
        for(int i = 0; i < count; i++)
        {
            positions[i] = new Vector3(-0.5f + i * step, 0.78f, 0);
        }
        return positions;
    }
}
