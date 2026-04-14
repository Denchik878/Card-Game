using UnityEngine;

public class ArrayTest : MonoBehaviour
{
    private int song1 = 5;
    private int song2;
    private int song3;
    void Start()
    {
        song2 = song1;
        song3 = song2;
        song3 = 10;
        print(song1);

        
    }
    
    void Update()
    {
        
    }
}
