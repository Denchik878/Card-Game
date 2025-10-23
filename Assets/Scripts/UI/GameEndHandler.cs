using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndHandler : MonoBehaviour
{
    
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
    
}
