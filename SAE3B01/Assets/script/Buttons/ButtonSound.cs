using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public AudioSource audio;
    public AudioClip hoverSound;
    public AudioClip clickSound;

    public void OnMouseEnter()
    {
        audio.PlayOneShot(hoverSound);
    }

    public void OnMouseDown()
    {
        audio.PlayOneShot(clickSound);
    }
}
