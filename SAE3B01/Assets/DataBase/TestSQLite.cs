using UnityEngine;
using System.Collections.Generic;

public class TestSQLite : MonoBehaviour
{
    private DBManager dbManager;

    void Start()
    {
        dbManager = new DBManager();
        string[] colonnes = { "ID", "nom", "posID", "dialogue" };
        string[] types = { "INTEGER", "TEXT", "TEXT", "TEXT" };
        dbManager.CreateTable("Dialogues", colonnes, types);
        
        string[] skibidiToilet = { "Bonjour, tu es bien l’étudiant de 2ème année chargé de convaincre les nouveaux arrivants au BUT Informatique ?", " Retrouve moi au bureau de la cheffe de département au RDC pour te donner quelques indications." };
        string skibidiFortnite = string.Concat(skibidiToilet);
        string[] cool = { "1", "2" };
        string pakool = string.Join(",", cool);
        string[] ui = { "1", "MAKSSOUD", pakool, skibidiFortnite };
        dbManager.Insert("Dialogues", ui);
        Debug.Log(skibidiFortnite);


        List<List<object>> resultat = dbManager.Selection("Dialogues", "*", "ID = 1");

        foreach (List<object> row in resultat)
        {
            List<string> rowStrings = row.ConvertAll(obj => obj.ToString());
            string rowString = string.Join(", ", rowStrings);
            Debug.Log("Ligne trouvée : " + rowString);
        }
        /*
        List<List<object>> resultat2 = dbManager.Selection("Inventaire", "*", "objet = ''");

        foreach (List<object> row2 in resultat2)
        {
            object rowString2 = string.Join(", ", row2);
            Debug.Log("Ligne trouvée : " + rowString2);
        }
        */
    }        

    void OnDestroy()
    {
        dbManager.CloseConnexion();
    }
}