using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestSQLite : MonoBehaviour
{
    private DBManager dbManager;

    void Start()
    {
        dbManager = new DBManager();

        string[] columns = { "iD", "name", "posID", "dialogue" };
        string[] types = { "INTEGER", "BLOB", "BLOB", "BLOB" };
        dbManager.CreateTable("Dialogues", columns, types);

        string[] columns2 = { "classroomName" };
        string[] types2 = { "BLOB" };
        dbManager.CreateTable("Classroom", columns2, types2);

        string[] columns3 = { "xPos", "yPos" };
        string[] types3 = { "BLOB", "BLOB" };
        dbManager.CreateTable("PlayerPos", columns3, types3);

        string[] columns4 = { "playerName", "playerGender" };
        string[] types4 = { "BLOB", "BLOB" };
        dbManager.CreateTable("PlayerData", columns4, types4);

        string[] columns5 = { "ID", "Proof", "Description", "isEarned" };
        string[] types5 = { "INTEGER", "BLOB","BLOB", "BLOB" };
        dbManager.CreateTable("ProofToEarn", columns5, types5);

        string[] columns6 = { "ID", "Proof", "Description", "isEarned" };
        string[] types6 = { "INTEGER", "BLOB","BLOB", "BLOB" };
        dbManager.CreateTable("Inventorty", columns6, types6);

        dbManager.DeleteEverythingFromTable("Dialogues");
        dbManager.DeleteEverythingFromTable("Classroom");
        dbManager.DeleteEverythingFromTable("PlayerPos");
        dbManager.DeleteEverythingFromTable("PlayerData");

        initTable();
    }

    void OnDestroy()
    {
        dbManager.CloseConnexion();
    }

    public void initTable()
    {
        dbManager = new DBManager();

        string iD = "1";
        string name = "MAKSSOUD|MAKSSOUD|MAKSSOUD";
        string posID = "1,2,2";
        string dialogue = "Bonjour $ ! Je suis ravi que tu te sois porté volontaire pour la JPO. Je compte sur toi pour convaincre un maximum de lycéens de venir dans notre établissement l’année prochaine.|J’ai quelques consignes à te donner mais elles sont sur mon bureau, rejoins moi plus tard.";
        string[] values = { iD, name, posID, dialogue };
        dbManager.Insert("Dialogues", values);

        string iD2 = "2";
        string name2 = "PAPIER|PAPIER|PAPIER";
        string posID2 = "1,1,1";
        string dialogue2 = "La réunion avec les lycéens va bientôt commencer!|Je suis désolé, je n’ai pas réussi à finaliser le livret d’informations à temps, il va falloir que tu t’en charges.|J’ai prévenu tes professeurs.|Rends toi dans toutes les salles du BUT qui sont ouvertes et n’oublie pas, pour voir le plan des salles, appuie sur “ , ”, et pour rentrer dans une salle appuie sur “ E ”. ";
        string[] values2 = { iD2, name2, posID2, dialogue2 };
        dbManager.Insert("Dialogues", values2);

        string iD3 = "3";
        string name3 = "NEUVOT|NEUVOT|NEUVOT";
        string posID3 = "2,1,2";
        string dialogue3 = "Bien le bonjour $, je t’attendais pour te remettre la liste des SAÉs réalisées cette année !|Si tu ne sais pas où tu l’as rangée, il te suffit d’appuyer sur “ echap ” pour la consulter !|Eh ! Je compte sur toi pour leur donner envie aux p’tits jeunes !";
        string[] values3 = { iD3, name3, posID3, dialogue3 };
        dbManager.Insert("Dialogues", values3);

        string iD4 = "4";
        string name4 = "$";
        string posID4 = "1";
        string dialogue4 = "Ah oui, il ne faut pas que j’oublie de parler des matières complémentaires !";
        string[] values4 = { iD4, name4, posID4, dialogue4};
        dbManager.Insert("Dialogues", values4);
        
        string iD5 = "5";
        string name5 = "$";
        string posID5 = "1";
        string dialogue5 = "Ça, j’le connais par cœur, mais bon, avec le trac, j’préfère l’avoir avec moi.";
        string[] values5 = { iD5, name5, posID5, dialogue5 };
        dbManager.Insert("Dialogues", values5);

        string iD6 = "6";
        string name6 = "MAKSSOUD|MAKSSOUD";
        string posID6 = "1,2";
        string dialogue6 = "Ah ! $ tu tombes bien ! J’ai oublié de te dire, ce serait peut être bien de parler des débouchés durant la réunion.|Tiens, je te remets ce document, c’est la liste des métiers exercés par nos anciens élèves, ça devrait t’aider pour répondre aux questions des lycéens.";
        string[] values6 = { iD6, name6, posID6, dialogue6 };
        dbManager.Insert("Dialogues", values6);

        string iD7 = "7";
        string name7 = "Myke|$|Myke";
        string posID7 = "1,1,2";
        string dialogue7 = "Salut [nom], tu viens jouer à Smash Bros ?|Ah, non pas aujourd’hui Myke. Là, il faut vraiment que tu m’aides. T’aurais pas le programme des activités du BDE sous la main ?|Ouais bien sûr, pas de soucis. Et en plus, là on réfléchit à plein de nouveaux trucs.";
        string[] values7 = { iD7, name7, posID7, dialogue7 };
        dbManager.Insert("Dialogues", values7);

        string iD8 = "8";
        string name8 = "Parrain|$|Parrain";
        string posID8 = "1,1,1";
        string dialogue8 = "Salut le filleul ! Toi, t’as encore besoin d’un conseil de ton parrain préféré !|Eh ouais, j’voudrais des infos concernant les stages et l’alternance.|Ah oui, la cheffe de dep m’avait dit que tu en aurais besoin ! Tiens, je t’ai préparé ça.";
        string[] values8 = { iD8, name8, posID8, dialogue8 };
        dbManager.Insert("Dialogues", values8);


        // initialisation des preuves

        string iDProof = "1";
        string proofName = "Liste des SAE";
        string description = "Il y a de nombreuses SAE, toutes plus intéressantes les unes que les autres. Voici certains projets que vous pourrez avoir en 2 années de BUT : Programmation d’un PacMan. Production d’un escape game. Conception d’un jeu vidéo en rapport avec le BUT. Création d’un code pour visualiser les catastrophes naturelles en France. Création d’un réseau social inédit. ";
        string isEarned = "F";
        string[] Proofvalues = { iDProof, proofName, description, isEarned };
        dbManager.Insert("Dialogues", Proofvalues);

        string iDProof2 = "2";
        string proofName2 = "Stage & Alternance";
        string description2 = "Information sur l'Alternance Durée : 1 ou 2 ans Objectifs : Acquérir une expérience professionnelle tout en poursuivant ses études Avantages : Rémunération, acquisition de compétences pratiques, possibilité d'être embauché à la fin de la formation | Informations sur le Stage : Durée : 11 semaines (à la fin de la deuxième année) Objectifs : Découverte du milieu professionnel, mise en pratique des connaissances théoriques Avantages : Acquisition d'expérience, possibilité d'obtenir une offre d'emploi à l'issue du stage";
        string isEarned2 = "F";
        string[] Proofvalues2 = { iDProof2, proofName2, description2, isEarned2 };
        dbManager.Insert("Dialogues", Proofvalues2);

        string iDProof3 = "3";
        string proofName3 = "BDE";
        string description3 = "Il y a de nombreuses SAE, toutes plus intéressantes les unes que les autres. Voici certains projets que vous pourrez avoir en 2 années de BUT : Programmation d’un PacMan. Production d’un escape game. Conception d’un jeu vidéo en rapport avec le BUT. Création d’un code pour visualiser les catastrophes naturelles en France. Création d’un réseau social inédit. ";
        string isEarned3 = "F";
        string[] Proofvalues3 = { iDProof3, proofName3, description3, isEarned3 };
        dbManager.Insert("Dialogues", Proofvalues3);
    
        LogTable("Dialogues");
        LogTable("Classroom");
        LogTable("PlayerPos");
        LogTable("PlayerData");
        LogTable("ProofToEarn");
        LogTable("Inventorty");
    }

    private void LogTable(string tableName)
    {
        List<List<object>> tableContents = dbManager.SelectAll(tableName);

        Debug.Log($"Table: {tableName}");

        foreach (List<object> row in tableContents)
        {
            string rowString = string.Join(", ", row.Select(obj => obj.ToString()).ToArray());
            Debug.Log(rowString);
        }

        Debug.Log($"End of Table: {tableName}");
    }

}