using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

/// <summary>
/// G�re l'affichage et la gestion des dialogues dans le jeu.
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
/// Repr�sente la s�lection d'un ensemble de dialogues par son r�pertoire.
/// </summary>
public class DialogueSelector
{
    public string repertory;
}

public class Dialogue : MonoBehaviour
{
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
    public List<int> sprites;
    public string nameSprite;


    /// <summary>
    /// M�thode appel�e au d�marrage.
    /// </summary>
    void Start()
    {
        dialogueText.text = "";
        filePath = Application.dataPath + "/SaveJson/dialogueManager.json";
        GetWichDialogue();
        StartDialogue();
        dialogueName.text = nameSprite;
    }

    /// <summary>
    /// M�thode appel�e � chaque frame fixe.
    /// </summary>
    void FixedUpdate()
    {
        wordSpeed = 0.05f;
    }

    /// <summary>
    /// M�thode appel�e � chaque frame.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isDialogueActive)
            {
                NextLine();
            }
            else
            {
                StartDialogue();
            }
        }
        if (index < dialogueToShow.Length && dialogueText.text == dialogueToShow[index])
        {
            contButton.SetActive(true);
        }
    }

    /// <summary>
    /// D�marre l'affichage du dialogue.
    /// </summary>
    void StartDialogue()
    {
        contButton.SetActive(false);
        if (dialogueToShow.Length > 0 && index < dialogueToShow.Length)
        {
            isDialogueActive = true;
            StartCoroutine(Typing());
        }
    }

    /// <summary>
    /// R�initialise le texte du dialogue.
    /// </summary>
    public void zeroText()
    {
        dialogueText.text = "";
        index = 0;
        isDialogueActive = false;
        SceneManager.LoadScene("MovingPhase");
    }

    /// <summary>
    /// Effectue l'effet de dactylographie pour afficher le dialogue lettre par lettre.
    /// </summary>
    /// <returns>Coroutine.</returns>
    IEnumerator Typing()
    {
        changImg(nameSprite, sprites[index]);
        foreach (char letter in dialogueToShow[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }

    }

    /// <summary>
    /// Passe � la ligne suivante du dialogue.
    /// </summary>
    public void NextLine()
    {

        contButton.SetActive(false);

        if (index < dialogueToShow.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            zeroText();
        }

    }

    /// <summary>
    /// R�cup�re les dialogues � partir du fichier JSON.
    /// </summary>
    public void GetDialogueByFileName()
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

    /// <summary>
    /// R�cup�re le r�pertoire de dialogues � partir du fichier JSON et charge les dialogues associ�s.
    /// </summary>
    public void GetWichDialogue()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            DialogueSelector dialoguesSelector = JsonUtility.FromJson<DialogueSelector>(json);
            if (dialoguesSelector != null)
            {
                filePath = Path.Combine(Application.dataPath, "SaveJson", dialoguesSelector.repertory+".json");
                GetDialogueByFileName();
            }
        }
    }

    public void changImg(string name, int poseID)
    {
        string spriteName = $"{name}{poseID}.png";
        string imagePath = Path.Combine(Application.dataPath, "Images", spriteName);
        Debug.LogError(imagePath);
        if (File.Exists(imagePath))
        {
            byte[] fileData = File.ReadAllBytes(imagePath);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(fileData);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            img.sprite = sprite;
        }
    }


}




