using UnityEngine;

public class FootstepController : MonoBehaviour
{
    public GameObject footstepObject;
    private AudioSource footstepAudio;

    void Start()
    {
        footstepAudio = footstepObject.GetComponent<AudioSource>();
        if (footstepAudio == null)
        {
            Debug.LogError("Footstep GameObject doesn't contain an AudioSource!");
        }
    }

    void Update()
    {
        bool isMoving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) ||
                        Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow);

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
}
