using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource audioSource;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    public void PlayClip(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
