using System;
using Unity.VisualScripting;
using UnityEngine;

public abstract class  Weapon : Card
{
    public GameObject dragPrefab;
    private GameObject dragObject;
    protected Player player;

    protected void Awake()
    {
        player = FindAnyObjectByType<Player>();
    }
    protected void OnMouseDown()
    {
        if(GameManager.Instance.State == GameState.PlayerTurn)
        {
            GameManager.Instance.ChangeState(GameState.PlayerAnimation);
            dragObject = Instantiate(dragPrefab);
        }
    }
    private void OnMouseUp()
    {
        if (GameManager.Instance.State == GameState.PlayerAnimation)
        {            
            DetectCard();
            Destroy(dragObject);
        }
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
    private void  DetectCard()
    {
        Collider2D collider = Physics2D.OverlapPoint(dragObject.transform.position);
        if (!collider)
        {
            GameManager.Instance.ChangeState(GameState.PlayerTurn);
            return;
        }
        Card buffer = collider.GetComponent<Card>();
        if(buffer == null)
        {
            GameManager.Instance.ChangeState(GameState.PlayerTurn);
            return;
        }
        Damage(buffer);
    }
    protected abstract void Damage(Card enemyCard);
}
