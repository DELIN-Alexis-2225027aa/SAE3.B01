using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Gère le lecteur vidéo et la transition vers une autre scène une fois la vidéo terminée.
/// </summary>
public class VideoPlayerManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    // Start is called before the first frame update
    void Start()
    {
        // Ajoute un gestionnaire d'événements pour détecter la fin de la vidéo
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    // Update is called once per frame
    void Update()
    {
        // Vérifie si les touches spécifiées sont enfoncées pour forcer la fin de la vidéo
        if (Input.GetKey(KeyCode.G) && Input.GetKey(KeyCode.O) && Input.GetKey(KeyCode.Q) && Input.GetKey(KeyCode.T))
        {
            OnVideoFinished();
        }
    }

    /// <summary>
    /// Gère l'événement de fin de la vidéo.
    /// </summary>
    /// <param name="vp">Le lecteur vidéo qui a déclenché l'événement.</param>
    void OnVideoFinished(VideoPlayer vp)
    {
        // Charge la scène spécifiée lorsque la vidéo est terminée
        SceneManager.LoadScene("IntroMmeMakssoud");
    }

    /// <summary>
    /// Gère l'événement de fin de la vidéo.
    /// </summary>
    void OnVideoFinished()
    {
        // Arrête le lecteur vidéo et charge la scène spécifiée lorsque la vidéo est terminée
        videoPlayer.Stop();
        SceneManager.LoadScene("IntroMmeMakssoud");
    }
}
