using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;


/// <summary>
/// Gère les interactions des boutons dans le menu principal.
/// </summary>
public class MainMenuButton : MonoBehaviour
{
    string json;

    /// <summary>
    /// Chemin du fichier JSON pour les données du joueur.
    /// </summary>
    string filePath;

    private void Start()
    {
        // Configuration des positions et du dialogue avant de charger la scène
        posSetup();
        dialogueSetup();
    }

    /// <summary>
    /// Configure les positions et le dialogue avant de charger la scène.
    /// </summary>
    public void onPlayButtonPressed()
    {
        SceneManager.LoadScene("dialogue");
    }

    /// <summary>
    /// Quitte l'application.
    /// </summary>
    public void onQuitButtonPressed()
    {
        Application.Quit();
    }

    /// <summary>
    /// Configure les positions initiales du joueur et enregistre les données dans un fichier JSON.
    /// </summary>
    public void posSetup()
    {
        filePath = Application.dataPath+ "/SaveJson/playerData.json";
        PlayerData playerData = new PlayerData
        {
            x = 0,
            y = 0,
            z = 0
        };

        string updatedJson = JsonUtility.ToJson(playerData);
        File.WriteAllText(filePath, updatedJson);
    }

    /// <summary>
    /// Configure le gestionnaire de dialogue et enregistre les données dans un fichier JSON.
    /// </summary>
    public void dialogueSetup()
    {
        filePath = Application.dataPath + "/SaveJson/dialogueManager.json";
        Debug.Log(filePath);
        DialogueSelector dialogueSelector = new DialogueSelector
        {
            repertory = "MakssoudIntro"
        };

        string updatedJson = JsonUtility.ToJson(dialogueSelector);

        // Écriture du fichier JSON dans le chemin spécifié
        File.WriteAllText(filePath, updatedJson);
    }
}
