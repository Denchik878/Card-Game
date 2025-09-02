using System;
using Unity.VisualScripting;
using UnityEngine;
using Cache = Unity.VisualScripting.Cache;

public abstract class  Weapon : Card
{
    public GameObject dragPrefab;
    private GameObject dragObject;
    protected Player player;
    public AudioClip grabClip;
    public AudioClip strikeClip;
    private Camera mainCamera;

    protected void Awake()
    {
        mainCamera = Camera.main;
        player = FindAnyObjectByType<Player>();
    }
    protected void OnMouseDown()
    {
        if(GameManager.Instance.State == GameState.PlayerTurn)
        {
            GameManager.Instance.ChangeState(GameState.PlayerAnimation);
            AudioManager.Instance.PlayClip(grabClip);
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
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(mouseScreenPosition);
        mouseWorldPosition.z = 0;
        return mouseWorldPosition;
    }
    private void  DetectCard()
    {
        Collider2D collider = Physics2D.OverlapPoint(dragObject.transform.position);
        if (!collider)
        {
            CancelAttack();
            return;
        }
        Card buffer = collider.GetComponent<Card>();
        if(buffer == null)
        {
            GameManager.Instance.ChangeState(GameState.PlayerTurn);
            return;
        }
        AudioManager.Instance.PlayClip(strikeClip);
        Damage(buffer);
    }
    protected void CancelAttack()
    {
        GameManager.Instance.ChangeState(GameState.PlayerTurn);
    }
    protected abstract void Damage(Card enemyCard);
}
