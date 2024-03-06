using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;


public class ClassroomSpriteSetter : MonoBehaviour
{
    private ValluesConvertor valluesConvertor;
    private DBManager dbManager;

    [SerializeField] Image background;
    [SerializeField] Button interactiveObject;
    [SerializeField] RectTransform dialogueButtonRect;
    [SerializeField] RectTransform nameDialogueRect;
    [SerializeField] RectTransform dialogueRect;
    [SerializeField] RectTransform dialogueBGRect;
    [SerializeField] RectTransform caracterSpriteRect;
    [SerializeField] RectTransform returnButtonRect;
    [SerializeField] Transform isDialogueFinished;
    [SerializeField] RectTransform interactiveObjectPos;


    string json;
    private string classroomNumber;
    private string spriteName;
    string imagePath;
    string strClassroomName;

    float xSize;
    float ySize;

    Vector3 dialogueButtonRectPos;
    Vector3 nameDialogueRectPos;
    Vector3 dialogueRectPos;
    Vector3 dialogueBGRectPos;
    Vector3 caracterSpriteRectPos;
    Vector3 returnButtonRectPos;
    Vector3 posOutOfUI;
    Vector3 PosChecker;

    // Start is called before the first frame update
    void Start()
    {
        dbManager = new DBManager();
        valluesConvertor = new ValluesConvertor();
        saveDialogueObjectPos();
        LoadClassroomSprites();
        SetupInteractibleObject();
        loadInteractiveObjectSprite();
        removeDialogueObjectFromUI();
        resizeInteractiveObjectByNameOfTheClassroom();
        PosChecker = new Vector3(0f, 0f, 0f);
        posOutOfUI = new Vector3(1000f, 1000f, 0f);
    }
    void Update()
    {
        if (isDialogueFinished.localPosition == PosChecker)
        {
            if (nameDialogueRectPos == nameDialogueRect.localPosition)
            {
                removeDialogueObjectFromUI();
                isDialogueFinished.localPosition = posOutOfUI;
            }
        }
    }

    void LoadClassroomSprites()
    {
        strClassroomName = getClassroomName(dbManager, valluesConvertor);
        spriteName = $"{strClassroomName}.png";
        imagePath = Path.Combine(Application.dataPath, "Images/Classe", spriteName);
        byte[] fileData = File.ReadAllBytes(imagePath);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(fileData);
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        background.sprite = sprite;
    }


    void SetupInteractibleObject()
    {
        float x = 0f;
        float y = 0f;
        float width = 0f;
        float height = 0f;
        switch (strClassroomName)
        {
            case "000":
                x = 1000f;
                y = 1000f;
                break;
            case "MAK":
                break;
            case "BDE":
                x = Screen.width / 100;
                y = Screen.height / 5;
                width = 320f;
                height = 400f;
                break;
            case "002":
                x = Screen.width / 3;
                y = -Screen.height / 5;
                width = 320f;
                height = 400f;
                break;
            case "010":
                x = Screen.width / 30;
                y = -Screen.height / 5.3f;
                width = 80f;
                height = 80f;
                break;
            case "109":
                x = -Screen.width / 2.2f;
                y = -Screen.height / 15;
                width = 80f;
                height = 80f;
                break;
            case "110":
                x = Screen.width / 3;
                y = -Screen.height / 5;
                width = 320f;
                height = 400f;
                break;
            case "208":
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
        switch (strClassroomName)
        {
            case "BDE":
                interactiveObjectSprite = "Myke1";
                break;
            case "MAK":
                interactiveObjectSprite = "Papier1";
                break;
            case "002":
                interactiveObjectSprite = "NEUVOT1";
                break;
            case "010":
                interactiveObjectSprite = "Papier1";
                break;
            case "109":
                interactiveObjectSprite = "Papier2";
                break;
            case "110":
                interactiveObjectSprite = "MAKSSOUD1";
                break;
            case "208":
                interactiveObjectSprite = "Parrain1";
                break;
        }
        if (interactiveObjectSprite != null)
        {
            spriteName = $"{interactiveObjectSprite}.png";
            imagePath = Path.Combine(Application.dataPath, "Images/Personnage", spriteName);
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
        posOutOfUI = new Vector3(1000f, 1000f, 0f);
        nameDialogueRect.localPosition = posOutOfUI;
        dialogueRect.localPosition = posOutOfUI;
        dialogueBGRect.localPosition = posOutOfUI;
        caracterSpriteRect.localPosition = posOutOfUI;
        returnButtonRect.localPosition = returnButtonRectPos;
        dialogueButtonRect.localPosition = posOutOfUI;
    }

    void putBackDialogueObjectToUI()
    {
        posOutOfUI = new Vector3(1000f, 1000f, 0f);
        dialogueButtonRect.localPosition = dialogueButtonRectPos;
        nameDialogueRect.localPosition = nameDialogueRectPos;
        dialogueRect.localPosition = dialogueRectPos;
        dialogueBGRect.localPosition = dialogueBGRectPos;
        caracterSpriteRect.localPosition = caracterSpriteRectPos;
        returnButtonRect.localPosition = posOutOfUI;
    }

    public void button()
    {

        if (nameDialogueRectPos != nameDialogueRect.localPosition)
        //Vérifie si les objets sont positioné en mode "dialogue"
        {
            putBackDialogueObjectToUI();
        }

    }

    public string getClassroomName(DBManager dbManager, ValluesConvertor valluesConvertor)
    {
        List<List<object>> resultat = dbManager.Select("Classroom", "classroomName", "1");
        string classroomRow = null;
        if (resultat.Count > 0)
        {
            foreach (List<object> row in resultat)
            {
                classroomRow = valluesConvertor.convertRowToString(row);
            }
        }
        return classroomRow;
    }

    public void resizeInteractiveObjectByNameOfTheClassroom()
    {
        switch(strClassroomName)
        {
            case "MAK":
                xSize = 3;
                ySize = 3;
                break;
            case "BDE":
                xSize = 2;
                ySize = 2;
                break;
            case "002":
                xSize = 2;
                ySize = 2;
                break;
            case "010":
                xSize = 1;
                ySize = 1;
                break;
            case "109":
                xSize = 1;
                ySize = 1;
                break;
            case "110":
                xSize = 3;
                ySize = 3;
                break;
            case "208":
                break;
        }

        interactiveObjectPos.localScale = new Vector3(xSize, ySize, 1f);
    }
}
