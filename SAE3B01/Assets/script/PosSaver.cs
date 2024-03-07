using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using System.Globalization;


/// <summary>
/// Gère la sauvegarde et le chargement de la position du joueur.
/// </summary>
public class PosSaver : MonoBehaviour
{
    DBManager dbManager;
    ValluesConvertor valluesConvertor;

    public Transform playerPos;

    string lastScene;
    string sceneName;
    string sceneToLoad;
    string strXValue;
    string strYValue;
    float xValue;
    float yValue;

    /// <summary>
    /// Méthode appelée au démarrage.
    /// </summary>
    void Start()
    {
        dbManager = new DBManager();
        valluesConvertor = new ValluesConvertor();

        if (getSceneName().Equals("MovingPhase"))
        {
            // Charge la position du joueur au démarrage
            LoadPlayerPosition(valluesConvertor, dbManager);
        }
    }

    /// <summary>
    /// Méthode appelée à chaque frame.
    /// </summary>
    void Update()
    {
        if (getSceneName().Equals("MovingPhase"))
        {
            if (Input.GetKeyDown("m"))
            {
                SavePlayerPosition();
                sceneToLoad = "Map";
                SaveScneToLoadWhenReturn(getSceneName());
                SceneManager.LoadScene(sceneToLoad);
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SavePlayerPosition();
                sceneToLoad = "Proof";
                SaveScneToLoadWhenReturn(getSceneName());
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }

    public string getSceneName()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        return currentScene.name;
    }

    /// <summary>
    /// Sauvegarde la position actuelle du joueur.
    /// </summary>
    public void SavePlayerPosition()
    {
        savePlayerPos(playerPos.position.x, playerPos.position.y);
    }

    public void SaveScneToLoadWhenReturn(string scneneName)
    {
        dbManager.DeleteEverythingFromTable("SceneResume");
        Debug.Log(scneneName);
        dbManager.InsertOneValue("SceneResume", scneneName);
    }

    /// <summary>
    /// Charge la position du joueur depuis le fichier JSON.
    /// </summary>
    void LoadPlayerPosition(ValluesConvertor valluesConvertor, DBManager dbManager)
    {
        float[] pos = loadPlayerPos();

        // Créer un Vector3 ・partir des données chargées
        Vector3 loadedPlayerPos = new Vector3(pos[0], pos[1] , 0f);

        // Appliquer la position chargée au joueur
        transform.position = loadedPlayerPos;
    }

    public static string ReplaceF(string input)
    {
        // Remplace toutes les occurrences de 'f' par une chaîne vide
        return input.Replace("f", "");
    }

    public void savePlayerPos(float xPos, float yPos)
    {
        dbManager = new DBManager();

        dbManager.DeleteEverythingFromTable("PlayerPos");
        
        string xPosStr = xPos.ToString();
        string yPosStr = yPos.ToString();
        string yPosChecked = ReplaceF(yPosStr);
        string xPosChecked = ReplaceF(xPosStr);
        string[] posToSave = { xPosChecked, yPosChecked};
        //Assets\script\PosSaver.cs(134,39): error CS1503: Argument 1: cannot convert from 'float' to 'string'
        dbManager.Insert("PlayerPos", posToSave);
    }

    public float[] loadPlayerPos()
    {
        dbManager = new DBManager();

        List<List<object>> resultX = dbManager.Select("PlayerPos", "xPos", "1");

            
        foreach (List<object> row in resultX)
        {
            strXValue = valluesConvertor.convertRowToString(row);
            strXValue = strXValue.Replace(" ", "");
            xValue = float.Parse(strXValue, CultureInfo.InvariantCulture);
        }

        List<List<object>> resultY = dbManager.Select("PlayerPos", "yPos", "1");

            
        foreach (List<object> row in resultY)
        {
            strYValue = valluesConvertor.convertRowToString(row);
            strYValue = strYValue.Replace(" ", "");
            yValue = float.Parse(strYValue, CultureInfo.InvariantCulture);
        }

        float yPosReturn = yValue/1000000;
        float xPosReturn = xValue/100000;
        float[] posToReturn = {xPosReturn,yPosReturn};
        return posToReturn;
    }
}
