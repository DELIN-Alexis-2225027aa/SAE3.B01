using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.IO;

/// <summary>
/// G�re le t�l�porteur du joueur en fonction des d�clencheurs et des touches press�es.
/// </summary>
public class TriggerTP : MonoBehaviour
{
    public float xTP;
    public float yTP;
    public float zTP;
    public string colName;
    [SerializeField] private Collider2D myCollider;

    string filePath;

    /// <summary>
    /// M�thode appel�e au d�marrage.
    /// </summary>
    void Start()
    {
        filePath = Application.dataPath + "/SaveJson/playerData.json";
        myCollider = GetComponent<Collider2D>();
    }

    /// <summary>
    /// M�thode appel�e � chaque frame.
    /// </summary>
    void Update()
    {
        Collider2D[] colliders = new Collider2D[1]; 
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.NoFilter(); 
        int count = Physics2D.OverlapCollider(myCollider, contactFilter, colliders);

        if (count == 0)
        {
            colName = null;
        }

        // D�termine la destination du t�l�porteur en fonction du nom de l'objet en collision
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (colName.Equals("004TPTrigger")) 
            {
                xTP = -16.1f;
                yTP = 0f;
                zTP = 0f;
                TPPlayer();
            }
            if (colName.Equals("002TPTrigger")) 
            {
                xTP = -31.5f;
                yTP = 0f;
                zTP = 0f;
                TPPlayer();
            }
            if (colName.Equals("009TPTrigger"))
            {
                xTP = 19.6f;
                yTP = 0f;
                zTP = 0f;
                TPPlayer();
            }
            if (colName.Equals("010TPTrigger")) 
            {
                xTP = 36f;
                yTP = 0f;
                zTP = 0f;
                TPPlayer();
            }
            if (colName.Equals("hallwayTPTrigger")) 
            {
                xTP = -34f;
                yTP = -1f;
                zTP = 0f;
                TPPlayer();
            }
            if (colName.Equals("104TPTrigger")) 
            {
                xTP = -19.7f;
                yTP = -100f;
                zTP = 0f;
                TPPlayer();
            }
            if (colName.Equals("109TPTrigger")) 
            {
                xTP = 3.6f;
                yTP = -100f;
                zTP = 0f;
                TPPlayer();
            }
            if (colName.Equals("110TPTrigger")) 
            {
                xTP = 27.8f;
                yTP = -100f;
                zTP = 0f;
                TPPlayer();
            }
            if (colName.Equals("hallway2ndFloorTPTrigger")) 
            {
                xTP = 0f;
                yTP = -100f;
                zTP = 0f;
                TPPlayer();
            }
            if (colName.Equals("BDETPTrigger"))
            {
                xTP = -27.2f;
                yTP = -200f;
                zTP = 0f;
                TPPlayer();
            }
            if (colName.Equals("208TPTrigger"))
            {
                xTP = -3.8f;
                yTP = -200f;
                zTP = 0f;
                TPPlayer();
            }
            if (colName.Equals("hallway3rdFloorTPTrigger"))
            {
                xTP = 0f;
                yTP = -200f;
                zTP = 0f;
                TPPlayer();
            }
        }
    }

    /// <summary>
    /// M�thode appel�e lorsque le t�l�porteur entre en collision avec un objet.
    /// </summary>
    /// <param /name=//collision>Collider de l'objet en collision.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        colName = collision.gameObject.name;
    }

    /// <summary>
    /// T�l�porte le joueur � la position sp�cifi�e et enregistre les donn�es dans le fichier JSON.
    /// </summary>
    void TPPlayer()
    {
        PlayerData playerData = new PlayerData
        {
            x = xTP,
            y = yTP,
            z = zTP
        };

        // Convertir la classe en JSON et �crire dans le fichier
        string updatedJson = JsonUtility.ToJson(playerData);
        File.WriteAllText(filePath, updatedJson);

        SceneManager.LoadScene("MovingPhase");
    }
}
