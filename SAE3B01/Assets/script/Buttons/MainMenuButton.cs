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
        // Configuration des positions et du dialogue avant de charger la scène
        dialogueSetup();
        classRoomSetup();
    }

    /// <summary>
    /// Configure les positions et le dialogue avant de charger la scène.
    /// </summary>
    public void onPlayButtonPressed()
    {
        playerInfoDispayScript.showOnUI();
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
    public void dialogueSetup()
    {
        filePath = Application.dataPath + "/SaveJson/dialogueManager.json";
        DialogueSelector dialogueSelector = new DialogueSelector
        {
            repertory = "MakssoudIntro"
        };

        string updatedJson = JsonUtility.ToJson(dialogueSelector);

        // écriture du fichier JSON dans le chemin splifié・
        File.WriteAllText(filePath, updatedJson);
    }

    public void classRoomSetup()
    {
        filePath = Application.dataPath + "/SaveJson/classroom.json";
        Classroom classroom = new Classroom
        {
            classroomName = "000"
        };

        string updatedJson = JsonUtility.ToJson(classroom);

        // écriture du fichier JSON dans le chemin spécifique・
        File.WriteAllText(filePath, updatedJson);
    }

    public void eraseDB()
    {
        dbManager = new DBManager();
        dbManager.Droptable("Dialogues");
    }
}
