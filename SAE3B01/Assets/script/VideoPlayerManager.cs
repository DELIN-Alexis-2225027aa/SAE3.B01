using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine;
using UnityEngine.SceneManagement;


public class VideoPlayerManager : MonoBehaviour
{

    public VideoPlayer videoPlayer;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.G) && Input.GetKey(KeyCode.O)&& Input.GetKey(KeyCode.Q)&& Input.GetKey(KeyCode.T))
        {
            OnVideoFinished();
        }
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        SceneManager.LoadScene("IntroMmeMakssoud");    
    }

    void OnVideoFinished()
    {
        videoPlayer.Stop();
        SceneManager.LoadScene("IntroMmeMakssoud");
    }
}
