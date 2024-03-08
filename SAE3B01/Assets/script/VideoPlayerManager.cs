using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// G�re le lecteur vid�o et la transition vers une autre sc�ne une fois la vid�o termin�e.
/// </summary>
public class VideoPlayerManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    // Start is called before the first frame update
    void Start()
    {
        // Ajoute un gestionnaire d'�v�nements pour d�tecter la fin de la vid�o
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    // Update is called once per frame
    void Update()
    {
        // V�rifie si les touches sp�cifi�es sont enfonc�es pour forcer la fin de la vid�o
        if (Input.GetKey(KeyCode.G) && Input.GetKey(KeyCode.O) && Input.GetKey(KeyCode.Q) && Input.GetKey(KeyCode.T))
        {
            OnVideoFinished();
        }
    }

    /// <summary>
    /// G�re l'�v�nement de fin de la vid�o.
    /// </summary>
    /// <param name="vp">Le lecteur vid�o qui a d�clench� l'�v�nement.</param>
    void OnVideoFinished(VideoPlayer vp)
    {
        // Charge la sc�ne sp�cifi�e lorsque la vid�o est termin�e
        SceneManager.LoadScene("IntroMmeMakssoud");
    }

    /// <summary>
    /// G�re l'�v�nement de fin de la vid�o.
    /// </summary>
    void OnVideoFinished()
    {
        // Arr�te le lecteur vid�o et charge la sc�ne sp�cifi�e lorsque la vid�o est termin�e
        videoPlayer.Stop();
        SceneManager.LoadScene("IntroMmeMakssoud");
    }
}
