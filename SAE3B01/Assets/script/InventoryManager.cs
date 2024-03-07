using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;

public class YourScript : MonoBehaviour
{
    private const int NumProofs = 6;

    private DBManager dbManager;
    private ValluesConvertor valluesConvertor;
    [SerializeField] private string[] proofsID;
    [SerializeField] private string[] proofsName;
    [SerializeField] private string[] proofsDescription;
    [SerializeField] private string[] proofsIsCollected;

private void Start()
{
    InitializeComponents();
    InitializeProofArrays();
    getIngo();
    CheckAndApplyImages();
}


private void InitializeComponents()
    {
        dbManager = new DBManager();
        valluesConvertor = new ValluesConvertor();
    }

private void InitializeProofArrays()
{
    proofsID = new string[NumProofs];
    proofsName = new string[NumProofs];
    proofsDescription = new string[NumProofs];
    proofsIsCollected = new string[NumProofs];
}


private void getInfo()
{
    for (int i = 0; i < NumProofs; ++i)
    {
           LoadProofDataForIndex(i);
    }
}

private void LoadProofDataForIndex(int index)
{
    List<List<object>> idResult = dbManager.Select("Proof", "ID", (index + 1).ToString());
    List<List<object>> nameResult = dbManager.Select("Proof", "name", (index + 1).ToString());
    List<List<object>> descriptionResult = dbManager.Select("Proof", "Description", (index + 1).ToString());
    List<List<object>> isCollectedResult = dbManager.Select("Proof", "isCollected", (index + 1).ToString());

    if (idResult.Count > 0)
    {
        List<object> idRow = idResult[0];
        List<object> nameRow = nameResult.Count > 0 ? nameResult[0] : new List<object>();
        List<object> descriptionRow = descriptionResult.Count > 0 ? descriptionResult[0] : new List<object>();
        List<object> isCollectedRow = isCollectedResult.Count > 0 ? isCollectedResult[0] : new List<object>();

        string id = valluesConvertor.convertRowToString(idRow);
        if (!isThereAlreadyThisValue(id))
        {
            proofsID[index] = id;
            proofsName[index] = valluesConvertor.convertRowToString(nameRow);
            proofsDescription[index] = valluesConvertor.convertRowToString(descriptionRow);
            proofsIsCollected[index] = valluesConvertor.convertRowToString(isCollectedRow);
        }
    }
    else
    {
        Debug.LogError($"No data found for Proof with ID: {index + 1}");
    }
} 


    public bool isThereAlreadyThisValue(string str){
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


    public void CheckAndApplyImages()
    {
        string[] proofNameForFile = {"","","","","",""};
        for (int i = 0; i < proofsIsCollected.Length; i++)
        {
            index = i;
            string itemName = "itemArea" + (i + 1);
            string proofName = proofsName[i];
            proofNameForFile[i] = putProofNameOnRightFormat(proofsName[i]);
            CheckAndApplyImageForItemArea(GameObject.Find(itemName), proofsIsCollected[i], proofNameForFile[i], proofName, proofsID[i]);
        }
    }

    public string putProofNameOnRightFormat (string str){
        string strFirstModification = str.Replace(' ', '_');
        string strSecondModification = strFirstModification.Replace('é', 'e');
        string strThirdModification = strSecondModification.Replace('è', 'e');
        string strToReturn = strThirdModification.ToUpper();
        return strToReturn;
    }

    private void CheckAndApplyImageForItemArea(GameObject itemArea, string value, string imageName, string proofName , string id)
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
    private void ApplyText(GameObject itemArea, string name)
    {
        TextMeshProUGUI txt = itemArea.transform.Find("name").GetComponent<TextMeshProUGUI>();
        txt.text = name;
    }

    public void storeItemInClass(string id){
        if(inventoryManagement[index].Equals(".")){
            inventoryManagement[index] = id;
        }else Debug.LogError(inventoryManagement[index]);
    }

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
