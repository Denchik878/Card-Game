using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AxeCard : Card
{
    private int attacksLeft;
    public GameObject[] elemets;
    public GameObject dragPrefab;
    private GameObject dragObject;
    private void Start()
    {
        base.Start();
        attacksLeft = elemets.Length;
    }
    public override async Awaitable Turn()
    {
        attacksLeft--;
        if (attacksLeft == 0)
        {
            Destroy(gameObject);
        }
        StartCoroutine(FadeAndDestroy(elemets[attacksLeft]));
        GameManager.Instance.state = GameState.EnemyTurn;
    }    
    private void OnMouseDown()
    {
        if(GameManager.Instance.state == GameState.PlayerTurn)
        {
            dragObject = Instantiate(dragPrefab);
        }
    }
    private void OnMouseUp()
    {
        DetectCard();
        Destroy(dragObject);
    }
    private void Update()
    {
        if (dragObject == null)
            return;
        dragObject.transform.position = GetMouseWorldPosition();
    }
    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        mouseWorldPosition.z = 0;
        return mouseWorldPosition;
    }
    private void DetectCard()
    {
        Collider2D collider = Physics2D.OverlapCircle(dragObject.transform.position, 0.1f);
        if (!collider)
            return;
        Card buffer = collider.GetComponent<Card>();
        if(buffer == null)
        {
            return;
        }
        if (buffer.isEnemy)
        {
            buffer.ChangeHealth(-damage);
            Turn();
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
