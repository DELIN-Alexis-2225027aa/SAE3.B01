using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// G�re les bruits de pas du personnage en fonction de ses mouvements.
/// </summary>
public class FootstepController : MonoBehaviour
{
    /// <summary>
    /// GameObject repr�sentant l'objet des bruits de pas.
    /// </summary>
    public GameObject footstepObject;

    /// <summary>
    /// Clip audio des bruits de pas.
    /// </summary>
    public AudioClip footstepClip;

    private AudioSource footstepAudio;
    private bool isMoving;

    /// <summary>
    /// Appel� lors de l'initialisation.
    /// </summary>
    void Start()
    {
        // R�cup�re le composant AudioSource de l'objet des bruits de pas.
        footstepAudio = footstepObject.GetComponent<AudioSource>();

        if (footstepAudio == null)
        {
            Debug.LogError("Footstep GameObject ne contient pas de AudioSource !");
        }

        // Initialise la variable de mouvement � false.
        isMoving = false;
    }

    /// <summary>
    /// Appel� lors de la destruction de l'objet.
    /// </summary>
    void OnDestroy()
    {
        // Arr�te la lecture des bruits de pas s'ils sont en cours de lecture lors de la destruction.
        if (footstepAudio.isPlaying)
        {
            footstepAudio.Stop();
        }
    }

    /// <summary>
    /// R�initialise le FootstepController lorsqu'une sc�ne est d�charg�e.
    /// </summary>
    /// <param name="scene">Sc�ne d�charg�e.</param>
    void OnSceneUnloaded(Scene scene)
    {
        // Appelle la m�thode de r�initialisation et d�truit l'objet.
        ResetFootstepController();
        Destroy(gameObject);
    }

    /// <summary>
    /// Appel� � chaque image fixe du moteur de jeu.
    /// G�re la lecture et l'arr�t des bruits de pas en fonction des mouvements du personnage.
    /// </summary>
    void FixedUpdate()
    {
        // V�rifie si le personnage est en mouvement.
        if (Input.GetAxisRaw("Horizontal") != 0f || Input.GetAxisRaw("Vertical") != 0f)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        // G�re la lecture et l'arr�t des bruits de pas en fonction du mouvement.
        if (isMoving)
        {
            if (!footstepAudio.isPlaying)
            {
                footstepAudio.Play();
            }
        }
        else
        {
            if (footstepAudio.isPlaying)
            {
                footstepAudio.Stop();
            }
        }
    }

    /// <summary>
    /// R�initialise le FootstepController en arr�tant la lecture des bruits de pas.
    /// </summary>
    public void ResetFootstepController()
    {
        // Arr�te la lecture des bruits de pas lors de la r�initialisation.
        if (footstepAudio.isPlaying)
        {
            footstepAudio.Stop();
        }
    }
}
