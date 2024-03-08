using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Contr�leur g�rant les empreintes de pas du joueur.
/// </summary>
public class FootstepController : MonoBehaviour
{
    /// <summary>
    /// Objet utilis� pour afficher les empreintes de pas.
    /// </summary>
    public GameObject footstepObject;

    /// <summary>
    /// Clip audio pour les empreintes de pas.
    /// </summary>
    public AudioClip footstepClip;

    /// <summary>
    /// Composant AudioSource utilis� pour jouer les empreintes de pas.
    /// </summary>
    private AudioSource footstepAudio;

    /// <summary>
    /// Indique si le personnage est en mouvement.
    /// </summary>
    private bool isMoving;

    /// <summary>
    /// M�thode appel�e au d�marrage.
    /// </summary>
    void Start()
    {
        // R�cup�ration du composant AudioSource de l'objet des empreintes de pas
        footstepAudio = footstepObject.GetComponent<AudioSource>();

        // V�rification si l'objet contient un composant AudioSource
        if (footstepAudio == null)
        {
            Debug.LogError("Footstep GameObject doesn't contain an AudioSource!");
        }

        // Initialisation de la variable isMoving � false
        isMoving = false;
    }

    /// <summary>
    /// M�thode appel�e � la destruction de l'objet.
    /// </summary>
    void OnDestroy()
    {
        // Arr�t de la lecture audio si elle est en cours
        if (footstepAudio.isPlaying)
        {
            footstepAudio.Stop();
        }
    }

    /// <summary>
    /// M�thode appel�e lorsque la sc�ne est d�charg�e.
    /// </summary>
    void OnSceneUnloaded(Scene scene)
    {
        // R�initialisation du FootstepController et destruction de l'objet
        ResetFootstepController();
        Destroy(gameObject);
    }

    /// <summary>
    /// M�thode appel�e � chaque frame fixe.
    /// </summary>
    void FixedUpdate()
    {
        // V�rification si le personnage est en mouvement
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
            // D�marrage de la lecture audio si elle n'est pas en cours
            if (!footstepAudio.isPlaying)
            {
                footstepAudio.Play();
            }
        }
        else
        {
            // Arr�t de la lecture audio si elle est en cours
            if (footstepAudio.isPlaying)
            {
                footstepAudio.Stop();
            }
        }
    }

    /// <summary>
    /// M�thode pour r�initialiser le FootstepController.
    /// </summary>
    public void ResetFootstepController()
    {
        // Arr�t de la lecture audio si elle est en cours
        if (footstepAudio.isPlaying)
        {
            footstepAudio.Stop();
        }
    }
}
