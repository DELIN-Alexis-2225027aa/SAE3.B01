using UnityEngine;

public class MainMenuMusicManager : MonoBehaviour
{
    public AudioClip ambianceMusic;
    private AudioSource audioSource;

    private void Start()
    {
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.clip = ambianceMusic;
        audioSource.loop = true;
        audioSource.Play();
    }

    private void OnDestroy()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }
}
