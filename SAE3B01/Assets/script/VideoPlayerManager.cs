using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Gère la lecture d'une vidéo et le chargement d'une scène à la fin.
/// </summary>
public class VideoPlayerManager : MonoBehaviour
{
    /// <summary>
    /// Référence au composant VideoPlayer.
    /// </summary>
    public VideoPlayer videoPlayer;

    /// <summary>
    /// Appelée au démarrage du script.
    /// </summary>
    void Start()
    {
        // Abonne la méthode OnVideoFinished à l'événement loopPointReached du VideoPlayer.
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    /// <summary>
    /// Appelée à chaque frame.
    /// </summary>
    void Update()
    {
        // Vérifie si les touches G, O, Q et T sont enfoncées simultanément.
        if (Input.GetKey(KeyCode.G) && Input.GetKey(KeyCode.O) && Input.GetKey(KeyCode.Q) && Input.GetKey(KeyCode.T))
        {
            // Déclenche la fin de la vidéo manuellement.
            OnVideoFinished();
        }
    }

    /// <summary>
    /// Appelée lorsque la vidéo est terminée.
    /// </summary>
    /// <param name="vp">Le VideoPlayer associé à l'événement.</param>
    void OnVideoFinished(VideoPlayer vp)
    {
        // Charge la scène "IntroMmeMakssoud" à la fin de la vidéo.
        SceneManager.LoadScene("IntroMmeMakssoud");
    }

    /// <summary>
    /// Arrête la lecture de la vidéo et charge la scène "IntroMmeMakssoud".
    /// </summary>
    void OnVideoFinished()
    {
        // Arrête la lecture de la vidéo.
        videoPlayer.Stop();

        // Charge la scène "IntroMmeMakssoud".
        SceneManager.LoadScene("IntroMmeMakssoud");
    }
}
