using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gère les sons associés aux interactions avec un bouton.
/// </summary>
public class ButtonSound : MonoBehaviour
{
    public AudioSource audio;  // Composant AudioSource pour jouer les sons.
    public AudioClip hoverSound;  // Son joué lorsqu'on survole le bouton.
    public AudioClip clickSound;  // Son joué lorsqu'on clique sur le bouton.

    /// <summary>
    /// Appelé lorsque la souris entre dans la zone du bouton.
    /// Joue le son de survol.
    /// </summary>
    public void OnMouseEnter()
    {
        audio.PlayOneShot(hoverSound);
    }

    /// <summary>
    /// Appelé lorsqu'un clic de souris est détecté sur le bouton.
    /// Joue le son de clic.
    /// </summary>
    public void OnMouseDown()
    {
        audio.PlayOneShot(clickSound);
    }
}
