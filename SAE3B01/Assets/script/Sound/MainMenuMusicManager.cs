using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuMusicManager : MonoBehaviour
{
    public AudioClip musicClip;
    private AudioSource musicSource;

    public string[] playMusicScenes = new string[]
{
    "Map",
    "Proof",
    "MovingPhase"
};

    private static MainMenuMusicManager instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.clip = musicClip;
        musicSource.loop = true;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        foreach (string playScene in playMusicScenes)
        {
            if (scene.name == playScene)
            {
                PlayMusic();
                return;
            }
        }

        StopMusic();
    }

    void PlayMusic()
    {
        if (!musicSource.isPlaying)
        {
            musicSource.Play();
        }
    }

    void StopMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }
    public void DestroyMusicManager()
    {
        Destroy(gameObject);
    }

}
