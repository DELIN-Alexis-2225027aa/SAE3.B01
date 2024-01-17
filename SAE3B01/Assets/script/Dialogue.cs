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
    public GameObject dialoguePanel;
    public Text dialogueText;
    public string[] dialogueToShow;
    private int index;
    public GameObject contButton;
    public float wordSpeed;
    private bool isDialogueActive;
    string json;
    string filePath;


    /// <summary>
    /// M�thode appel�e au d�marrage.
    /// </summary>
    void Start()
    {
        dialogueText.text = "";
        filePath = Application.dataPath + "/SaveJson/dialogueManager.json";
        GetWichDialogue();
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

}




