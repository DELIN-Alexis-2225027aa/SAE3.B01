using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestSQLite : MonoBehaviour
{
    private DBManager dbManager;

    void Start()
    {
        dbManager = new DBManager();

        //dbManager.Droptable("Dialogues");

        string[] columns = { "iD", "name", "posID", "dialogue" };
        string[] types = { "INTEGER", "BLOB", "BLOB", "BLOB" };
        dbManager.CreateTable("Dialogues",columns , types);

        string[] columns2 = { "classroomName" };
        string[] types2 = { "BLOB" };
        dbManager.CreateTable("Classroom", columns2, types2);

        string iD = "2";
        string name = "MAKSSOUD";
        string posID = "1,2,2";
        string dialogues = "Bonjour,| tu es bien l’étudiant de 2ème année chargé de convaincre les nouveaux arrivants au BUT Informatique ?| Retrouve moi au bureau de la cheffe de département au RDC pour te donner quelques indications.";
        string[] values = {iD,name, posID, dialogues};
        dbManager.Insert("Dialogues",values);

        /*List<List<object>> resultat = dbManager.Select("Dialogues", "dialogue", "ID = 1");

        foreach (List<object> row in resultat)
        {
            //object rowString = string.Join(", ", row);
            byte[] nameBytes = (byte[])row[0];
            string resultString = System.Text.Encoding.UTF8.GetString(nameBytes);
            Debug.Log("Ligne trouvée : " + resultString);
        }

        List<List<object>> resultat2 = dbManager.Selection("Inventaire", "*", "objet = ''");

        foreach (List<object> row2 in resultat2)
        {
            object rowString2 = string.Join(", ", row2);
            Debug.Log("Ligne trouvée : " + rowString2);
        }*/
    }

    void OnDestroy()
    {
        dbManager.CloseConnexion();
    }
}