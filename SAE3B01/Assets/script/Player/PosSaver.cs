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
        // Initialise les gestionnaires de base de données et de conversion de valeurs.
        dbManager = new DBManager();
        valluesConvertor = new ValluesConvertor();

        // Si la scène active est "MovingPhase", charge la position du joueur.
        if (getSceneName().Equals("MovingPhase"))
        {
            float[] vector2Pos = loadPlayerPos();  // Renommez la variable pour éviter la confusion
            Vector3 newPos = new Vector3(vector2Pos[0], vector2Pos[1], 0f);  // Correction de la virgule
            playerPos.position = newPos;
        }
    }

    /// <summary>
    /// Méthode appelée à chaque frame.
    /// </summary>
    void Update()
    {
        // Si la scène active est "MovingPhase", permet au joueur de sauvegarder sa position en appuyant sur 'm' ou de revenir au menu principal en appuyant sur la touche Échap.
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

    /// <summary>
    /// Récupère le nom de la scène active.
    /// </summary>
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

    /// <summary>
    /// Sauvegarde la scène vers laquelle le joueur retournera lors de la prochaine session.
    /// </summary>
    public void SaveScneToLoadWhenReturn(string scneneName)
    {
        dbManager.DeleteEverythingFromTable("SceneResume");
        dbManager.InsertOneValue("SceneResume", scneneName);
    }

    /// <summary>
    /// Charge la position du joueur depuis la base de données.
    /// </summary>
    void LoadPlayerPosition(ValluesConvertor valluesConvertor, DBManager dbManager)
    {
        float[] pos = loadPlayerPos();

        // Crée un Vector3 à partir des données chargées
        Vector3 loadedPlayerPos = new Vector3(pos[0], pos[1], 0f);

        // Applique la position chargée au joueur
        transform.position = loadedPlayerPos;
    }

    /// <summary>
    /// Remplace toutes les occurrences de 'f' dans la chaîne d'entrée par une chaîne vide.
    /// </summary>
    public static string ReplaceF(string input)
    {
        // Remplace toutes les occurrences de 'f' par une chaîne vide
        return input.Replace("f", "");
    }

    /// <summary>
    /// Sauvegarde la position du joueur dans la base de données.
    /// </summary>
    public void savePlayerPos(float xPos, float yPos)
    {
        dbManager = new DBManager();

        dbManager.DeleteEverythingFromTable("PlayerPos");
        string xPosStr = xPos.ToString();
        string yPosStr = yPos.ToString();
        string yPosChecked = ReplaceF(yPosStr);
        string xPosChecked = ReplaceF(xPosStr);
        string[] posToSave = { xPosChecked, yPosChecked };
        dbManager.Insert("PlayerPos", posToSave);
    }

    /// <summary>
    /// Charge la position du joueur depuis la base de données.
    /// </summary>
    public float[] loadPlayerPos()
    {
        dbManager = new DBManager();

        // Récupère la position X du joueur depuis la base de données
        List<List<object>> resultX = dbManager.Select("PlayerPos", "xPos", "1");
        foreach (List<object> row in resultX)
        {
            strXValue = valluesConvertor.convertRowToString(row);
            strXValue = strXValue.Replace(",", ".");
            xValue = float.Parse(strXValue, CultureInfo.InvariantCulture);
        }

        // Récupère la position Y du joueur depuis la base de données
        List<List<object>> resultY = dbManager.Select("PlayerPos", "yPos", "1");
        foreach (List<object> row in resultY)
        {
            strYValue = valluesConvertor.convertRowToString(row);
            strYValue = strYValue.Replace(",", ".");
            yValue = float.Parse(strYValue, CultureInfo.InvariantCulture);
        }

        // Convertit les valeurs de la base de données pour obtenir les coordonnées correctes
        float yPosReturn = yValue / 1000000;
        float xPosReturn = xValue / 100000;
        float[] posToReturn = { xPosReturn, yPosReturn };
        return posToReturn;
    }
}
