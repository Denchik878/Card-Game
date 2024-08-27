using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public GameObject dragPrefab;
    private GameObject dragObject;
    private void OnMouseDown()
    {
        dragObject = Instantiate(dragPrefab);
    }
    private void OnMouseUp()
    {
        Destroy(dragObject);
    }
}
