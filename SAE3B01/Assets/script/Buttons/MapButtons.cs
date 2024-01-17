using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;


public class MapButtons : MonoBehaviour
{
    string json;
    string filePath;
    public int floor;
    [SerializeField] private Transform tr;

    void Start()
    {
        filePath = Application.dataPath + "/SaveJson/playerData.json";
        VerifyStartFloor();
    }

    void VerifyStartFloor()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            PlayerData loadedPlayerData = JsonUtility.FromJson<PlayerData>(json);
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

    public void OnButton3Pressed()
    {
        Vector3 CrosshairPos = new Vector3(-45f, -60f, 0f);
        tr.position = CrosshairPos;
    }

    public void OnButton2Pressed()
    {
        Vector3 CrosshairPos = new Vector3(-45f, -30f, 0f);
        tr.position = CrosshairPos;
    }

    public void OnButton1Pressed()
    {
        Vector3 CrosshairPos = new Vector3(-45f, -0f, 0f);
        tr.position = CrosshairPos;
    }
}
