using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Gère les interactions des boutons dans le menu latéral.
/// </summary>
public class SideMenuButton : MonoBehaviour
{

    /// <summary>
    /// Charge la scène de la carte.
    /// </summary>
    public void onMapButtonPressed()
    {
        SceneManager.LoadScene("Map");
    }

    /// <summary>
    /// Charge la scène de la preuve.
    /// </summary>
    public void onProofButtonPressed()
    {
        SceneManager.LoadScene("Proof");
    }

    /// <summary>
    /// Charge la scène du menu principal.
    /// </summary>
    public void onQuitButtonPressed() 
    {
        SceneManager.LoadScene("MainMenu");
    }
}
