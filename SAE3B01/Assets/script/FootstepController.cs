using UnityEngine;
using UnityEngine.SceneManagement;

public class FootstepController : MonoBehaviour
{
    public GameObject footstepObject;
    public AudioClip footstepClip;
    private AudioSource footstepAudio;
    private bool isMoving;

    void Start()
    {
        footstepAudio = footstepObject.GetComponent<AudioSource>();
        if (footstepAudio == null)
        {
            Debug.LogError("Footstep GameObject doesn't contain an AudioSource!");
        }
        isMoving = false;
    }

    void OnDestroy()
    {
        if (footstepAudio.isPlaying)
        {
            footstepAudio.Stop();
        }
    }



    void OnSceneUnloaded(Scene scene)
    {
        ResetFootstepController();
        Destroy(gameObject);
    }

    void FixedUpdate()
    {
        if (Input.GetAxisRaw("Horizontal") != 0f || Input.GetAxisRaw("Vertical") !=0f )
        {
            isMoving = true;
        }else 
        {
            isMoving = false;
        }
        

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
    public void ResetFootstepController()
    {
        if (footstepAudio.isPlaying)
        {
            footstepAudio.Stop();
        }
    }

}
