using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class Classroom
{
    public string classroomName;
}

/// <summary>
/// Gère les interactions des boutons dans le menu principal.
/// </summary>
public class MainMenuButton : MonoBehaviour
{
    private TestSQLite testSQLite;
    private DBManager dbManager;
    private PlayerInfoDispayScript playerInfoDispayScript;

    string json;

    /// <summary>
    /// Chemin du fichier JSON pour les données du joueur.
    /// </summary>
    string filePath;

    private void Start()
    {

    }

    /// <summary>
    /// Configure les positions et le dialogue avant de charger la scène.
    /// </summary>
    public void onPlayButtonPressed()
    {
        playerInfoDispayScript.showOnUI();
    }

    public void onParametersButtonPressed()
    {
        SceneManager.LoadScene("ParametersMainMenu");
    }

    public void onMainMenuButtonPressed()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void onHelpButtonPressed()
    {
        SceneManager.LoadScene("HelpMainMenu");
    }

    /// <summary>
    /// Quitte l'application.
    /// </summary>
    public void onQuitButtonPressed()
    {
        Application.Quit();
    }

    /// <summary>
    /// Configure le gestionnaire de dialogue et enregistre les données dans un fichier JSON.
    /// </summary>

    public void eraseDB()
    {
        dbManager = new DBManager();
        dbManager.Droptable("Dialogues");
    }
}
