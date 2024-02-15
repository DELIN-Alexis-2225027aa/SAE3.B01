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

    string json;
    string filePath;
    public string sceneToLoad;

    /// <summary>
    /// Méthode appelée au démarrage.
    /// </summary>
    void Start()
    {
        filePath = Application.dataPath + "/SaveJson/playerData.json";
        // Charge la position du joueur au démarrage
        LoadPlayerPosition();
    }

    /// <summary>
    /// Méthode appelée à chaque frame.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown("m"))
        {
            sceneToLoad = "Map";
            SavePlayerPosition();
            SceneManager.LoadScene(sceneToLoad);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            sceneToLoad = "Proof";
            SavePlayerPosition();
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    /// <summary>
    /// Sauvegarde la position actuelle du joueur.
    /// </summary>
    void SavePlayerPosition()
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

        Debug.Log("Player position saved to file.");
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
        else
        {
            Debug.Log("No saved player position found.");
        }
    }
}
