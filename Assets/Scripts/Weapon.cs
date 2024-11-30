using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Card
{
    public GameObject[] elements;
    public int turnsToAttack;
    public Card weaponPrefab;
    public override async Awaitable Turn()
    {
        if (turnsToAttack <= 1)
        {
            DestroySelf();
        }
        else
        {
            turnsToAttack--;
            StartCoroutine(FadeAndDestroy(elements[turnsToAttack]));
        }
    }
    private IEnumerator FadeAndDestroy(GameObject element)
    {
        SpriteRenderer renderer = element.GetComponent<SpriteRenderer>();
        Color color = renderer.color;
        float alpha = color.a;
        while (alpha > 0)
        {
            yield return null;
            alpha -= Time.deltaTime;
            color.a = alpha;
            renderer.color = color;
        }
        Destroy(element);
    }
    private void OnMouseDown()
    {
        FindObjectOfType<Player>().CreateWeapon(weaponPrefab);
        GameManager.Instance.state = GameState.EnemyTurn;
        DestroySelf();
    }
}
