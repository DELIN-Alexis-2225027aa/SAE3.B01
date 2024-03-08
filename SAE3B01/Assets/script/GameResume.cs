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

    }

    /// <summary>
    /// Méthode appelée à chaque frame.
    /// </summary>
    void Update()
    {
        // Gestion du chargement de la scène "MovingPhase" lors de l'appui sur la touche Échap
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            getSceneToLoadName();

            // Vérification si la scène à charger est "Classroom"
            if (mapReturner().Equals("Classroom"))
            {
                Debug.Log("1");
                SceneManager.LoadScene("Proof");
            }
            else
            {
                SceneManager.LoadScene(sceneToLoad);
            }
        }

        // Gestion du chargement de la scène "Map" lors de l'appui sur la touche 'm'
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
    /// Retourne le nom de la scène actuelle.
    /// </summary>
    /// <returns>Le nom de la scène actuelle.</returns>
    public string mapReturner()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        return currentScene.name;
    }

    /// <summary>
    /// Charge la scène "MovingPhase" lorsqu'un bouton est cliqué.
    /// </summary>
    public void onClick()
    {
        SceneManager.LoadScene("MovingPhase");
    }

    /// <summary>
    /// Charge le nom de la scène à reprendre depuis la base de données.
    /// </summary>
    /// <returns>Le nom de la scène à reprendre.</returns>
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
    /// Retourne le nom de la scène actuelle.
    /// </summary>
    /// <returns>Le nom de la scène actuelle.</returns>
    public string getSceneName()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        return currentScene.name;
    }

    /// <summary>
    /// Obtient le nom de la scène à charger depuis la base de données.
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
