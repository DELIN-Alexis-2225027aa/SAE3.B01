using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;


public class ClassroomSpriteSetter : MonoBehaviour
{

    [SerializeField] Image background;
    [SerializeField] Button interactiveObject;
    [SerializeField] RectTransform dialogueButtonRect;
    [SerializeField] RectTransform nameDialogueRect;
    [SerializeField] RectTransform dialogueRect;
    [SerializeField] RectTransform dialogueBGRect;
    [SerializeField] RectTransform caracterSpriteRect;
    [SerializeField] RectTransform returnButtonRect;

    string json;
    string filePath;
    [SerializeField] RectTransform interactiveObjectPos;
    private string classroomNumber;
    private string spriteName;
    string imagePath;
    int intValueOfClassroom;


    Vector3 dialogueButtonRectPos;
    Vector3 nameDialogueRectPos;
    Vector3 dialogueRectPos;
    Vector3 dialogueBGRectPos;
    Vector3 caracterSpriteRectPos;
    Vector3 returnButtonRectPos;
    Vector3 posOutOfUI;

    // Start is called before the first frame update
    void Start()
    {
        saveDialogueObjectPos();

        filePath = Application.dataPath + "/SaveJson/classroom.json";
        LoadClassroomSprites();
        SetupInteractibleObject();
        loadInteractiveObjectSprite();
        filePath = Application.dataPath + "/SaveJson/dialogueManager.json";
        removeDialogueObjectFromUI();
    }

    void LoadClassroomSprites()
    {
        if (File.Exists(filePath))
        {
            // Lire le JSON depuis le fichier
            string json = File.ReadAllText(filePath);
            Classroom classroomNumber = JsonUtility.FromJson<Classroom>(json);
            spriteName = $"{classroomNumber.classroomName}.png";
            intValueOfClassroom = int.Parse(classroomNumber.classroomName);
            imagePath = Path.Combine(Application.dataPath, "Images", spriteName);
            byte[] fileData = File.ReadAllBytes(imagePath);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(fileData);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            background.sprite = sprite;
        }
        else
        {
            Debug.Log("No saved player position found.");
        }
    }


    void SetupInteractibleObject()
    {
        float x = 0f;
        float y = 0f;
        float width = 0f;
        float height = 0f;
        switch (intValueOfClassroom)
        {
            case 000:
                x = 1000f;
                y = 1000f;
                    break;
            case 002:
                x = Screen.width / 3;
                y = -Screen.height / 5;
                width = 320f;
                height = 400f;
                break;
        }

        Vector3 newPos = interactiveObjectPos.localPosition;
        newPos.x = x;
        newPos.y = y;
        interactiveObjectPos.localPosition = newPos;
        Vector2 newSize = new Vector2(width, height);
        interactiveObjectPos.sizeDelta = newSize;
    }


    void loadInteractiveObjectSprite()
    {
        string interactiveObjectSprite = null;
        switch (intValueOfClassroom)
        {
            case 002:
                interactiveObjectSprite = "MAKSSOUD1";
                break;
        }
        if (interactiveObjectSprite != null)
        {
            spriteName = $"{interactiveObjectSprite}.png";
            imagePath = Path.Combine(Application.dataPath, "Images", spriteName);
            byte[] fileData = File.ReadAllBytes(imagePath);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(fileData);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            interactiveObject.image.sprite = sprite;
        }
        else
        {
            Vector3 newPos = interactiveObjectPos.localPosition;
            newPos.y = 1000f;
            interactiveObjectPos.localPosition = newPos;
        }
        
    }

    void saveDialogueObjectPos()
    {
        dialogueButtonRectPos = dialogueButtonRect.localPosition;
        nameDialogueRectPos = nameDialogueRect.localPosition;
        dialogueRectPos = dialogueRect.localPosition;
        dialogueBGRectPos = dialogueBGRect.localPosition;
        caracterSpriteRectPos = caracterSpriteRect.localPosition;
        returnButtonRectPos = returnButtonRect.localPosition;
    }

    public void removeDialogueObjectFromUI()
    {
        posOutOfUI = new Vector3(1001f, 1000f, 0f);
        dialogueButtonRect.localPosition = posOutOfUI;
        nameDialogueRect.localPosition = posOutOfUI;
        dialogueRect.localPosition = posOutOfUI;
        dialogueBGRect.localPosition = posOutOfUI; 
        caracterSpriteRect.localPosition = posOutOfUI;
        returnButtonRect.localPosition = returnButtonRectPos;
    }

    void putBackDialogueObjectToUI()
    {
        posOutOfUI = new Vector3(1001f, 1000f, 0f);
        dialogueButtonRect.localPosition = dialogueButtonRectPos;
        nameDialogueRect.localPosition = nameDialogueRectPos;
        dialogueRect.localPosition = dialogueRectPos;
        dialogueBGRect.localPosition = dialogueBGRectPos;
        caracterSpriteRect.localPosition = caracterSpriteRectPos;
        returnButtonRect.localPosition = posOutOfUI;
    }

    public void button()
    {
        if (dialogueButtonRectPos != dialogueButtonRect.localPosition)
        //Vérifie si les objets sont positioné en mode "dialogue"
        {
            putBackDialogueObjectToUI();
        }

    }
}
