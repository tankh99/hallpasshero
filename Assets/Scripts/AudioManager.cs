using UnityEngine;

public class AudioManager: MonoBehaviour 
{
    public AudioSource musicSource;
    public AudioClip backgroundMusic;
    private static AudioManager instance;

    void Awake() 
    {
        Debug.Log("AudioManager Awake called");
        DontDestroyOnLoad(gameObject);
        
        // Singleton pattern to ensure only one AudioManager exists
        if (instance == null)
        {
            Debug.Log("Setting up new AudioManager instance");
            instance = this;

            // Initialize audio if not already playing
            if (musicSource != null && backgroundMusic != null)
            {
                musicSource.clip = backgroundMusic;
                musicSource.loop = true;
                musicSource.Play();
                Debug.Log("Started playing music");
            } else 
            {
                Debug.LogWarning("Missing musicSource or backgroundMusic reference!");
            }
        } else {
            Debug.Log("Destroying duplicate AudioManager");
            Destroy(gameObject);
        }
    }
}