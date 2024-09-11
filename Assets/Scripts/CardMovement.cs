using UnityEngine;
using UnityEngineInternal;

public class CardMovement : MonoBehaviour
{
    public GameObject dragPrefab;
    private GameObject dragObject;
    private void OnMouseDown()
    {
        dragObject = Instantiate(dragPrefab);
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
        if (collider)
        {
            collider.gameObject.GetComponent<Card>().ChangeHealth(-1);
        }
    }
}
