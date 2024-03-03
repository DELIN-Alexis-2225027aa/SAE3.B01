using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestSQLite : MonoBehaviour
{
    private DBManager dbManager;

    void Start()
    {
        dbManager = new DBManager();

        dbManager.Droptable("Dialogues");
        dbManager.Droptable("PlayerData");

        string[] columns = { "iD", "name", "posID", "dialogue" };
        string[] types = { "INTEGER", "BLOB", "BLOB", "BLOB" };
        dbManager.CreateTable("Dialogues",columns , types);

        string[] columns2 = { "classroomName" };
        string[] types2 = { "BLOB" };
        dbManager.CreateTable("Classroom", columns2, types2);

        string[] columns3 = { "xPos", "yPos" };
        string[] types3 = { "BLOB", "BLOB" };
        dbManager.CreateTable("PlayerPos", columns3, types3);

        string[] columns4 = { "playerName", "playerGender" };
        string[] types4 = { "BLOB", "BLOB" };
        dbManager.CreateTable("PlayerData", columns4, types4);


        string iD = "1";
        string name = "MAKSSOUD";
        string posID = "1,2,2";
        //string dialogues = "Bonjour,| tu es bien l’étudiant de 2ème année chargé de convaincre les nouveaux arrivants au BUT Informatique ?| Retrouve moi au bureau de la cheffe de département au RDC pour te donner quelques indications.";
        string dialogues = "Bonjour $ ! Je suis ravi que tu te sois porté volontaire pour la JPO. Je compte sur toi pour convaincre un maximum de lycéens de venir dans notre établissement l’année prochaine.|J’ai quelques consignes à te donner mais elles sont sur mon bureau, rejoins moi plus tard.";
        string[] values = {iD,name, posID, dialogues};
        dbManager.Insert("Dialogues",values);

        string iD2 = "2";
        string name2 = "paper";
        string posID2 = "1,1,1";
        string dialogue2 = "La réunion avec les lycéens va bientôt commencer!|Je suis désolé, je n’ai pas réussi à finaliser le livret d’informations à temps, il va falloir que tu t’en charges.|J’ai prévenu tes professeurs.|Rends toi dans toutes les salles du BUT qui sont ouvertes et n’oublie pas, pour voir le plan des salles, appuie sur “ , ”, et pour rentrer dans une salle appuie sur “ E ”. ";
        string[] values2 = { iD2, name2, posID2, dialogue2 };
        dbManager.Insert("Dialogues", values2);

        string iD3 = "3";
        string name3 = "NEUVOT";
        string posID3 = "";
        string dialogue3 = "Bien le bonjour $, je t’attendais pour te remettre la liste des SAÉs réalisées cette année !|Si tu ne sais pas où tu l’as rangée, il te suffit d’appuyer sur “ echap ” pour la consulter !|Eh ! Je compte sur toi pour leur donner envie aux p’tits jeunes !";
        string[] values3 = { iD3, name3, posID3, dialogue3 };
        dbManager.Insert("Dialogues", values3);


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