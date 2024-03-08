using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Gère la musique du menu principal en fonction des scènes chargées.
/// </summary>
public class MainMenuMusicManager : MonoBehaviour
{
    /// <summary>
    /// Clip audio pour la musique du menu principal.
    /// </summary>
    public AudioClip musicClip;

    private AudioSource musicSource;

    /// <summary>
    /// Tableau des noms de scènes pour lesquelles la musique sera jouée.
    /// </summary>
    public string[] playMusicScenes = new string[]
    {
        "Map",
        "Proof",
        "MovingPhase"
    };

    private static MainMenuMusicManager instance;

    /// <summary>
    /// Appelé lors de la création de l'objet.
    /// </summary>
    void Awake()
    {
        // Vérifie s'il existe déjà une instance et détruit celle-ci si c'est le cas.
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Définit cette instance comme l'instance actuelle et la rend persistante entre les scènes.
        instance = this;
        DontDestroyOnLoad(gameObject);

        // Initialise le composant AudioSource pour la musique.
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.clip = musicClip;
        musicSource.loop = true;

        // S'abonne à l'événement de chargement de scène.
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    /// <summary>
    /// Appelé lorsque la scène est chargée.
    /// </summary>
    /// <param name="scene">Scène chargée.</param>
    /// <param name="mode">Mode de chargement de scène.</param>
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Vérifie si la musique doit être jouée pour la scène actuelle.
        foreach (string playScene in playMusicScenes)
        {
            if (scene.name == playScene)
            {
                PlayMusic();
                return;
            }
        }

        // Arrête la musique si la scène actuelle n'est pas dans la liste des scènes de lecture de musique.
        StopMusic();
    }

    /// <summary>
    /// Joue la musique si elle n'est pas déjà en cours de lecture.
    /// </summary>
    void PlayMusic()
    {
        if (!musicSource.isPlaying)
        {
            musicSource.Play();
        }
    }

    /// <summary>
    /// Arrête la musique si elle est en cours de lecture.
    /// </summary>
    void StopMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }

    /// <summary>
    /// Détruit l'objet pour arrêter la musique du menu principal.
    /// </summary>
    public void DestroyMusicManager()
    {
        Destroy(gameObject);
    }
}
