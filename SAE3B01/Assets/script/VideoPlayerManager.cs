using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// G�re la lecture d'une vid�o et le chargement d'une sc�ne � la fin.
/// </summary>
public class VideoPlayerManager : MonoBehaviour
{
    /// <summary>
    /// R�f�rence au composant VideoPlayer.
    /// </summary>
    public VideoPlayer videoPlayer;

    /// <summary>
    /// Appel�e au d�marrage du script.
    /// </summary>
    void Start()
    {
        // Abonne la m�thode OnVideoFinished � l'�v�nement loopPointReached du VideoPlayer.
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    /// <summary>
    /// Appel�e � chaque frame.
    /// </summary>
    void Update()
    {
        // V�rifie si les touches G, O, Q et T sont enfonc�es simultan�ment.
        if (Input.GetKey(KeyCode.G) && Input.GetKey(KeyCode.O) && Input.GetKey(KeyCode.Q) && Input.GetKey(KeyCode.T))
        {
            // D�clenche la fin de la vid�o manuellement.
            OnVideoFinished();
        }
    }

    /// <summary>
    /// Appel�e lorsque la vid�o est termin�e.
    /// </summary>
    /// <param name="vp">Le VideoPlayer associ� � l'�v�nement.</param>
    void OnVideoFinished(VideoPlayer vp)
    {
        // Charge la sc�ne "IntroMmeMakssoud" � la fin de la vid�o.
        SceneManager.LoadScene("IntroMmeMakssoud");
    }

    /// <summary>
    /// Arr�te la lecture de la vid�o et charge la sc�ne "IntroMmeMakssoud".
    /// </summary>
    void OnVideoFinished()
    {
        // Arr�te la lecture de la vid�o.
        videoPlayer.Stop();

        // Charge la sc�ne "IntroMmeMakssoud".
        SceneManager.LoadScene("IntroMmeMakssoud");
    }
}
