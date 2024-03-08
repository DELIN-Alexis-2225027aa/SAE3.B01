using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// G�re la musique du menu principal en fonction des sc�nes charg�es.
/// </summary>
public class MainMenuMusicManager : MonoBehaviour
{
    /// <summary>
    /// Clip audio pour la musique du menu principal.
    /// </summary>
    public AudioClip musicClip;

    private AudioSource musicSource;

    /// <summary>
    /// Tableau des noms de sc�nes pour lesquelles la musique sera jou�e.
    /// </summary>
    public string[] playMusicScenes = new string[]
    {
        "Map",
        "Proof",
        "MovingPhase"
    };

    private static MainMenuMusicManager instance;

    /// <summary>
    /// Appel� lors de la cr�ation de l'objet.
    /// </summary>
    void Awake()
    {
        // V�rifie s'il existe d�j� une instance et d�truit celle-ci si c'est le cas.
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // D�finit cette instance comme l'instance actuelle et la rend persistante entre les sc�nes.
        instance = this;
        DontDestroyOnLoad(gameObject);

        // Initialise le composant AudioSource pour la musique.
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.clip = musicClip;
        musicSource.loop = true;

        // S'abonne � l'�v�nement de chargement de sc�ne.
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    /// <summary>
    /// Appel� lorsque la sc�ne est charg�e.
    /// </summary>
    /// <param name="scene">Sc�ne charg�e.</param>
    /// <param name="mode">Mode de chargement de sc�ne.</param>
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // V�rifie si la musique doit �tre jou�e pour la sc�ne actuelle.
        foreach (string playScene in playMusicScenes)
        {
            if (scene.name == playScene)
            {
                PlayMusic();
                return;
            }
        }

        // Arr�te la musique si la sc�ne actuelle n'est pas dans la liste des sc�nes de lecture de musique.
        StopMusic();
    }

    /// <summary>
    /// Joue la musique si elle n'est pas d�j� en cours de lecture.
    /// </summary>
    void PlayMusic()
    {
        if (!musicSource.isPlaying)
        {
            musicSource.Play();
        }
    }

    /// <summary>
    /// Arr�te la musique si elle est en cours de lecture.
    /// </summary>
    void StopMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }

    /// <summary>
    /// D�truit l'objet pour arr�ter la musique du menu principal.
    /// </summary>
    public void DestroyMusicManager()
    {
        Destroy(gameObject);
    }
}
