using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

/// <summary>
/// Représente une salle de classe avec un nom associé.
/// </summary>
public class Classroom
{
    public string classroomName;
}

/// <summary>
/// Gère les interactions des boutons dans le menu principal.
/// </summary>
public class MainMenuButton : MonoBehaviour
{
    private TestSQLite testSQLite;  // TestSQLite est une classe, peut-être liée à une base de données SQLite.
    private DBManager dbManager;    // DBManager semble gérer la base de données.
    private PlayerInfoDispayScript playerInfoDispayScript;  // Script pour afficher les informations du joueur.

    string json;

    /// <summary>
    /// Chemin du fichier JSON pour les données du joueur.
    /// </summary>
    string filePath;

    private void Start()
    {
        // Aucune initialisation nécessaire pour le moment.
    }

    /// <summary>
    /// Configure les positions et le dialogue avant de charger la scène.
    /// </summary>
    public void onPlayButtonPressed()
    {
        playerInfoDispayScript.showOnUI();  // Affiche les informations du joueur sur l'interface utilisateur.
    }

    /// <summary>
    /// Charge la scène des paramètres du menu principal.
    /// </summary>
    public void onParametersButtonPressed()
    {
        SceneManager.LoadScene("ParametersMainMenu");
    }

    /// <summary>
    /// Charge la scène du menu principal.
    /// </summary>
    public void onMainMenuButtonPressed()
    {
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Charge la scène d'aide du menu principal.
    /// </summary>
    public void onHelpButtonPressed()
    {
        SceneManager.LoadScene("HelpMainMenu");
    }

    /// <summary>
    /// Quitte l'application.
    /// </summary>
    public void onQuitButtonPressed()
    {
        Application.Quit();  // Ferme l'application.
    }

    /// <summary>
    /// Configure le gestionnaire de dialogue et enregistre les données dans un fichier JSON.
    /// </summary>
    public void eraseDB()
    {
        dbManager = new DBManager();  // Initialise le gestionnaire de base de données.
        dbManager.Droptable("Dialogues");  // Supprime la table "Dialogues" de la base de données.
    }
}
