using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerzCard : Card
{
    public GameObject dragPrefab;
    private GameObject dragObject;
    public override async Awaitable Turn()
    {
        GameManager.Instance.ChangeState(GameState.EnemyTurn);
    }
    private void OnMouseDown()
    {
        if (GameManager.Instance.State == GameState.PlayerTurn)
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
        if (buffer == null)
        {
            return;
        }
        if (buffer.isEnemy)
        {

            buffer.ChangeHealth(-buffer.maxHealth);
            ChangeHealth(-buffer.damage);
            Turn();
        }
    }
}
