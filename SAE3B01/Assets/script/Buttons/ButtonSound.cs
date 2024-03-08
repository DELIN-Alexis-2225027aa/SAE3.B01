using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// G�re les sons associ�s aux interactions avec un bouton.
/// </summary>
public class ButtonSound : MonoBehaviour
{
    public AudioSource audio;  // Composant AudioSource pour jouer les sons.
    public AudioClip hoverSound;  // Son jou� lorsqu'on survole le bouton.
    public AudioClip clickSound;  // Son jou� lorsqu'on clique sur le bouton.

    /// <summary>
    /// Appel� lorsque la souris entre dans la zone du bouton.
    /// Joue le son de survol.
    /// </summary>
    public void OnMouseEnter()
    {
        audio.PlayOneShot(hoverSound);
    }

    /// <summary>
    /// Appel� lorsqu'un clic de souris est d�tect� sur le bouton.
    /// Joue le son de clic.
    /// </summary>
    public void OnMouseDown()
    {
        audio.PlayOneShot(clickSound);
    }
}
