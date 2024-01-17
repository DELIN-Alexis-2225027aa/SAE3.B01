using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// G�re les interactions des boutons dans le menu lat�ral.
/// </summary>
public class SideMenuButton : MonoBehaviour
{

    /// <summary>
    /// Charge la sc�ne de la carte.
    /// </summary>
    public void onMapButtonPressed()
    {
        SceneManager.LoadScene("Map");
    }

    /// <summary>
    /// Charge la sc�ne de la preuve.
    /// </summary>
    public void onProofButtonPressed()
    {
        SceneManager.LoadScene("Proof");
    }

    /// <summary>
    /// Charge la sc�ne du menu principal.
    /// </summary>
    public void onQuitButtonPressed() 
    {
        SceneManager.LoadScene("MainMenu");
    }
}
