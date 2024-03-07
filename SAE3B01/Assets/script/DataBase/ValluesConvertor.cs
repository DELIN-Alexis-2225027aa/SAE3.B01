using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ValluesConvertor
{
    private DBManager dbManager;
    
    int resultInt;
    string resultString;
    public string name;

    // Start is called before the first frame update
    void Start()
    {
        dbManager = new DBManager();
    }

    public ValluesConvertor()
    {
        // Constructor logic, if needed
    }

    public string[] ConvertRowToStringArray(List<object> row)
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
        if (nameNeedChecker(resultString))
        {
            string playerName = getName();
            resultString = putNameInStr(resultString, playerName);
        }
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

    public string putNameInStr(string strToChange, string playerName)
    {
        strToChange = strToChange.Replace("$", playerName);
        return strToChange;
    }

    public bool nameNeedChecker(string str)
    {
        bool isNameNeeded = str.Contains("$");
        return isNameNeeded;
    }
    public string getName()
    {
        dbManager = new DBManager();

        List<List<object>> resultat = dbManager.Select("PlayerData", "playerName","1");
        foreach (List<object> rowName in resultat)
        {
            byte[] bytesArray = (byte[])rowName[0];
            name = System.Text.Encoding.UTF8.GetString(bytesArray);
        }

        return name;
    }
}

