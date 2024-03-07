using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestSQLite : MonoBehaviour
{
    private DBManager dbManager;

        string iD;
        string name;
        string posID;

        string iDProof;
        string pName;
        string description; 
        string isEarned;

        string xPos;
        string yPos;
        
    void Start()
    {
        dbManager = new DBManager();

        //deleteTables();
        createTables();
        resetTables();
        initTables();
    }

    public void createTables()
    {
        dbManager = new DBManager();

        string[] columns = { "iD", "name", "posID", "dialogue", "isFirstTime" };
        string[] types = { "INTEGER", "BLOB", "BLOB", "BLOB", "BLOB" };
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

        string[] columns5 = { "ID", "name", "Description", "isCollected" };
        string[] types5 = { "BLOB", "BLOB","BLOB", "BLOB" };
        dbManager.CreateTable("Proof", columns5, types5);

        string[] columns6 = {"sceneToResume"};
        string[] types6 = {"BLOB"};
        dbManager.CreateTable("SceneResume", columns6, types6);

    }

    public void deleteTables()
    {
        dbManager = new DBManager();

        dbManager.Droptable("Dialogues");
        dbManager.Droptable("Classroom");
        dbManager.Droptable("PlayerPos");
        dbManager.Droptable("PlayerData");
        dbManager.Droptable("Proof");
        dbManager.Droptable("SceneResume");

    }

    void resetTables()
    {
        dbManager = new DBManager();

        dbManager.DeleteEverythingFromTable("Dialogues");
        dbManager.DeleteEverythingFromTable("Classroom");
        dbManager.DeleteEverythingFromTable("PlayerPos");
        dbManager.DeleteEverythingFromTable("PlayerData");
        dbManager.DeleteEverythingFromTable("Proof");
        dbManager.DeleteEverythingFromTable("SceneResume");

    }

    public void initTables()
    {
        dbManager = new DBManager();

        iD = "1";
        name = "MAKSSOUD|MAKSSOUD";
        posID = "1,2";
        string dialogue = "Bonjour $ ! Je suis ravi que tu te sois porté volontaire pour la JPO. Je compte sur toi pour convaincre un maximum de lycéens de venir dans notre établissement l’année prochaine.|J’ai quelques consignes à te donner mais elles sont sur mon bureau, rejoins moi plus tard.";
        firstTime = "T";
        string[] values = { iD, name, posID, dialogue, firstTime};
        dbManager.Insert("Dialogues", values);

        iD = "2";
        name = "PAPIER|PAPIER|PAPIER";
        posID = "1,1,1";
        string dialogue2 = "La réunion avec les lycéens va bientôt commencer!|Je suis désolé, je n’ai pas réussi à finaliser le livret d’informations à temps, il va falloir que tu t’en charges.|J’ai prévenu tes professeurs.|Rends toi dans toutes les salles du BUT qui sont ouvertes et n’oublie pas, pour voir le plan des salles, appuie sur “ , ”, et pour rentrer dans une salle appuie sur “ E ”. ";
        string[] values2 = { iD, name, posID, dialogue2 };
        dbManager.Insert("Dialogues", values2);

        iD = "3";
        name = "NEUVOT|NEUVOT|NEUVOT";
        posID = "2,1,2";
        string dialogue3 = "Bien le bonjour $, je t’attendais pour te remettre la liste des SAÉs réalisées cette année !|Si tu ne sais pas où tu l’as rangée, il te suffit d’appuyer sur “ echap ” pour la consulter !|Eh ! Je compte sur toi pour leur donner envie aux p’tits jeunes !";
        string[] values3 = { iD, name, posID, dialogue3 };
        dbManager.Insert("Dialogues", values3);

        iD = "4";
        name = "$|$";
        posID = "1,1";
        string dialogue4 = "Ah oui, il ne faut pas que j’oublie de parler des matières complémentaires !|Liste des matières complémentaires obtenue";
        string[] values4 = { iD, name, posID, dialogue4};
        dbManager.Insert("Dialogues", values4);
        
        iD = "5";
        name = "$|$";
        posID = "1,1";
        string dialogue5 = "Ça, j’le connais par cœur, mais bon, avec le trac, j’préfère l’avoir avec moi.|Liste des matières informatiques obtenue";
        string[] values5 = { iD, name, posID, dialogue5 };
        dbManager.Insert("Dialogues", values5);

        iD = "6";
        name = "MAKSSOUD|MAKSSOUD";
        posID = "1,2";
        string dialogue6 = "Ah ! $ tu tombes bien ! J’ai oublié de te dire, ce serait peut être bien de parler des débouchés durant la réunion.|Tiens, je te remets ce document, c’est la liste des métiers exercés par nos anciens élèves, ça devrait t’aider pour répondre aux questions des lycéens.";
        string[] values6 = { iD, name, posID, dialogue6 };
        dbManager.Insert("Dialogues", values6);

        iD = "7";
        name = "Myke|$|Myke";
        posID = "1,1,2";
        string dialogue7 = "Salut [nom], tu viens jouer à Smash Bros ?|Ah, non pas aujourd’hui Myke. Là, il faut vraiment que tu m’aides. T’aurais pas le programme des activités du BDE sous la main ?|Ouais bien sûr, pas de soucis. Et en plus, là on réfléchit à plein de nouveaux trucs.";
        string[] values7 = { iD, name, posID, dialogue7 };
        dbManager.Insert("Dialogues", values7);

        iD = "8";
        name = "Parrain|$|Parrain";
        posID = "1,1,1";
        string dialogue8 = "Salut le filleul ! Toi, t’as encore besoin d’un conseil de ton parrain préféré !|Eh ouais, j’voudrais des infos concernant les stages et l’alternance.|Ah oui, la cheffe de dep m’avait dit que tu en aurais besoin ! Tiens, je t’ai préparé ça.";
        string[] values8 = { iD, name, posID, dialogue8 };
        dbManager.Insert("Dialogues", values8);


        // initialisation des preuves

        iDProof = "1";
        pName = "Liste des SAE";
        description = "Il y a de nombreuses SAE, toutes plus intéressantes les unes que les autres. Voici certains projets que vous pourrez avoir en 2 années de BUT : Programmation d’un PacMan. Production d’un escape game. Conception d’un jeu vidéo en rapport avec le BUT. Création d’un code pour visualiser les catastrophes naturelles en France. Création d’un réseau social inédit. ";
        isEarned = "T";
        string[] Proofvalues = { iDProof, pName, description, isEarned };
        dbManager.Insert("Proof", Proofvalues);

        iDProof = "2";
        pName = "Stage & Alternance";
        description = "Information sur l'Alternance Durée : 1 ou 2 ans Objectifs : Acquérir une expérience professionnelle tout en poursuivant ses études Avantages : Rémunération, acquisition de compétences pratiques, possibilité d'être embauché à la fin de la formation | Informations sur le Stage : Durée : 11 semaines (à la fin de la deuxième année) Objectifs : Découverte du milieu professionnel, mise en pratique des connaissances théoriques Avantages : Acquisition d'expérience, possibilité d'obtenir une offre d'emploi à l'issue du stage";
        isEarned = "T";
        string[] Proofvalues2 = { iDProof, pName, description, isEarned };
        dbManager.Insert("Proof", Proofvalues2);

        iDProof = "3";
        pName = "BDE";
        description = "Il y a de nombreuses SAE, toutes plus intéressantes les unes que les autres. Voici certains projets que vous pourrez avoir en 2 années de BUT : Programmation d’un PacMan. Production d’un escape game. Conception d’un jeu vidéo en rapport avec le BUT. Création d’un code pour visualiser les catastrophes naturelles en France. Création d’un réseau social inédit. ";
        isEarned = "T";
        string[] Proofvalues3 = { iDProof, pName, description, isEarned };
        dbManager.Insert("Proof", Proofvalues3);

        iDProof = "4";
        pName = "Liste des matières complémentaires";
        description = "Liste des matières complémentaires au BUT : Mathématiques Anglais Droit Gestion Management";
        isEarned = "T";
        string[] Proofvalues4 = { iDProof, pName, description, isEarned };
        dbManager.Insert("Proof", Proofvalues4);

        iDProof = "5";
        pName = "Liste des débouchés des anciens élèves";
        description = "Liste des débouchés des anciens élèves : Développeur Web Ingénieur en informatique Analyste de données Administrateur de bases de données Concepteur de jeux vidéo Designer d\'interfaces utilisateur Spécialiste en sécurité informatique Architecte logiciel Ingénieur réseau Consultant en technologies de l'information";
        isEarned = "T";
        string[] Proofvalues5 = { iDProof, pName, description, isEarned };
        dbManager.Insert("Proof", Proofvalues5);

        iDProof = "6";
        pName = "Liste des matières informatiques";
        description = "Liste des matières informatiques au BUT : Apprentissage du Python pour les bases de la programmation Exploration du langage C++ et Java pour la création de jeux et de logiciels Utilisation du SQL pour naviguer dans une base de données Programmation web avec PHP, HTML, CSS et JavaScript pour la création de sites web";
        isEarned = "T";
        string[] Proofvalues6 = { iDProof, pName, description, isEarned };
        dbManager.Insert("Proof", Proofvalues6);
    
        //initialisation de la position de Départ

        xPos = "-33.7";
        yPos = "-12";
        string[] pos = { xPos, yPos};
        dbManager.Insert("PlayerPos", pos);
    }
    
    void OnDestroy()
    {
        dbManager.CloseConnexion();
    }
}