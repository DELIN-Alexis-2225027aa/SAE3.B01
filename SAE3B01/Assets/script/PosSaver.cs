using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;

/// <summary>
/// Représente les données du joueur.
/// </summary>
[System.Serializable]
public class PlayerData
{
    public float x;
    public float y;
    public float z;
}

/// <summary>
/// Gère la sauvegarde et le chargement de la position du joueur.
/// </summary>
public class PosSaver : MonoBehaviour
{

    
    string sceneName;
    string json;
    string filePath;
    public string sceneToLoad;

    /// <summary>
    /// Méthode appelée au démarrage.
    /// </summary>
    void Start()
    {
        if (getSceneName().Equals("MovingPhase"))
        {
            filePath = Application.dataPath + "/SaveJson/playerData.json";
            // Charge la position du joueur au démarrage
            LoadPlayerPosition();
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
                SaveScneToLoadWhenReturn();
                SceneManager.LoadScene(sceneToLoad);
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SavePlayerPosition();
                sceneToLoad = "Proof";
                SaveScneToLoadWhenReturn();
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
        // Obtient la position actuelle du joueur
        Vector3 playerPos = transform.position;

        // Convertir la position du joueur en une classe PlayerData
        PlayerData playerData = new PlayerData
        {
            x = playerPos.x,
            y = playerPos.y,
            z = playerPos.z
        };

        // Convertir la classe en JSON et écrire dans le fichier
        string updatedJson = JsonUtility.ToJson(playerData);
        File.WriteAllText(filePath, updatedJson);

    }

    public void SaveScneToLoadWhenReturn()
    {
        filePath = Application.dataPath + "/SaveJson/sceneToLoad.json";

        // Convertir la position du joueur en une classe PlayerData
        LastMainScene lastMainScene = new LastMainScene
        {
            lastScene = getSceneName()
        };

        // Convertir la classe en JSON et écrire dans le fichier
        string updatedJson = JsonUtility.ToJson(lastMainScene);
        File.WriteAllText(filePath, updatedJson);

        filePath = Application.dataPath + "/SaveJson/playerData.json";

    }

    /// <summary>
    /// Charge la position du joueur depuis le fichier JSON.
    /// </summary>
    void LoadPlayerPosition()
    {
        if (File.Exists(filePath))
        {
            // Lire le JSON depuis le fichier
            string json = File.ReadAllText(filePath);

            // Convertir le JSON en classe PlayerData
            PlayerData loadedPlayerData = JsonUtility.FromJson<PlayerData>(json);

            // Créer un Vector3 ・partir des données chargées
            Vector3 loadedPlayerPos = new Vector3(loadedPlayerData.x, loadedPlayerData.y, loadedPlayerData.z);

            // Appliquer la position chargée au joueur
            transform.position = loadedPlayerPos;
        }
    }
}
