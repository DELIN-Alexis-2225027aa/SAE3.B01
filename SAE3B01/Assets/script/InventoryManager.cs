using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;

/// <summary>
/// Gère la gestion et l'affichage de l'inventaire du joueur.
/// </summary>
public class InventoryManager : MonoBehaviour
{
    ClassroomSpriteSetter classroomSpriteSetter;
    DBManager dbManager;
    ValluesConvertor valluesConvertor;
    [SerializeField] string[] proofsID;
    [SerializeField] string[] proofsName;
    [SerializeField] string[] proofsDescription;
    [SerializeField] string[] proofsIsCollected;
    [SerializeField] string[] inventoryManagement;
    [SerializeField] int index;
    [SerializeField] Image descriptionImg;
    [SerializeField] string imgName;
    [SerializeField] RectTransform descriptionRect;

    [SerializeField] Vector3 descriptionRectPos;
    [SerializeField] Vector3 posOutOfUI;

    private int proofID;
    private string isProofCollected;
    private string spriteName;

    void Start()
    {
        // Initialisation des positions pour l'affichage des descriptions
        descriptionRectPos = descriptionRect.localPosition;
        posOutOfUI = new Vector3(1000f, 1000f, 0f);
        descriptionRect.localPosition = posOutOfUI;

        // Initialisation des gestionnaires
        dbManager = new DBManager();
        valluesConvertor = new ValluesConvertor();

        // Initialisation des tableaux de preuves
        proofsID = new string[] { ".", ".", ".", ".", ".", "." };
        proofsName = new string[] { ".", ".", ".", ".", ".", "." };
        proofsDescription = new string[] { ".", ".", ".", ".", ".", "." };
        proofsIsCollected = new string[] { ".", ".", ".", ".", ".", "." };
        inventoryManagement = new string[] { ".", ".", ".", ".", ".", "." };

        // Récupération des informations des preuves depuis la base de données
        getInfo(valluesConvertor, dbManager);
        // Vérification et application des images dans l'inventaire
        CheckAndApplyImages();
    }

    /// <summary>
    /// Récupère les informations des preuves depuis la base de données.
    /// </summary>
    public void getInfo(ValluesConvertor valluesConvertor, DBManager dbManager)
    {
        for (int i = 0; i < 6; ++i)
        {
            List<List<object>> idResult = dbManager.Select("Proof", "ID", (i + 1).ToString());
            List<List<object>> nameResult = dbManager.Select("Proof", "name", (i + 1).ToString());
            List<List<object>> descriptionResult = dbManager.Select("Proof", "Description", (i + 1).ToString());
            List<List<object>> isCollectedResult = dbManager.Select("Proof", "isCollected", (i + 1).ToString());

            if (idResult.Count > 0)
            {
                List<object> idRow = idResult[i];
                List<object> nameRow = nameResult[i];
                List<object> descriptionRow = descriptionResult[i];
                List<object> isCollectedRow = isCollectedResult[i];

                // Vérifie si la preuve n'a pas déjà été ajoutée
                if (!isThereAlreadyThisValue(valluesConvertor.convertRowToString(idRow)))
                {
                    proofsID[i] = valluesConvertor.convertRowToString(idRow);
                    proofsName[i] = valluesConvertor.convertRowToString(nameRow);
                    proofsDescription[i] = valluesConvertor.convertRowToString(descriptionRow);
                    proofsIsCollected[i] = valluesConvertor.convertRowToString(isCollectedRow);
                }
            }
        }
    }

    /// <summary>
    /// Vérifie si une valeur existe déjà dans le tableau.
    /// </summary>
    public bool isThereAlreadyThisValue(string str)
    {
        bool result = false;
        for (int i = 0; i < 6; ++i)
        {
            if (proofsID[i].Equals(str))
            {
                result = true;
                break;
            }
        }
        return result;
    }

    /// <summary>
    /// Vérifie et applique les images pour les zones d'objets dans l'inventaire.
    /// </summary>
    public void CheckAndApplyImages()
    {
        string[] proofNameForFile = { "", "", "", "", "", "" };
        for (int i = 0; i < proofsIsCollected.Length; i++)
        {
            index = i;
            string itemName = "itemArea" + (i + 1);
            string proofName = proofsName[i];
            proofNameForFile[i] = putProofNameOnRightFormat(proofsName[i]);
            CheckAndApplyImageForItemArea(GameObject.Find(itemName), proofsIsCollected[i], proofNameForFile[i], proofName, proofsID[i]);
        }
    }

    /// <summary>
    /// Met le nom de la preuve dans le bon format pour le fichier image.
    /// </summary>
    public string putProofNameOnRightFormat(string str)
    {
        string strFirstModification = str.Replace(' ', '_');
        string strSecondModification = strFirstModification.Replace('é', 'e');
        string strThirdModification = strSecondModification.Replace('è', 'e');
        string strToReturn = strThirdModification.ToUpper();
        return strToReturn;
    }

    /// <summary>
    /// Vérifie et applique l'image pour la zone d'objet spécifiée.
    /// </summary>
    private void CheckAndApplyImageForItemArea(GameObject itemArea, string value, string imageName, string proofName, string id)
    {
        if (itemArea != null && value.Equals("T"))
        {
            Image imageComponent = itemArea.transform.Find("img").GetComponent<Image>();

            string spriteName = $"{imageName}.png";
            string imagePath = Path.Combine(Application.dataPath, "Images/Collectibles", spriteName);
            if (File.Exists(imagePath))
            {
                byte[] fileData = File.ReadAllBytes(imagePath);
                Texture2D texture = new Texture2D(2, 2);
                texture.LoadImage(fileData);
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                imageComponent.sprite = sprite;

                ApplyText(itemArea, proofName);
                storeItemInClass(id);
            }
        }
    }

    /// <summary>
    /// Applique le texte pour la zone d'objet spécifiée.
    /// </summary>
    private void ApplyText(GameObject itemArea, string name)
    {
        TextMeshProUGUI txt = itemArea.transform.Find("name").GetComponent<TextMeshProUGUI>();
        txt.text = name;
    }

    /// <summary>
    /// Stocke l'objet dans la classe d'inventaire.
    /// </summary>
    public void storeItemInClass(string id)
    {
        if (inventoryManagement[index].Equals("."))
        {
            inventoryManagement[index] = id;
        }
        else Debug.LogError(inventoryManagement[index]);
    }

    /// <summary>
    /// Gestion du clic sur les boutons de description.
    /// </summary>
    public void ButtonClick()
    {
        Button clickedButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        switch (clickedButton.name)
        {
            case "Button1":
                imgName = "1";
                break;
            case "Button2":
                imgName = "2";
                break;
            case "Button3":
                imgName = "3";
                break;
            case "Button4":
                imgName = "4";
                break;
            case "Button5":
                imgName = "5";
                break;
            case "Button6":
                imgName = "6";
                break;
        }
        if (imgName != null)
        {
            if (inventoryManagement[int.Parse(imgName) - 1] != ("."))
            {
                descriptionRect.localPosition = descriptionRectPos;
                string spriteName = $"{imgName}.png";
                string imagePath = Path.Combine(Application.dataPath, "Images/Descriptions", spriteName);
                if (File.Exists(imagePath))
                {
                    byte[] fileData = File.ReadAllBytes(imagePath);
                    Texture2D texture = new Texture2D(2, 2);
                    texture.LoadImage(fileData);
                    Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                    descriptionImg.sprite = sprite;
                }
            }
        }
    }
}
