using UnityEngine;

public class EarnValuta : MonoBehaviour
{
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 1)
        {
            timer = 0;
            GameManager.Instance.valuta++;
        }
    }
}
