using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;


public class MainMenuButton : MonoBehaviour
{
    string json;
    string filePath;

    public void onPlayButtonPressed()
    {
        posSetup();
        dialogueSetup();
        SceneManager.LoadScene("dialogue");
    }

    public void onQuitButtonPressed()
    {
        Application.Quit();
    }

    public void posSetup()
    {
        filePath = Application.dataPath + "/SaveJson/playerData.json";
        PlayerData playerData = new PlayerData
        {
            x = 0,
            y = 0,
            z = 0
        };

        string updatedJson = JsonUtility.ToJson(playerData);
        File.WriteAllText(filePath, updatedJson);
    }

    public void dialogueSetup()
    {
        filePath = Application.dataPath + "/SaveJson/dialogueManager.json";
        DialogueSelector dialogueSelector = new DialogueSelector
        {
            repertory = "MakssoudIntro"
        };
        string updatedJson = JsonUtility.ToJson(dialogueSelector);
        File.WriteAllText(filePath, updatedJson);
    }
}
