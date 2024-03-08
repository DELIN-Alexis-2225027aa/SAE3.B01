using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Gère la musique du jeu.
/// </summary>
public class MusicManager : MonoBehaviour
{
    /// <summary>
    /// Clip audio pour la musique.
    /// </summary>
    public AudioClip musicClip;

    /// <summary>
    /// Composant AudioSource utilisé pour jouer la musique.
    /// </summary>
    private AudioSource musicSource;

    /// <summary>
    /// Scènes où la musique doit être jouée.
    /// </summary>
    public string[] playMusicScenes = new string[]
    {
        "Map",
        "Proof",
        "MovingPhase"
    };

    /// <summary>
    /// Instance statique du gestionnaire de musique.
    /// </summary>
    private static MusicManager instance;

    /// <summary>
    /// Méthode appelée au réveil de l'objet.
    /// </summary>
    void Awake()
    {
        // Vérification si une instance existe déjà
        if (instance != null && instance != this)
        {
            // Détruit l'objet s'il y a déjà une instance
            Destroy(gameObject);
            return;
        }

        // Définit l'instance comme l'instance actuelle
        instance = this;

        // Ne détruit pas l'objet lors du chargement d'une nouvelle scène
        DontDestroyOnLoad(gameObject);

        // Ajoute un composant AudioSource à l'objet et configure la musique
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.clip = musicClip;
        musicSource.loop = true;

        // S'abonne à l'événement de chargement de scène
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    /// <summary>
    /// Méthode appelée lorsqu'une scène est chargée.
    /// </summary>
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Vérifie si la musique doit être jouée pour la scène actuelle
        foreach (string playScene in playMusicScenes)
        {
            if (scene.name == playScene)
            {
                PlayMusic();
                return;
            }
        }

        // Arrête la musique si la scène actuelle ne correspond à aucune scène de lecture musicale
        StopMusic();
    }

    /// <summary>
    /// Méthode pour démarrer la lecture de la musique.
    /// </summary>
    void PlayMusic()
    {
        // Démarre la musique si elle n'est pas déjà en cours de lecture
        if (!musicSource.isPlaying)
        {
            musicSource.Play();
        }
    }

    /// <summary>
    /// Méthode pour arrêter la musique.
    /// </summary>
    void StopMusic()
    {
        // Arrête la musique si elle est en cours de lecture
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }

    /// <summary>
    /// Méthode pour détruire le gestionnaire de musique.
    /// </summary>
    public void DestroyMusicManager()
    {
        // Détruit l'objet du gestionnaire de musique
        Destroy(gameObject);
    }
}
