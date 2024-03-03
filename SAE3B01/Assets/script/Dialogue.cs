using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;


/// Gère l'affichage et la gestion des dialogues dans le jeu.
/// </summary>
[System.Serializable]
public class DialogueBD
{
    public int ID;
    public string name;
    public List<int> poseID;
    public string[] dialogue;
}


/// <summary>
/// Représente la sélection d'un ensemble de dialogues par son répertoire.
/// </summary>
public class DialogueSelector
{
    public string repertory;
}

public class Dialogue : MonoBehaviour
{
    private ValluesConvertor valluesConvertor;
    private DBManager dbManager;

    [SerializeField] Image img;
    public GameObject dialoguePanel;
    public Text dialogueText;
    public Text dialogueName;
    public string[] dialogueToShow;
    private int index;
    public GameObject contButton;
    public float wordSpeed;
    private bool isDialogueActive;
    string json;
    string filePath;
    public int[] sprites;
    public string nameSprite;
    [SerializeField] GameObject isIntroObject;
    bool isTextInitialized;

    public int id;

    public bool isDialogueLoaded;

    [SerializeField] ClassroomSpriteSetter classroomSpriteSetter;


    /// Méthode appelée au démarrage.
    /// </summary>
    void Start()
    {
        valluesConvertor = new ValluesConvertor();
        dbManager = new DBManager();


        isTextInitialized = false;
        isDialogueLoaded = false;
        dialogueText.text = "";

        id = 1;
        getDialogueInfoByID(id);

        if (isIntro() == true)
        {
            loadDialogue();
        }
    }

    /// Méthode appelée à chaque frame fixe.
    /// </summary>
    void FixedUpdate()
    {
        wordSpeed = 0.05f;
    }

    /// Méthode appelée à chaque frame.
    /// </summary>
    void Update()
    {
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
    }




    /// </summary>
    void startDialogue()
    {
        contButton.SetActive(false);
        if (dialogueToShow.Length > 0 && index < dialogueToShow.Length)
        {
            contButton.SetActive(false);
            isTextInitialized = true;
            isDialogueActive = true;
            StartCoroutine(typing());
        }
    }


    /// Réinitialise le texte du dialogue.
    /// </summary>
    public void zeroText()
    {
        dialogueText.text = "";
        index = 0;
        isDialogueActive = false;
        if (isIntro() == true)
        {
            SceneManager.LoadScene("MovingPhase");
        }
        else
        {
            classroomSpriteSetter.removeDialogueObjectFromUI();
        }
    }

    /// Effectue l'effet de dactylographie pour afficher le dialogue lettre par lettre.
    /// </summary>
    /// <returns>Coroutine.</returns>
    IEnumerator typing()
    {
        changImg(nameSprite, sprites[index]);
        foreach (char letter in dialogueToShow[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
        isDialogueActive = false;

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
            /*if (getWichDialogue() != false)
            {
                getWichDialogue();
                startDialogue();
                dialogueName.text = mmeOrMr() + nameSprite;
            }*/
        }
    }

    /// <summary>
    /// Passe à la ligne suivante du dialogue.
    public void nextLine()
    {
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
    /*
    /// <summary>
    /// Récupère les dialogues à partir du fichier JSON.

    public void getDialogueByFileName()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            DialogueBD dialogues = JsonUtility.FromJson<DialogueBD>(json);
            dialogueToShow = dialogues.dialogue;
            sprites = dialogues.poseID;
            nameSprite = dialogues.name;
        }
    }

     <summary>
    /// Récupère le répertoire de dialogues à partir du fichier JSON et charge les dialogues associée.
    public bool getWichDialogue()
    {
        bool returednBool = false;
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            DialogueSelector dialoguesSelector = JsonUtility.FromJson<DialogueSelector>(json);
            if (dialoguesSelector != null)
            {
                filePath = Path.Combine(Application.dataPath, "SaveJson", dialoguesSelector.repertory + ".json");
                getDialogueByFileName();
                returednBool = true;
                isDialogueLoaded = true;
                resetClassroom();
            }
        }
        return returednBool;
    }
    
    */
    public void changImg(string name, int poseID)
    {
        string spriteName = $"{name}{poseID}.png";
        string imagePath = Path.Combine(Application.dataPath, "Images", spriteName);
        if (File.Exists(imagePath))
        {
            byte[] fileData = File.ReadAllBytes(imagePath);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(fileData);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            img.sprite = sprite;
        }
    }
    /*
    void resetClassroom()
    {
        filePath = Path.Combine(Application.dataPath, "SaveJson/classroom.json");
        Classroom classroom = new Classroom
        {
            classroomName = null
        };

        // Convertir la classe en JSON et écrire dans le fichier
        string updatedJson = JsonUtility.ToJson(classroom);
        File.WriteAllText(filePath, updatedJson);

    }*/


    public void getDialogueInfoByID(int id)
    {
        getDialogueByID(id);
        getNameByID(id);
        getPosIDsByID(id);
        dialogueName.text = mmeOrMr() + nameSprite;
        isDialogueLoaded = true;
    }

    public void getDialogueByID(int ID)
    {
        List<List<object>> resultat = dbManager.Select("Dialogues", "dialogue", "ID = " + ID);

        foreach (List<object> row in resultat)
        {
            dialogueToShow = valluesConvertor.ConvertrowToStringArray(row);
            Debug.LogError(dialogueToShow);
        }
    }

    public void getNameByID(int ID)
    {
        List<List<object>> resultat = dbManager.Select("Dialogues", "name", "ID = " + ID);

        foreach (List<object> row in resultat)
        {
            //string str = valluesConvertor.convertRowToString(row);
            nameSprite = valluesConvertor.convertRowToString(row);
        }
    }

    public void getPosIDsByID(int ID)
    {
        List<List<object>> resultat = dbManager.Select("Dialogues", "posID", "ID = " + ID);

        foreach (List<object> row in resultat)
        {
            string str = valluesConvertor.convertRowToString(row);
            sprites = valluesConvertor.convertDBstringToIntArray(str);
        }
    }


    bool isIntro()
    {
        if (isIntroObject != null)
        {
            return true;
        }
        return false;
    }

    public string mmeOrMr()
    {
        if (nameSprite.Equals("MAKSSOUD"))
        {
            return "Mme ";
        }
        else return "Mr ";
    }


    public void getIdByClassroomNumber(string numb)
    {
        int id;
        switch(numb)
        {
            case "BDE":
                break;
            case "MAK":
                break;
            case "001":
                id = 2;
                break;

        }
    }
}
