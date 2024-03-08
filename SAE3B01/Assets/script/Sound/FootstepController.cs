using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Gère les bruits de pas du personnage en fonction de ses mouvements.
/// </summary>
public class FootstepController : MonoBehaviour
{
    /// <summary>
    /// GameObject représentant l'objet des bruits de pas.
    /// </summary>
    public GameObject footstepObject;

    /// <summary>
    /// Clip audio des bruits de pas.
    /// </summary>
    public AudioClip footstepClip;

    private AudioSource footstepAudio;
    private bool isMoving;

    /// <summary>
    /// Appelé lors de l'initialisation.
    /// </summary>
    void Start()
    {
        // Récupère le composant AudioSource de l'objet des bruits de pas.
        footstepAudio = footstepObject.GetComponent<AudioSource>();

        if (footstepAudio == null)
        {
            Debug.LogError("Footstep GameObject ne contient pas de AudioSource !");
        }

        // Initialise la variable de mouvement à false.
        isMoving = false;
    }

    /// <summary>
    /// Appelé lors de la destruction de l'objet.
    /// </summary>
    void OnDestroy()
    {
        // Arrête la lecture des bruits de pas s'ils sont en cours de lecture lors de la destruction.
        if (footstepAudio.isPlaying)
        {
            footstepAudio.Stop();
        }
    }

    /// <summary>
    /// Réinitialise le FootstepController lorsqu'une scène est déchargée.
    /// </summary>
    /// <param name="scene">Scène déchargée.</param>
    void OnSceneUnloaded(Scene scene)
    {
        // Appelle la méthode de réinitialisation et détruit l'objet.
        ResetFootstepController();
        Destroy(gameObject);
    }

    /// <summary>
    /// Appelé à chaque image fixe du moteur de jeu.
    /// Gère la lecture et l'arrêt des bruits de pas en fonction des mouvements du personnage.
    /// </summary>
    void FixedUpdate()
    {
        // Vérifie si le personnage est en mouvement.
        if (Input.GetAxisRaw("Horizontal") != 0f || Input.GetAxisRaw("Vertical") != 0f)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        // Gère la lecture et l'arrêt des bruits de pas en fonction du mouvement.
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
    /// Réinitialise le FootstepController en arrêtant la lecture des bruits de pas.
    /// </summary>
    public void ResetFootstepController()
    {
        // Arrête la lecture des bruits de pas lors de la réinitialisation.
        if (footstepAudio.isPlaying)
        {
            footstepAudio.Stop();
        }
    }
}
