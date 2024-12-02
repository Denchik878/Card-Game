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
            DestroySelf();
        }
        await FadeAndDestroy(elemets[attacksLeft]);
        GameManager.Instance.ChangeState(GameState.EnemyTurn);
    }    
    private void OnMouseDown()
    {
        if(GameManager.Instance.State == GameState.PlayerTurn)
        {
            GameManager.Instance.ChangeState(GameState.PlayerAnimation);
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
        if (buffer.isEnemy)
        {
            buffer.ChangeHealth(-damage);
            Turn();
        }
        else
        {
            GameManager.Instance.ChangeState(GameState.PlayerTurn);
        }
    }
}
