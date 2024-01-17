using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SideMenuButton : MonoBehaviour
{
    public void onMapButtonPressed()
    {
        SceneManager.LoadScene("Map");
    }

    public void onProofButtonPressed()
    {
        SceneManager.LoadScene("Proof");
    }

    public void onQuitButtonPressed() 
    {
        SceneManager.LoadScene("MainMenu");
    }
}
