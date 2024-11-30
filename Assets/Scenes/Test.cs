using UnityEngine;

public class Test : MonoBehaviour
{
    public int a = 1;
    public int b = 3;
    public int c = 8;
    public GameObject[] squares;
    async void Start()
    {
        while (a+b != c)
        {
            await Awaitable.NextFrameAsync();
        }
        print("Всё, цикл закончился.");
    }
    private async Awaitable FadeAndDestroy(GameObject element)
    {
        SpriteRenderer renderer = element.GetComponent<SpriteRenderer>();
        Color color = renderer.color;
        float alpha = color.a;
        while (alpha > 0)
        {
            await Awaitable.NextFrameAsync();
            alpha -= Time.deltaTime;
            color.a = alpha;
            renderer.color = color;
        }
        Destroy(element);
    }
}
