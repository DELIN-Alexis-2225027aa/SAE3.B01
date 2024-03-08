using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// G�re la musique du jeu.
/// </summary>
public class MusicManager : MonoBehaviour
{
    /// <summary>
    /// Clip audio pour la musique.
    /// </summary>
    public AudioClip musicClip;

    /// <summary>
    /// Composant AudioSource utilis� pour jouer la musique.
    /// </summary>
    private AudioSource musicSource;

    /// <summary>
    /// Sc�nes o� la musique doit �tre jou�e.
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
    /// M�thode appel�e au r�veil de l'objet.
    /// </summary>
    void Awake()
    {
        // V�rification si une instance existe d�j�
        if (instance != null && instance != this)
        {
            // D�truit l'objet s'il y a d�j� une instance
            Destroy(gameObject);
            return;
        }

        // D�finit l'instance comme l'instance actuelle
        instance = this;

        // Ne d�truit pas l'objet lors du chargement d'une nouvelle sc�ne
        DontDestroyOnLoad(gameObject);

        // Ajoute un composant AudioSource � l'objet et configure la musique
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.clip = musicClip;
        musicSource.loop = true;

        // S'abonne � l'�v�nement de chargement de sc�ne
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    /// <summary>
    /// M�thode appel�e lorsqu'une sc�ne est charg�e.
    /// </summary>
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // V�rifie si la musique doit �tre jou�e pour la sc�ne actuelle
        foreach (string playScene in playMusicScenes)
        {
            if (scene.name == playScene)
            {
                PlayMusic();
                return;
            }
        }

        // Arr�te la musique si la sc�ne actuelle ne correspond � aucune sc�ne de lecture musicale
        StopMusic();
    }

    /// <summary>
    /// M�thode pour d�marrer la lecture de la musique.
    /// </summary>
    void PlayMusic()
    {
        // D�marre la musique si elle n'est pas d�j� en cours de lecture
        if (!musicSource.isPlaying)
        {
            musicSource.Play();
        }
    }

    /// <summary>
    /// M�thode pour arr�ter la musique.
    /// </summary>
    void StopMusic()
    {
        // Arr�te la musique si elle est en cours de lecture
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }

    /// <summary>
    /// M�thode pour d�truire le gestionnaire de musique.
    /// </summary>
    public void DestroyMusicManager()
    {
        // D�truit l'objet du gestionnaire de musique
        Destroy(gameObject);
    }
}
