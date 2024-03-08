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
    // Référence au gestionnaire de base de données
    DBManager dbManager;

    // Référence au convertisseur de valeurs
    ValluesConvertor valluesConvertor;

    // Référence à la position du joueur
    public Transform playerPos;

    // Nom de la dernière scène chargée
    string lastScene;

    // Nom de la scène actuelle
    string sceneName;

    // Nom de la scène à charger
    string sceneToLoad;

    // Valeur de la position X en chaîne
    string strXValue;

    // Valeur de la position Y en chaîne
    string strYValue;

    // Valeur de la position X en float
    float xValue;

    // Valeur de la position Y en float
    float yValue;

    /// <summary>
    /// Méthode appelée au démarrage.
    /// </summary>
    void Start()
    {
        // Initialisation du gestionnaire de base de données
        dbManager = new DBManager();

        // Initialisation du convertisseur de valeurs
        valluesConvertor = new ValluesConvertor();

        // Vérification de la scène actuelle
        if (getSceneName().Equals("MovingPhase"))
        {
            // Chargement de la position du joueur
            float[] vector2Pos = loadPlayerPos();
            Vector3 newPos = new Vector3(vector2Pos[0], vector2Pos[1], 0f);
            playerPos.position = newPos;
        }
    }

    /// <summary>
    /// Méthode appelée à chaque frame.
    /// </summary>
    void Update()
    {
        // Vérification de la scène actuelle
        if (getSceneName().Equals("MovingPhase"))
        {
            // Gestion des événements clavier pour sauvegarder la position et changer de scène
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
    /// Méthode pour obtenir le nom de la scène actuelle.
    /// </summary>
    public string getSceneName()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        return currentScene.name;
    }

    /// <summary>
    /// Méthode pour sauvegarder la position actuelle du joueur.
    /// </summary>
    public void SavePlayerPosition()
    {
        savePlayerPos(playerPos.position.x, playerPos.position.y);
    }

    /// <summary>
    /// Méthode pour sauvegarder le nom de la scène à charger lorsque le joueur revient.
    /// </summary>
    public void SaveScneToLoadWhenReturn(string scneneName)
    {
        dbManager.DeleteEverythingFromTable("SceneResume");
        dbManager.InsertOneValue("SceneResume", scneneName);
    }

    /// <summary>
    /// Méthode pour charger la position du joueur depuis la base de données.
    /// </summary>
    void LoadPlayerPosition(ValluesConvertor valluesConvertor, DBManager dbManager)
    {
        float[] pos = loadPlayerPos();
        Vector3 loadedPlayerPos = new Vector3(pos[0], pos[1], 0f);
        transform.position = loadedPlayerPos;
    }

    /// <summary>
    /// Méthode de remplacement de caractères dans une chaîne.
    /// </summary>
    public static string ReplaceF(string input)
    {
        return input.Replace("f", "");
    }

    /// <summary>
    /// Méthode pour sauvegarder la position du joueur dans la base de données.
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
    /// Méthode pour charger la position du joueur depuis la base de données.
    /// </summary>
    public float[] loadPlayerPos()
    {
        dbManager = new DBManager();

        // Chargement de la position X
        List<List<object>> resultX = dbManager.Select("PlayerPos", "xPos", "1");
        foreach (List<object> row in resultX)
        {
            strXValue = valluesConvertor.convertRowToString(row);
            strXValue = strXValue.Replace(",", ".");
            xValue = float.Parse(strXValue, CultureInfo.InvariantCulture);
        }

        // Chargement de la position Y
        List<List<object>> resultY = dbManager.Select("PlayerPos", "yPos", "1");
        foreach (List<object> row in resultY)
        {
            strYValue = valluesConvertor.convertRowToString(row);
            strYValue = strYValue.Replace(",", ".");
            yValue = float.Parse(strYValue, CultureInfo.InvariantCulture);
        }

        // Correction des valeurs de retour
        float yPosReturn = yValue / 1000000;
        float xPosReturn = xValue / 100000;
        float[] posToReturn = { xPosReturn, yPosReturn };
        return posToReturn;
    }
}
