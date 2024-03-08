using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class PlayerInfoDispayScript : MonoBehaviour
{
    // R�f�rence au RectTransform de l'interface utilisateur (UI)
    [SerializeField] RectTransform infoUIPos;

    // R�f�rence au champ de texte TMP_InputField utilis� pour le nom du joueur
    [SerializeField] TMP_InputField textFieldName;

    // R�f�rence au gestionnaire de base de donn�es
    private DBManager dbManager;

    // Position de l'interface utilisateur lorsque celle-ci est affich�e
    public Vector3 infoUIShownPos;

    // Position hors de l'interface utilisateur
    public Vector3 posOutOfUI;

    // Variable pour stocker le genre du joueur
    private string gender;

    // Start is called before the first frame update
    void Start()
    {
        // Initialisation du gestionnaire de base de donn�es
        dbManager = new DBManager();

        // Sauvegarde de la position affich�e de l'interface utilisateur
        savePos();

        // Placement initial de la fen�tre hors de l'interface utilisateur
        putWindowOutOfUI();
    }

    // M�thode appel�e pour afficher la fen�tre dans l'interface utilisateur
    public void showOnUI()
    {
        putWindowInUI();
    }

    // M�thode appel�e pour sauvegarder la position affich�e de l'interface utilisateur
    public void savePos()
    {
        infoUIShownPos = infoUIPos.localPosition;
    }

    // M�thode appel�e pour placer la fen�tre hors de l'interface utilisateur
    public void putWindowOutOfUI()
    {
        posOutOfUI = new Vector3(1001f, 1000f, 0f);
        infoUIPos.localPosition = posOutOfUI;
    }

    // M�thode appel�e pour placer la fen�tre dans l'interface utilisateur
    public void putWindowInUI()
    {
        infoUIPos.localPosition = infoUIShownPos;
    }

    // M�thode appel�e lorsque le bouton "Male" est press�
    public void onMaleButtonPressed()
    {
        gender = "M";
        onGenderSelectionButtonPressed();
    }

    // M�thode appel�e lorsque le bouton "Female" est rel�ch�
    public void onFemaleButtonReleased()
    {
        gender = "F";
        onGenderSelectionButtonPressed();
    }

    // M�thode pour v�rifier si du texte est ins�r� dans le champ de texte
    public bool isTextInsertInTextField()
    {
        return !string.IsNullOrEmpty(textFieldName.text);
    }

    // M�thode appel�e lorsque le bouton de s�lection du genre est press�
    public void onGenderSelectionButtonPressed()
    {
        // V�rifie si du texte est ins�r� dans le champ de texte
        if (isTextInsertInTextField())
        {
            // Cr�e un tableau de donn�es avec le nom du joueur et son genre
            string[] data = { textFieldName.text, gender };

            // Ins�re les donn�es dans la table "PlayerData" de la base de donn�es
            dbManager.Insert("PlayerData", data);

            // Charge la sc�ne "IntroVideo"
            SceneManager.LoadScene("IntroVideo");
        }
    }
}
