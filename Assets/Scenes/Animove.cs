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
        if (progress > 0)
            return;
        Vector3 current = Vector3.zero;
        Vector3 start = transform.position;
        while (progress < 1)
        {
            progress += Time.deltaTime / duration;
            progress = Mathf.Clamp01(progress);
            current = Vector3.LerpUnclamped(start, target.position, curve.Evaluate(progress));
            transform.position = current;
            await Awaitable.NextFrameAsync();
        }
        progress = 0;
    }

    void Start()
    {
        distance = Vector3.Distance(target.position, transform.position);
    }

    void Update()
    {
        
    }
}
