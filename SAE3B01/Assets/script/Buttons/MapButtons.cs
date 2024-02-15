using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

/// <summary>
/// Gère les interactions des boutons sur la carte.
/// </summary>
public class MapButtons : MonoBehaviour
{
    string json;

    /// <summary>
    /// Chemin du fichier JSON pour les données du joueur.
    /// </summary>
    string filePath;

    /// <summary>
    /// Etage du joueur.
    /// </summary>
    public int floor;

    [SerializeField] private Transform tr;

    /// <summary>
    /// Méthode appelée au démarrage.
    /// </summary>
    void Start()
    {
        filePath = Application.persistentDataPath + "/SaveJson/playerData.json";
        VerifyStartFloor();
    }

    /// <summary>
    /// Vérifie le niveau initial du joueur en fonction de sa position chargée depuis le fichier.
    /// </summary>
    void VerifyStartFloor()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            PlayerData loadedPlayerData = JsonUtility.FromJson<PlayerData>(json);
            // Trouver l'etage en fonction de la position y du joueur
            if (loadedPlayerData.y > -50f)
            {
                floor = 1;
            }else if (loadedPlayerData.y < -150f)
            {
                floor = 3;
            }
            else
            {
                floor = 2;
            }
            PlaceCrosshair();
        }
        else
        {
            Debug.Log("No saved player position found.");
        }
    }

    /// <summary>
    /// Place le Crosshair sur la carte en fonction du niveau actuel.
    /// </summary>
    void PlaceCrosshair()
    {
        if(floor == 1)
        {
            Vector3 CrosshairPos = new Vector3(-45f, -0f, 0f);
            tr.position = CrosshairPos;
        }
        else if( floor == 2 ) 
        {
            Vector3 CrosshairPos = new Vector3(-45f, -30f, 0f);
            tr.position = CrosshairPos;
        }
        else
        {
            Vector3 CrosshairPos = new Vector3(-45f, -60f, 0f);
            tr.position = CrosshairPos;
        }
    }

    /// <summary>
    /// Place le réticule sur la carte au niveau 3.
    /// </summary>
    public void OnButton3Pressed()
    {
        Vector3 CrosshairPos = new Vector3(-45f, -60f, 0f);
        tr.position = CrosshairPos;
    }

    /// <summary>
    /// Place le réticule sur la carte au niveau 2.
    /// </summary>
    public void OnButton2Pressed()
    {
        Vector3 CrosshairPos = new Vector3(-45f, -30f, 0f);
        tr.position = CrosshairPos;
    }

    /// <summary>
    /// Place le réticule sur la carte au niveau 1.
    /// </summary>
    public void OnButton1Pressed()
    {
        Vector3 CrosshairPos = new Vector3(-45f, -0f, 0f);
        tr.position = CrosshairPos;
    }
}
