using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Contrôleur gérant les empreintes de pas du joueur.
/// </summary>
public class FootstepController : MonoBehaviour
{
    /// <summary>
    /// Objet utilisé pour afficher les empreintes de pas.
    /// </summary>
    public GameObject footstepObject;

    /// <summary>
    /// Clip audio pour les empreintes de pas.
    /// </summary>
    public AudioClip footstepClip;

    /// <summary>
    /// Composant AudioSource utilisé pour jouer les empreintes de pas.
    /// </summary>
    private AudioSource footstepAudio;

    /// <summary>
    /// Indique si le personnage est en mouvement.
    /// </summary>
    private bool isMoving;

    /// <summary>
    /// Méthode appelée au démarrage.
    /// </summary>
    void Start()
    {
        // Récupération du composant AudioSource de l'objet des empreintes de pas
        footstepAudio = footstepObject.GetComponent<AudioSource>();

        // Vérification si l'objet contient un composant AudioSource
        if (footstepAudio == null)
        {
            Debug.LogError("Footstep GameObject doesn't contain an AudioSource!");
        }

        // Initialisation de la variable isMoving à false
        isMoving = false;
    }

    /// <summary>
    /// Méthode appelée à la destruction de l'objet.
    /// </summary>
    void OnDestroy()
    {
        // Arrêt de la lecture audio si elle est en cours
        if (footstepAudio.isPlaying)
        {
            footstepAudio.Stop();
        }
    }

    /// <summary>
    /// Méthode appelée lorsque la scène est déchargée.
    /// </summary>
    void OnSceneUnloaded(Scene scene)
    {
        // Réinitialisation du FootstepController et destruction de l'objet
        ResetFootstepController();
        Destroy(gameObject);
    }

    /// <summary>
    /// Méthode appelée à chaque frame fixe.
    /// </summary>
    void FixedUpdate()
    {
        // Vérification si le personnage est en mouvement
        if (Input.GetAxisRaw("Horizontal") != 0f || Input.GetAxisRaw("Vertical") != 0f)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        // Gestion de la lecture audio en fonction du mouvement
        if (isMoving)
        {
            // Démarrage de la lecture audio si elle n'est pas en cours
            if (!footstepAudio.isPlaying)
            {
                footstepAudio.Play();
            }
        }
        else
        {
            // Arrêt de la lecture audio si elle est en cours
            if (footstepAudio.isPlaying)
            {
                footstepAudio.Stop();
            }
        }
    }

    /// <summary>
    /// Méthode pour réinitialiser le FootstepController.
    /// </summary>
    public void ResetFootstepController()
    {
        // Arrêt de la lecture audio si elle est en cours
        if (footstepAudio.isPlaying)
        {
            footstepAudio.Stop();
        }
    }
}
