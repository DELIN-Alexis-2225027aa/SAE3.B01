using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

/// <summary>
/// G�re l'affichage des informations du joueur sur l'interface utilisateur.
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
    /// Appel� au d�marrage du script.
    /// </summary>
    void Start()
    {
        // Initialise le gestionnaire de base de donn�es et sauvegarde la position de l'interface utilisateur.
        dbManager = new DBManager();
        savePos();
        putWindowOutOfUI();
    }

    /// <summary>
    /// Affiche la fen�tre d'informations sur l'interface utilisateur.
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
    /// Place la fen�tre d'informations en dehors de l'interface utilisateur.
    /// </summary>
    public void putWindowOutOfUI()
    {
        posOutOfUI = new Vector3(1001f, 1000f, 0f);
        infoUIPos.localPosition = posOutOfUI;
    }

    /// <summary>
    /// Replace la fen�tre d'informations � sa position initiale dans l'interface utilisateur.
    /// </summary>
    public void putWindowInUI()
    {
        infoUIPos.localPosition = infoUIShownPos;
    }

    /// <summary>
    /// Appel� lorsqu'un bouton de s�lection du genre masculin est press�.
    /// </summary>
    public void onMaleButtonPressed()
    {
        gender = "M";
        onGenderSelectionButtonPressed();
    }

    /// <summary>
    /// Appel� lorsqu'un bouton de s�lection du genre f�minin est rel�ch�.
    /// </summary>
    public void onFemaleButtonReleased()
    {
        gender = "F";
        onGenderSelectionButtonPressed();
    }

    /// <summary>
    /// V�rifie si un texte est ins�r� dans le champ de texte.
    /// </summary>
    /// <returns>True si un texte est ins�r�, sinon False.</returns>
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
    /// Appel� lorsqu'un bouton de s�lection du genre est press�.
    /// V�rifie si un texte est ins�r� dans le champ de texte, puis enregistre les donn�es du joueur dans la base de donn�es et change de sc�ne.
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
