﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;



/// <summary>
/// Représente la sélection d'un ensemble de dialogues par son répertoire.
/// </summary>
public class DialogueSelector
{
    public string repertory;
}

public class Dialogue : MonoBehaviour
{
    private InventoryManager inventoryManager;
    private TestSQLite testSQLite;
    private ValluesConvertor valluesConvertor;
    private DBManager dbManager;
    [SerializeField] Transform isDialogueFinished;

    [SerializeField] Image img;
    public GameObject dialoguePanel;
    public Text dialogueText;
    public Text dialogueName;
    public string[] dialogueToShow = {"."};
    private int index;
    public GameObject contButton;
    public float wordSpeed;
    private bool isDialogueActive;
    string json;
    string filePath;
    public int[] sprites;
    public string[] nameSprite;
    public string isFirstTime;
    [SerializeField] GameObject isIntroObject;
    public bool isTextInitialized;
    public string spriteName;
    public int proofID;
    public bool isOneDialogueAsBeenFinished;
    public bool newDialogueCheck;
    public int id;
    public int dialogueInt;
    public string isProofCollected;
    public bool isDialogueLoaded;
    Vector3 outPos;
    Vector3 inPos;
    [SerializeField] ClassroomSpriteSetter classroomSpriteSetter;

    /// Méthode appelée au démarrage.
    /// </summary>
    void Start()
{
    isOneDialogueAsBeenFinished = false;
    newDialogueCheck = false;
    inventoryManager = FindObjectOfType<InventoryManager>();
    classroomSpriteSetter = new ClassroomSpriteSetter();
    valluesConvertor = new ValluesConvertor();
    dbManager = new DBManager();

    isTextInitialized = false;
    isDialogueLoaded = false;
    dialogueText.text = "";

    spriteName = classroomSpriteSetter.getClassroomName(dbManager, valluesConvertor);

    getDialogueInfoByID(dbManager, id);

    outPos = new Vector3(1000f, 1000f, 0f);
    inPos = new Vector3(0f, 0f, 0f);
}
    /// Méthode appelée à chaque frame fixe.
    /// </summary>
    void FixedUpdate()
    {
        wordSpeed = 0.02f;
    }
    /// Méthode appelée à chaque frame.
    /// </summary>
    void Update()
    {
        wordSpeed = Mathf.Clamp(wordSpeed, 0.01f, 0.1f);
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isDialogueActive)
            {
                nextLine();
            }
        }
        if (index < dialogueToShow.Length && dialogueText.text == dialogueToShow[index])
        {
            contButton.SetActive(true);
        }
        if(newDialogueCheck){
            getDialogueInfoByID(dbManager, id);
        }
    }

    /// Réinitialise le texte du dialogue.
    /// </summary>
    public void zeroText()
    {

        dialogueText.text = "";
        index = 0;
        isDialogueActive = false;
        isTextInitialized = false;
        SceneManager.LoadScene("MovingPhase");
    }

    /// Effectue l'effet de dactylographie pour afficher le dialogue lettre par lettre.
    /// </summary>
    /// <returns>Coroutine.</returns>
    IEnumerator typing()
    {
        changImg(nameSprite[index], sprites[index]);
        foreach (char letter in dialogueToShow[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
        isDialogueActive = false;
        newDialogueCheck = true;
    }

    public void loadDialogue()
    {
        if (isDialogueLoaded)
        {
            if (!isTextInitialized)
            {
                startDialogue();
            }
            else
            {
                if (!isDialogueActive)
                {
                    nextLine();
                }
            }
        }
        else
        {

        }
    }

    /// </summary>
    void startDialogue()
    {
        contButton.SetActive(false);
        if (dialogueToShow.Length > 0 && index < dialogueToShow.Length)
        {
            isTextInitialized = true;
            isDialogueActive = true;
            StartCoroutine(typing());
        }
    }

    /// <summary>
    /// Passe à la ligne suivante du dialogue.
    public void nextLine()
    {
        if (isDialogueFinished != null)
        {
            isDialogueFinished.localPosition = outPos;
        }
        isDialogueActive = true;
        contButton.SetActive(false);

        if (index < dialogueToShow.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(typing());
        }
        else
        {
            zeroText();
        }
    }

    public void changImg(string name, int poseID)
    {
        string spriteName = $"{name}{poseID}.png";
        string imagePath = Path.Combine(Application.dataPath, "Images/Personnage", spriteName);
        if (File.Exists(imagePath))
        {
            byte[] fileData = File.ReadAllBytes(imagePath);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(fileData);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            img.sprite = sprite;
        }
    }


    public void getDialogueInfoByID(DBManager dbManager, int id)
    {
        int checkId = id;
        string strID = checkId.ToString();
            getDialogueByID(strID);
            getNameByID(strID);
            getPosIDsByID(strID);
            dialogueName.text = nameSprite[index];
            isDialogueLoaded = true;
    }

    public void getDialogueByID(string ID)
    {
        
        List<List<object>> resultat = dbManager.Select("Dialogues", "dialogue", "ID = " + ID);

        foreach (List<object> row in resultat)
        {
            dialogueToShow = valluesConvertor.ConvertRowToStringArray(row);
        }
    }
    
    public void getNameByID(string ID)
    {
        List<List<object>> resultat = dbManager.Select("Dialogues", "name", "ID = " + ID);

        foreach (List<object> row in resultat)
        {
            nameSprite = valluesConvertor.ConvertRowToStringArray(row);
        }
    }

    public void getIsFirstTimeByID(string ID)
    {
        List<List<object>> resultat = dbManager.Select("Dialogues", "isFirstTime", "ID = " + ID);

        foreach (List<object> row in resultat)
        {
            isFirstTime = valluesConvertor.convertRowToString(row);
        }
    }

    public void getPosIDsByID(string ID)
    {
        List<List<object>> resultat = dbManager.Select("Dialogues", "posID", "ID = " + ID);

        foreach (List<object> row in resultat)
        {
            string str = valluesConvertor.convertRowToString(row);
            sprites = valluesConvertor.convertDBstringToIntArray(str);
        }
    }


}
