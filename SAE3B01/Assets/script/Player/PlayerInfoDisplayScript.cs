using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

/// <summary>
/// Gère l'affichage des informations du joueur sur l'interface utilisateur.
/// </summary>
public class PlayerInfoDispayScript : MonoBehaviour
{
    [SerializeField] RectTransform infoUIPos;
    [SerializeField] TMP_InputField textFieldName;

    private DBManager dbManager;

    public Vector3 infoUIShownPos;
    public Vector3 posOutOfUI;
    private string gender;

    /// <summary>
    /// Appelé au démarrage du script.
    /// </summary>
    void Start()
    {
        // Initialise le gestionnaire de base de données et sauvegarde la position de l'interface utilisateur.
        dbManager = new DBManager();
        savePos();
        putWindowOutOfUI();
    }

    /// <summary>
    /// Affiche la fenêtre d'informations sur l'interface utilisateur.
    /// </summary>
    public void showOnUI()
    {
        putWindowInUI();
    }

    /// <summary>
    /// Sauvegarde la position initiale de l'interface utilisateur.
    /// </summary>
    public void savePos()
    {
        infoUIShownPos = infoUIPos.localPosition;
    }

    /// <summary>
    /// Place la fenêtre d'informations en dehors de l'interface utilisateur.
    /// </summary>
    public void putWindowOutOfUI()
    {
        posOutOfUI = new Vector3(1001f, 1000f, 0f);
        infoUIPos.localPosition = posOutOfUI;
    }

    /// <summary>
    /// Replace la fenêtre d'informations à sa position initiale dans l'interface utilisateur.
    /// </summary>
    public void putWindowInUI()
    {
        infoUIPos.localPosition = infoUIShownPos;
    }

    /// <summary>
    /// Appelé lorsqu'un bouton de sélection du genre masculin est pressé.
    /// </summary>
    public void onMaleButtonPressed()
    {
        gender = "M";
        onGenderSelectionButtonPressed();
    }

    /// <summary>
    /// Appelé lorsqu'un bouton de sélection du genre féminin est relâché.
    /// </summary>
    public void onFemaleButtonReleased()
    {
        gender = "F";
        onGenderSelectionButtonPressed();
    }

    /// <summary>
    /// Vérifie si un texte est inséré dans le champ de texte.
    /// </summary>
    /// <returns>True si un texte est inséré, sinon False.</returns>
    public bool isTextInsertInTextField()
    {
        if (textFieldName.text != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Appelé lorsqu'un bouton de sélection du genre est pressé.
    /// Vérifie si un texte est inséré dans le champ de texte, puis enregistre les données du joueur dans la base de données et change de scène.
    /// </summary>
    public void onGenderSelectionButtonPressed()
    {
        if (isTextInsertInTextField())
        {
            string[] data = { textFieldName.text, gender };
            dbManager.Insert("PlayerData", data);
            SceneManager.LoadScene("IntroVideo");
        }
    }
}
