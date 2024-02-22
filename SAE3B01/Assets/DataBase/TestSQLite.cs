using UnityEngine;
using System.Collections.Generic;

public class TestSQLite : MonoBehaviour
{
    private DBManager dbManager;

    void Start()
    {
        dbManager = new DBManager();

        List<List<object>> resultat = dbManager.Selection("Dialogues", "dialogue", "dialogueID = 1");

        foreach (List<object> row in resultat)
        {
            object rowString = string.Join(", ", row);
            Debug.Log("Ligne trouvée : " + rowString);
        }

        List<List<object>> resultat2 = dbManager.Selection("Inventaire", "*", "objet = ''");

        foreach (List<object> row2 in resultat2)
        {
            object rowString2 = string.Join(", ", row2);
            Debug.Log("Ligne trouvée : " + rowString2);
        }
    }

    void OnDestroy()
    {
        dbManager.FermerConnexion();
    }
}