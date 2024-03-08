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
    [SerializeField] private Collider2D myCollider;
    public string colName;
    public string classroomNumber;
    [SerializeField] private PosSaver posSaver;

    private DBManager dbManager;

    /// <summary>
    /// Appelé au démarrage de l'objet.
    /// </summary>
    void Start()
    {
        myCollider = GetComponent<Collider2D>();
    }

    /// <summary>
    /// Méthode appelée à chaque frame.
    /// </summary>
    void Update()
    {
        // Vérifie si la touche E est enfoncée et qu'un nom de collision est défini.
        if (Input.GetKeyDown(KeyCode.E) && colName != null)
        {
            // Vérifie si tous les caractères de la salle sont des chiffres.
            if (areAllCharactersDigits())
            {
                // Enregistre la position du joueur, sauvegarde la salle dans la base de données, et change de scène.
                posSaver.SavePlayerPosition();
                SaveClassroomOnDB();
                TPClassroom();
            }
            else
            {
                // Si le nom de la salle est "Bde" ou "Mak", enregistre la position du joueur, sauvegarde la salle dans la base de données, et change de scène.
                if (classroomNumber.Equals("Bde") || classroomNumber.Equals("Mak"))
                {
                    posSaver.SavePlayerPosition();
                    SaveClassroomOnDB();
                    TPClassroom();
                }
            }
        }
    }

    /// <summary>
    /// Appelé lorsque le joueur entre dans le déclencheur de la salle de classe.
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        colName = collision.gameObject.name;
        classroomNumber = colName.Substring(0, 3);
    }

    /// <summary>
    /// Appelé lorsque le joueur quitte le déclencheur de la salle de classe.
    /// </summary>
    private void OnTriggerExit2D(Collider2D collision)
    {
        colName = null;
        classroomNumber = null;
    }

    /// <summary>
    /// Change de scène vers "Classroom".
    /// </summary>
    void TPClassroom()
    {
        SceneManager.LoadScene("Classroom");
    }

    /// <summary>
    /// Enregistre la salle de classe actuelle dans la base de données.
    /// </summary>
    void SaveClassroomOnDB()
    {
        dbManager = new DBManager();
        dbManager.DeleteEverythingFromTable("Classroom");
        dbManager.InsertOneValue("Classroom", classroomNumber);
    }

    /// <summary>
    /// Vérifie si tous les caractères de la salle de classe sont des chiffres.
    /// </summary>
    /// <returns>True si tous les caractères sont des chiffres, sinon False.</returns>
    bool areAllCharactersDigits()
    {
        foreach (char character in classroomNumber)
        {
            if (!char.IsDigit(character))
            {
                // Si le caractère n'est pas un chiffre, retourne false.
                return false;
            }
        }
        // Si tous les caractères sont des chiffres, retourne true.
        return true;
    }
}
