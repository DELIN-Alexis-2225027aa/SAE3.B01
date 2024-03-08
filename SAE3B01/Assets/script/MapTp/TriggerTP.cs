using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

/// <summary>
/// Gère le téléporteur du joueur en fonction des déclencheurs et des touches pressées.
/// </summary>
public class TriggerTP : MonoBehaviour
{
    // Instances des gestionnaires et convertisseurs nécessaires
    DBManager dbManager;
    ValluesConvertor valluesConvertor;
    PosSaver posSaver;

    // Coordonnées de téléportation
    public float xTP;
    public float yTP;

    // Nom de l'objet en collision
    public string colName;

    // Collider attaché à cet objet
    [SerializeField] private Collider2D myCollider;

    // Chemin du fichier pour la sauvegarde des positions
    string filePath;

    /// <summary>
    /// Méthode appelée au démarrage.
    /// </summary>
    void Start()
    {
        // Initialisation des gestionnaires et convertisseurs
        posSaver = new PosSaver();
        dbManager = new DBManager();
        valluesConvertor = new ValluesConvertor();

        // Récupération du Collider2D attaché
        myCollider = GetComponent<Collider2D>();
    }

    /// <summary>
    /// Méthode appelée à chaque frame.
    /// </summary>
    void Update()
    {
        // Tableau de colliders pour stocker les collisions
        Collider2D[] colliders = new Collider2D[1];
        // Filtre pour les collisions
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.NoFilter();
        // Nombre de collisions
        int count = Physics2D.OverlapCollider(myCollider, contactFilter, colliders);

        // Si aucune collision, réinitialiser le nom de l'objet en collision
        if (count == 0)
        {
            colName = null;
        }

        // Détermine la destination du téléporteur en fonction du nom de l'objet en collision
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (colName != null)
            {
                // Utilisation d'une instruction switch pour améliorer la lisibilité
                switch (colName)
                {
                    case "004TPTrigger":
                        xTP = -16.1f;
                        yTP = 0f;
                        TPPlayer();
                        break;

                    case "002TPTrigger":
                        xTP = -31.5f;
                        yTP = 0f;
                        TPPlayer();
                        break;

                    case "009TPTrigger":
                        xTP = 19.6f;
                        yTP = 0f;
                        TPPlayer();
                        break;

                    case "010TPTrigger":
                        xTP = 36f;
                        yTP = 0f;
                        TPPlayer();
                        break;

                    case "hallwayTPTrigger":
                        xTP = -34f;
                        yTP = -1f;
                        TPPlayer();
                        break;

                    case "104TPTrigger":
                        xTP = -19.7f;
                        yTP = -100f;
                        TPPlayer();
                        break;

                    case "109TPTrigger":
                        xTP = 3.6f;
                        yTP = -100f;
                        TPPlayer();
                        break;

                    case "110TPTrigger":
                        xTP = 27.8f;
                        yTP = -100f;
                        TPPlayer();
                        break;

                    case "hallway2ndFloorTPTrigger":
                        xTP = 0f;
                        yTP = -100f;
                        TPPlayer();
                        break;

                    case "BDETPTrigger":
                        xTP = -27.2f;
                        yTP = -200f;
                        TPPlayer();
                        break;

                    case "208TPTrigger":
                        xTP = -3.8f;
                        yTP = -200f;
                        TPPlayer();
                        break;

                    case "hallway3rdFloorTPTrigger":
                        xTP = 0f;
                        yTP = -200f;
                        TPPlayer();
                        break;

                    default:
                        // Si le nom de l'objet en collision n'est associé à aucun cas, ne rien faire
                        break;
                }
            }
        }
    }

    /// <summary>
    /// Méthode appelée lorsque le téléporteur entre en collision avec un objet.
    /// </summary>
    /// <param /name=//collision>Collider de l'objet en collision.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Met à jour le nom de l'objet en collision lorsqu'une collision se produit
        colName = collision.gameObject.name;
    }

    /// <summary>
    /// Téléporte le joueur à la position spécifiée et enregistre les données dans le fichier JSON.
    /// </summary>
    void TPPlayer()
    {
        // Enregistre la position du joueur
        posSaver.savePlayerPos(xTP, yTP);
        // Charge la scène "MovingPhase"
        SceneManager.LoadScene("MovingPhase");
    }
}
