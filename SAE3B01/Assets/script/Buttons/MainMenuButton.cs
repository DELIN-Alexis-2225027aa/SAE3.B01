using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuButton : MonoBehaviour
{
    public void onPlayButtonPressed()
    {
        SceneManager.LoadScene("MovingPhase");
    }

    public void onQuitButtonPressed()
    {
        Application.Quit();
    }

}
