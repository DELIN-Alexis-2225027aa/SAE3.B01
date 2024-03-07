using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class PlayerInfoDispayScript : MonoBehaviour
{
    [SerializeField] RectTransform infoUIPos;
    [SerializeField] TMP_InputField textFieldName;

    private DBManager dbManager;
    
    public Vector3 infoUIShownPos;
    public Vector3 posOutOfUI;
    private string gender;

    // Start is called before the first frame update
    void Start()
    {
        dbManager = new DBManager();
        savePos();
        putWindowOutOfUI();
    }

    public void showOnUI()
    {
        putWindowInUI();
    }

    public void savePos()
    {
        infoUIShownPos = infoUIPos.localPosition;
    }

    public void putWindowOutOfUI()
    {
        posOutOfUI = new Vector3(1001f, 1000f, 0f);
        infoUIPos.localPosition = posOutOfUI;
    }

    public void putWindowInUI()
    {
        infoUIPos.localPosition = infoUIShownPos;
    }

    public void onMaleButtonPressed()
    {
        gender = "M";
        onGenderSelectionButtonPressed();
    }

    public void onFemaleButtonReleased() 
    {
        gender = "F";
        onGenderSelectionButtonPressed();
    }

    public bool isTextInsertInTextField()
    {
        if (textFieldName.text != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void onGenderSelectionButtonPressed()
    {
        if (isTextInsertInTextField())
        {
            string[] data = { textFieldName.text , gender };
            dbManager.Insert("PlayerData", data);
            SceneManager.LoadScene("IntroVideo");
        }
    }
}
