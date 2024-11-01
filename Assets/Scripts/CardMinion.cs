using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardMinion : Card
{
    public GameObject[] elements;
    public int turnsToAttack;
    public override void Turn() 
    {
        if(turnsToAttack <= 1)
        {
            FindObjectOfType<HerzCard>().ChangeHealth(-damage);
            Destroy(gameObject);//Будет баг
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
        while(alpha > 0)
        {
            yield return null;
            alpha -= Time.deltaTime;
            color.a = alpha;
            renderer.color = color;
        }
        Destroy(element);
    } 
    
}
