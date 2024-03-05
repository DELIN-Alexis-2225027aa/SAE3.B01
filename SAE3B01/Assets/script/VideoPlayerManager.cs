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
        
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        SceneManager.LoadScene("IntroMmeMakssoud");    
    }

}
