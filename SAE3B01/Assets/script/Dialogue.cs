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
    [SerializeField] Transform isDialogueFinished;

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
    public string[] nameSprite;
    [SerializeField] GameObject isIntroObject;
    bool isTextInitialized;
    string spriteName;

    public int id;

    public bool isDialogueLoaded;

    Vector3 outPos;
    Vector3 inPos;

    [SerializeField] ClassroomSpriteSetter classroomSpriteSetter;

    /// Méthode appelée au démarrage.
    /// </summary>
    void Start()
    {
        classroomSpriteSetter = new ClassroomSpriteSetter();
        valluesConvertor = new ValluesConvertor();
        dbManager = new DBManager();


        isTextInitialized = false;
        isDialogueLoaded = false;
        dialogueText.text = "";

        spriteName = classroomSpriteSetter.getClassroomName(dbManager, valluesConvertor);
        if (spriteName != null)
        {
            id = getIdByClassroomNumber(spriteName);
        }
        else id = 1;
        
        getDialogueInfoByID(id);

        if (isIntro() == true)
        {
            loadDialogue();
        }

        outPos = new Vector3(1000f, 1000f, 0f);;
        inPos = new Vector3(0f, 0f, 0f);;
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
            isDialogueFinished.localPosition = inPos;
            classroomSpriteSetter.removeDialogueObjectFromUI();
        }
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
        isDialogueFinished.localPosition = outPos;
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
            Debug.Log("1");
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


    public void getDialogueInfoByID(int id)
    {
        string strID = id.ToString();
        getDialogueByID(strID);
        getNameByID(strID);
        getPosIDsByID(strID);
        dialogueName.text = mmeOrM() + nameSprite[index];
        isDialogueLoaded = true;
    }

    public void getDialogueByID(string ID)
    {
        
        List<List<object>> resultat = dbManager.Select("Dialogues", "dialogue", "ID = " + ID);

        foreach (List<object> row in resultat)
        {
            dialogueToShow = valluesConvertor.ConvertrowToStringArray(row);
            Debug.Log(dialogueToShow);
        }
    }

    public void getNameByID(string ID)
    {
        List<List<object>> resultat = dbManager.Select("Dialogues", "name", "ID = " + ID);

        foreach (List<object> row in resultat)
        {
            //string str = valluesConvertor.convertRowToString(row);
            nameSprite = valluesConvertor.ConvertrowToStringArray(row);
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


    bool isIntro()
    {
        if (isIntroObject != null)
        {
            return true;
        }
        return false;
    }

    public string mmeOrM()
    {
        if (nameSprite[index].Equals("MAKSSOUD"))
        {
            return "Mme ";
        }
        else return "M. ";
    
    }
    public int getIdByClassroomNumber(string numb)
    {
        switch(numb)
        {
            case "BDE":
                break;
            case "MAK":
                id = 2;
                break;
            case "002":
                id = 3;
                break;

        }
            return id;
    }
}
