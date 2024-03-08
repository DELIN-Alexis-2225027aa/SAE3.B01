using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

/// <summary>
/// Gère la reprise du jeu en chargeant la scène "MovingPhase" lorsque la touche Échap est pressée.
/// </summary>
public class GameResume : MonoBehaviour
{
    DBManager dbManager;
    ValluesConvertor valluesConvertor;
    public PosSaver posaver;
    public string sceneToLoad;
    string str;

    void Start()
    {
        // Initialisations nécessaires au démarrage du script
    }

    /// <summary>
    /// Méthode appelée à chaque frame.
    /// </summary>
    void Update()
    {
        // Gestion de la reprise du jeu lorsque la touche Échap est pressée
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            getSceneToLoadName();
            if (mapReturner().Equals("Classroom"))
            {
                SceneManager.LoadScene("Proof");
            }
            else
            {
                SceneManager.LoadScene(sceneToLoad);
            }
        }

        // Gestion du chargement de la scène "Map" lorsque la touche 'm' est pressée
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

    /// <summary>
    /// Renvoie le nom de la scène active.
    /// </summary>
    public string mapReturner()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        return currentScene.name;
    }

    /// <summary>
    /// Chargement de la scène "MovingPhase" lorsqu'un bouton est cliqué.
    /// </summary>
    public void onClick()
    {
        SceneManager.LoadScene("MovingPhase");
    }

    /// <summary>
    /// Charge la scène à reprendre à partir de la base de données.
    /// </summary>
    public string loadSceneToLoad()
    {
        List<List<object>> resultat = dbManager.Select("SceneResume", "sceneToResume", "1");

        foreach (List<object> row in resultat)
        {
            str = valluesConvertor.convertRowToString(row);
        }
        return str;
    }

    /// <summary>
    /// Renvoie le nom de la scène active.
    /// </summary>
    public string getSceneName()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        return currentScene.name;
    }

    /// <summary>
    /// Récupère le nom de la scène à reprendre à partir de la base de données.
    /// </summary>
    public void getSceneToLoadName()
    {
        dbManager = new DBManager();
        valluesConvertor = new ValluesConvertor();
        List<List<object>> resultX = dbManager.Select("SceneResume", "sceneToResume", "1");

        foreach (List<object> row in resultX)
        {
            sceneToLoad = valluesConvertor.convertRowToString(row);
        }
    }
}
