using System;
using UnityEngine;

public class Animove : MonoBehaviour
{
    public float progress;
    public AnimationCurve curve;
    public float duration;
    public Transform target;
    private float distance;

    private async void OnMouseDown()
    {
        
    }

    void Start()
    {
        distance = Vector3.Distance(target.position, transform.position);
    }

    void Update()
    {
        
    }
}
