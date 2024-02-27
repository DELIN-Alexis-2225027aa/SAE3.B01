using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ValluesConvertor
{
    private DBManager dbManager;

    string resultString;

    // Start is called before the first frame update
    void Start()
    {
        dbManager = new DBManager();

        List<List<object>> resultat = dbManager.Select("Dialogues", "dialogue", "ID = 1");

        foreach (List<object> row in resultat)
        {
            string[] strArray = ConvertrowToStringArray(row);
            if (strArray != null)
            {
                for (int i = 0; i < strArray.Length; ++i)
                {
                    Debug.Log(strArray[i]);
                }
            }
            else Debug.Log("Erreur");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public ValluesConvertor()
    {
        // Constructor logic, if needed
    }

    public string[] ConvertrowToStringArray(List<object> row)
    {

        string str = convertRowToString(row);
        if (str.Any(char.IsLetter))
        {
            if (checkIfStringNeedToSplit(str))
            {
                return convertDBStringToStringArray(str);
            }
            else return null;
        }
        else return null;
    }

    public bool checkIfStringNeedToSplit(string strToCheck)
    {
        return strToCheck.Contains("|");
    }

    public string convertRowToString(List<object> rowToConvert)
    {
        byte[] bytesArray = (byte[])rowToConvert[0];
        resultString = System.Text.Encoding.UTF8.GetString(bytesArray);
        return resultString;
    }

    public string[] convertDBStringToStringArray(string dbString)
    {
        return dbString.Split('|');
    }

    void OnDestroy()
    {
        dbManager.CloseConnexion();
    }

    public int[] convertDBstringToIntArray(string dbString)
    {
        return dbString.Split(',').Select(int.Parse).ToArray();
    }
}

