using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;

/// <summary>
/// Gère les déclencheurs des salles de classe pour changer de scène.
/// </summary>
public class ClassroomsTriggers : MonoBehaviour
{
    // Collider attaché à cet objet
    [SerializeField] private Collider2D myCollider;

    // Nom de l'objet en collision
    public string colName;

    // Numéro de salle de classe extrait du nom de l'objet en collision
    public string classroomNumber;

    // Instance du gestionnaire de sauvegarde de position
    [SerializeField] private PosSaver posSaver;

    // Instance du gestionnaire de base de données
    private DBManager dbManager;

    /// <summary>
    /// Méthode appelée au démarrage.
    /// </summary>
    void Start()
    {
        // Récupération du Collider2D attaché
        myCollider = GetComponent<Collider2D>();
    }

    /// <summary>
    /// Méthode appelée à chaque frame.
    /// </summary>
    void Update()
    {
        // Vérifie si la touche E est enfoncée et s'il y a une collision
        if (Input.GetKeyDown(KeyCode.E) && colName != null)
        {
            // Vérifie si tous les caractères du numéro de salle de classe sont des chiffres
            if (areAllCharactersDigits())
            {
                // Sauvegarde la position du joueur
                posSaver.SavePlayerPosition();
                // Sauvegarde le numéro de salle de classe dans la base de données
                SaveClassroomOnDB();
                // Téléporte vers la salle de classe
                TPClassroom();
            }
            else
            {
                // Si le numéro de salle de classe n'est pas composé que de chiffres,
                // vérifie s'il s'agit d'une salle spéciale et effectue le téléport et la sauvegarde.
                if (classroomNumber.Equals("Bde") || classroomNumber.Equals("Mak"))
                {
                    // Sauvegarde la position du joueur
                    posSaver.SavePlayerPosition();
                    // Sauvegarde le numéro de salle de classe dans la base de données
                    SaveClassroomOnDB();
                    // Téléporte vers la salle de classe
                    TPClassroom();
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
        // Extrait le numéro de salle de classe du nom de l'objet en collision
        classroomNumber = colName.Substring(0, 3);
    }

    /// <summary>
    /// Méthode appelée lorsque le téléporteur cesse d'être en collision avec un objet.
    /// </summary>
    /// <param /name=//collision>Collider de l'objet en collision.</param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Réinitialise le nom de l'objet en collision et le numéro de salle de classe
        colName = null;
        classroomNumber = null;
    }

    /// <summary>
    /// Téléporte le joueur vers la scène de la salle de classe.
    /// </summary>
    void TPClassroom()
    {
        // Charge la scène "Classroom"
        SceneManager.LoadScene("Classroom");
    }

    /// <summary>
    /// Sauvegarde le numéro de salle de classe dans la base de données.
    /// </summary>
    void SaveClassroomOnDB()
    {
        // Initialise la base de données
        dbManager = new DBManager();
        // Supprime toutes les données de la table "Classroom"
        dbManager.DeleteEverythingFromTable("Classroom");
        // Insère le numéro de salle de classe dans la table "Classroom"
        dbManager.InsertOneValue("Classroom", classroomNumber);
    }

    /// <summary>
    /// Vérifie si tous les caractères de la chaîne sont des chiffres.
    /// </summary>
    /// <returns>True si tous les caractères sont des chiffres, sinon False.</returns>
    bool areAllCharactersDigits()
    {
        // Parcourt tous les caractères de la chaîne
        foreach (char character in classroomNumber)
        {
            // Si le caractère n'est pas un chiffre, retourne false
            if (!char.IsDigit(character))
            {
                return false;
            }
        }
        // Si tous les caractères sont des chiffres, retourne true
        return true;
    }
}
