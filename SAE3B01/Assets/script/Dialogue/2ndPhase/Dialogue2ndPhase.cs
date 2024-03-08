using System.Collections;
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

public class Dialogue2ndPhase : MonoBehaviour
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
    [SerializeField]public string tableIdToRead;
    [SerializeField]public string tableNameToRead; 

    Vector3 outPos;
    Vector3 inPos;
    [SerializeField] ClassroomSpriteSetter classroomSpriteSetter;

    /// Méthode appelée au démarrage.
    /// </summary>
    void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
        classroomSpriteSetter = new ClassroomSpriteSetter();
        valluesConvertor = new ValluesConvertor();
        dbManager = new DBManager();

        isTextInitialized = false;
        isDialogueLoaded = false;
        dialogueText.text = "";

        getDialoguendPhaseID();
        getDialoguendPhaseTableName();
        getNextDialogueToRead(tableNameToRead+tableIdToRead);

        getDialogueInfoByID(dbManager, tableNameToRead, tableIdToRead);

        outPos = new Vector3(1000f, 1000f, 0f);
        inPos = new Vector3(0f, 0f, 0f);

        loadDialogue();
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
            getDialogueInfoByID(dbManager, tableNameToRead, tableIdToRead);
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


    public void getDialogueInfoByID(DBManager dbManager, string table, string ID)
    {
        getDialogueByID(table, ID);
        getNameByID(table, ID);
        getPosIDsByID(table, ID);
        dialogueName.text = nameSprite[index];
        isDialogueLoaded = true;
    }

    public void getDialoguendPhaseID()
    {
        
        List<List<object>> resultat = dbManager.Select("ndPhaseDialogueSelector", "iD", "1");

        foreach (List<object> row in resultat)
        {
            tableIdToRead = valluesConvertor.convertRowToString(row);
        }
    }

    public void getDialoguendPhaseTableName()
    {
        
        List<List<object>> resultat = dbManager.Select("ndPhaseDialogueSelector", "tableName", "1");

        foreach (List<object> row in resultat)
        {
            tableNameToRead = valluesConvertor.convertRowToString(row);
        }
    }

    public void getDialogueByID(string table, string ID)
    {
        
        List<List<object>> resultat = dbManager.Select(table, "dialogue", "ID = " + ID);

        foreach (List<object> row in resultat)
        {
            dialogueToShow = valluesConvertor.ConvertRowToStringArray(row);
        }
    }
    
    public void getNameByID(string table, string ID)
    {
        List<List<object>> resultat = dbManager.Select(table, "name", "ID = " + ID);

        foreach (List<object> row in resultat)
        {
            nameSprite = valluesConvertor.ConvertRowToStringArray(row);
        }
    }

    public void getPosIDsByID(string table, string ID)
    {
        List<List<object>> resultat = dbManager.Select(table, "posID", "ID = " + ID);

        foreach (List<object> row in resultat)
        {
            string str = valluesConvertor.convertRowToString(row);
            sprites = valluesConvertor.convertDBstringToIntArray(str);
        }
    }

    void getNextDialogueToRead(string fullName)
    {
        
        switch(fullName)
        {
            case "5553366655533666":
                tableIdToRead = "1";
                tableNameToRead = "Questions";
                break;
            case "Questions1":
                tableIdToRead = "1";
                tableNameToRead = "Answers";
                break;
            case "Answers1":
                tableIdToRead = "2";
                tableNameToRead = "Questions";
                break;
            case "Questions2":
                tableIdToRead = "2";
                tableNameToRead = "Answers";
                break;
            case "Answers2":
                tableIdToRead = "3";
                tableNameToRead = "Questions";
                break;
            case "Questions3":
                tableIdToRead = "3";
                tableNameToRead = "Answers";
                break;
            case "Answers3":
                tableIdToRead = "4";
                tableNameToRead = "Questions";
                break;
            case "Questions4":
                tableIdToRead = "4";
                tableNameToRead = "Answers";
                break;
            case "Answers4":
                tableIdToRead = "5";
                tableNameToRead = "Questions";
                break;
            case "Questions5":
                tableIdToRead = "5";
                tableNameToRead = "Answers";
                break;
            case "Answers5":
                tableIdToRead = "6";
                tableNameToRead = "Questions";
                break;
            case "Questions6":
                tableIdToRead = "6";
                tableNameToRead = "Answers";
                break;
            case "Answers6":
                SceneManager.LoadScene("MainMenu");
                break;
        }
    }
}
