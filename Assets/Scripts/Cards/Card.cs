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
    private List<GameObject> iconEffects = new();
    public event Action<Card> OnDeath;
    public float animationSpeed = 1;
    public float crystalAnimDura = 0.25f;
    public GameObject crystal;
    public int crystalAmount;
    private List<GameObject> crystals = new();
    public AudioClip takeDamageSound;
    public AudioClip healingSound;
    public int valutaErned;
    public int crystalsAddedByAbility;
    private Collider2D collider;
    private Vector3[] iconPositions;

    protected void Start()
    {
        iconPositions = IconPositions();
        ChangeCrystalAmount(0);
        if (damageText != null)
        {
            damageText.text = damage.ToString();
        }

        if (HPText != null)
        {
            ChangeHealth(maxHealth);
        }
        collider = GetComponent<Collider2D>();
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

    public async Awaitable ChangeCrystalAmount(int amount)
    {
        crystalAmount += amount;
        Vector3[] positions = CrystalsPositions(crystalAmount);
        float timer = 0;
        while (timer < crystalAnimDura)
        {
            await Awaitable.NextFrameAsync();
            timer += Time.deltaTime;
            foreach (GameObject crystal in crystals)
            {
                var render = crystal.GetComponent<SpriteRenderer>();
                render.material.SetFloat("_Dissolved", 1 - timer / crystalAnimDura);
            }
        }

        foreach (var crystal in crystals)
        {
            Destroy(crystal);
        }

        crystals.Clear();
        foreach (Vector3 position in positions)
        {
            GameObject newCrystal = Instantiate(crystal, transform.TransformPoint(position),
                Quaternion.identity, transform);
            crystals.Add(newCrystal);
            var render = newCrystal.GetComponent<SpriteRenderer>();
            render.material.SetFloat("_Dissolved", 0);
        }

        timer = 0;
        while (timer < crystalAnimDura)
        {
            await Awaitable.NextFrameAsync();
            timer += Time.deltaTime;
            foreach (GameObject crystal in crystals)
            {
                var render = crystal.GetComponent<SpriteRenderer>();
                render.material.SetFloat("_Dissolved", timer / crystalAnimDura);
            }
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
        while (alpha > 0)
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
        for (int i = 0; i < count; i++)
        {
            positions[i] = new Vector3(-0.5f + i * step, 0.78f, 0);
        }

        return positions;
    }

    public void AddEffectIcon(GameObject effect)
    {
        var go = Instantiate(effect);
        iconEffects.Add(go);
        for (int i = 0; i < iconEffects.Count; i++)
        {
            iconEffects[i].transform.position = iconPositions[i];
        }
    }

    private Vector3[] IconPositions()
    {
        Vector3[] positions = new Vector3[20];
        float width = collider.bounds.size.x;
        float height = collider.bounds.size.y;
        float gap = width / 5;
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                positions[j + i*4] = new Vector3(width/2 - (j + 1)*gap, height/2 - gap, 0);
            }
        }
        return null;
    }
}