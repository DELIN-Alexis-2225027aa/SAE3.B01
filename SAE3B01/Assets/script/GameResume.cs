
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.IO;



/// <summary>
/// Gère la reprise du jeu en chargeant la scènne "MovingPhase" lorsque la touche ﾉchap est pressée.
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
    /// Méthode appelée ・chaque frame.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))   
        {
            getSceneToLoadName();
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

    public string loadSceneToLoad()
    {
        List<List<object>> resultat = dbManager.Select("SceneResume", "sceneToResume", "1" );
        
        foreach (List<object> row in resultat)
        {
            str = valluesConvertor.convertRowToString(row);
        }
        return str;
    }

    public string getSceneName()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        return currentScene.name;
    }

    public void getSceneToLoadName(){

        dbManager = new DBManager();
        valluesConvertor = new ValluesConvertor();
        List<List<object>> resultX = dbManager.Select("SceneResume", "sceneToResume", "1");

            
        foreach (List<object> row in resultX)
        {
            sceneToLoad = valluesConvertor.convertRowToString(row);
        }
    }
}
