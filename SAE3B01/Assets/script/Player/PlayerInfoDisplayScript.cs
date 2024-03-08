using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class PlayerInfoDispayScript : MonoBehaviour
{
    // Référence au RectTransform de l'interface utilisateur (UI)
    [SerializeField] RectTransform infoUIPos;

    // Référence au champ de texte TMP_InputField utilisé pour le nom du joueur
    [SerializeField] TMP_InputField textFieldName;

    // Référence au gestionnaire de base de données
    private DBManager dbManager;

    // Position de l'interface utilisateur lorsque celle-ci est affichée
    public Vector3 infoUIShownPos;

    // Position hors de l'interface utilisateur
    public Vector3 posOutOfUI;

    // Variable pour stocker le genre du joueur
    private string gender;

    // Start is called before the first frame update
    void Start()
    {
        // Initialisation du gestionnaire de base de données
        dbManager = new DBManager();

        // Sauvegarde de la position affichée de l'interface utilisateur
        savePos();

        // Placement initial de la fenêtre hors de l'interface utilisateur
        putWindowOutOfUI();
    }

    // Méthode appelée pour afficher la fenêtre dans l'interface utilisateur
    public void showOnUI()
    {
        putWindowInUI();
    }

    // Méthode appelée pour sauvegarder la position affichée de l'interface utilisateur
    public void savePos()
    {
        infoUIShownPos = infoUIPos.localPosition;
    }

    // Méthode appelée pour placer la fenêtre hors de l'interface utilisateur
    public void putWindowOutOfUI()
    {
        posOutOfUI = new Vector3(1001f, 1000f, 0f);
        infoUIPos.localPosition = posOutOfUI;
    }

    // Méthode appelée pour placer la fenêtre dans l'interface utilisateur
    public void putWindowInUI()
    {
        infoUIPos.localPosition = infoUIShownPos;
    }

    // Méthode appelée lorsque le bouton "Male" est pressé
    public void onMaleButtonPressed()
    {
        gender = "M";
        onGenderSelectionButtonPressed();
    }

    // Méthode appelée lorsque le bouton "Female" est relâché
    public void onFemaleButtonReleased()
    {
        gender = "F";
        onGenderSelectionButtonPressed();
    }

    // Méthode pour vérifier si du texte est inséré dans le champ de texte
    public bool isTextInsertInTextField()
    {
        return !string.IsNullOrEmpty(textFieldName.text);
    }

    // Méthode appelée lorsque le bouton de sélection du genre est pressé
    public void onGenderSelectionButtonPressed()
    {
        // Vérifie si du texte est inséré dans le champ de texte
        if (isTextInsertInTextField())
        {
            // Crée un tableau de données avec le nom du joueur et son genre
            string[] data = { textFieldName.text, gender };

            // Insère les données dans la table "PlayerData" de la base de données
            dbManager.Insert("PlayerData", data);

            // Charge la scène "IntroVideo"
            SceneManager.LoadScene("IntroVideo");
        }
    }
}
