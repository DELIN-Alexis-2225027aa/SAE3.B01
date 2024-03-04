
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class LastMainScene
{
    public string lastScene;
}

/// <summary>
/// Gère la reprise du jeu en chargeant la scènne "MovingPhase" lorsque la touche ﾉchap est pressée.
/// </summary>
public class GameResume : MonoBehaviour
{
    public PosSaver posaver;
    public string sceneToLoad;
    public string filePath;

    void Start()
    {
        filePath = Application.dataPath + "/SaveJson/sceneToLoad.json";
        //loadSceneToLoad();
        if (mapReturner().Equals("Classroom"))
        {
            posaver.SaveScneToLoadWhenReturn();
        }
    }

    /// <summary>
    /// Méthode appelée ・chaque frame.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            if (mapReturner().Equals("Classroom"))
            {
                SceneManager.LoadScene("Proof");
            }
            else
            {
                SceneManager.LoadScene(sceneToLoad);
            }
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        if (Input.GetKeyDown("m"))
        {
            if (mapReturner().Equals("Map"))
            {
                SceneManager.LoadScene(sceneToLoad);
            }
            else
            {
                SceneManager.LoadScene("Map");
            }
        }
    }

    public string mapReturner()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        return currentScene.name;

    }

    public void onClick()
    {
        SceneManager.LoadScene("MovingPhase");
    }

    public void loadSceneToLoad()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            LastMainScene lastMainScene = JsonUtility.FromJson<LastMainScene>(json);
            sceneToLoad = lastMainScene.lastScene;
        }

    }
}
