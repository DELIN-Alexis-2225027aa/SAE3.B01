using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Text dialogueText;
    public string[] dialogue;
    private int index;
    public GameObject contButton;
    public float wordSpeed;
    private bool isDialogueActive;

    void Start()
    {
        dialogueText.text = "";
    }


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
        if (dialogueText.text == dialogue[index])
        {
            contButton.SetActive(true);
        }
    }

    void StartDialogue()
    {
        if (index < dialogue.Length)
        {
            isDialogueActive = true;
            StartCoroutine(Typing());
        }
    }

    public void zeroText()
    {
        dialogueText.text = "";
        index = 0;
        isDialogueActive = false;
    }

    IEnumerator Typing()
    {
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }

    }

    public void NextLine()
    {

        contButton.SetActive(false);

        if (index < dialogue.Length - 1)
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
}




