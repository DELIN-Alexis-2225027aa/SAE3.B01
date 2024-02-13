using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class ClassroomSpriteSetter : MonoBehaviour
{

    string json;
    string filePath;
    [SerializeField] Image background;
    private string classroomNumber;

    // Start is called before the first frame update
    void Start()
    {
        filePath = Application.dataPath + "/SaveJson/classroom.json";
        LoadClassroomSprites();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadClassroomSprites()
    {
        if (File.Exists(filePath))
        {
            // Lire le JSON depuis le fichier
            string json = File.ReadAllText(filePath);
            classroomNumber = json;
            string imagePath = Path.Combine(Application.dataPath, "Images", classroomNumber);

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
}
