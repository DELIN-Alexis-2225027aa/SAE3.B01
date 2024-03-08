using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestSQLite : MonoBehaviour
{
    private DBManager dbManager;

        string iD;
        string name;
        string posID;
        string firstTime;

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

        string[] columns7 = { "iD", "name", "posID", "dialogue" };
        string[] types7 = { "INTEGER", "BLOB", "BLOB", "BLOB"};
        dbManager.CreateTable("DialoguesRepetition", columns7, types7);

        string[] columns8 = { "iD", "name", "posID", "dialogue" };
        string[] types8 = { "INTEGER", "BLOB", "BLOB", "BLOB"};
        dbManager.CreateTable("Questions", columns8, types8);

        string[] columns9 = { "iD", "name", "posID", "dialogue" };
        string[] types9 = { "INTEGER", "BLOB", "BLOB", "BLOB"};
        dbManager.CreateTable("Answers", columns9, types9);
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
        dbManager.Droptable("Questions");
        dbManager.Droptable("Answers");
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
        dbManager.DeleteEverythingFromTable("Questions");
        dbManager.DeleteEverythingFromTable("Answers");

    }

    public void initTables()
    {
        dbManager = new DBManager();

        iD = "1";
        name = "MAKSSOUD|MAKSSOUD";
        posID = "1,2";
        string dialogue = "Bonjour $ ! Je suis ravie que tu te sois porté volontaire pour la JPO. Je compte sur toi pour convaincre un maximum de lycéens de venir dans notre établissement l’année prochaine.|J’ai quelques consignes à te donner mais elles sont sur mon bureau, rejoins moi plus tard, et appuie sur “ E ” pour entrer.";
        firstTime = "T";
        string[] values = { iD, name, posID, dialogue, firstTime};
        dbManager.Insert("Dialogues", values);

        iD = "2";
        name = "PAPIER|PAPIER|PAPIER";
        posID = "1,1,1";
        string dialogue2 = "La réunion avec les lycéens va bientôt commencer!|Je suis désolé, je n’ai pas réussi à finaliser le livret d’informations à temps, il va falloir que tu t’en charges.|J’ai prévenu tes professeurs.|Rends toi dans toutes les salles du BUT qui sont ouvertes et n’oublie pas, pour voir le plan des salles, appuie sur “ , ”. ";
        firstTime = "T";
        string[] values2 = { iD, name, posID, dialogue2, firstTime };
        dbManager.Insert("Dialogues", values2);

        iD = "3";
        name = "NEUVOT|NEUVOT|NEUVOT|.";
        posID = "2,1,2,1";
        string dialogue3 = "Bien le bonjour $, je t’attendais pour te remettre la liste des SAÉs réalisées cette année !|Si tu ne sais pas où tu l’as rangée, il te suffit d’appuyer sur “ echap ” pour la consulter !|Eh ! Je compte sur toi pour leur donner envie aux p’tits jeunes !| Liste des SAÉ obtenue";
        firstTime = "T";
        string[] values3 = { iD, name, posID, dialogue3, firstTime };
        dbManager.Insert("Dialogues", values3);

        iD = "4";
        name = "$|.";
        posID = "1,1";
        string dialogue4 = "Ah oui, il ne faut pas que j’oublie de parler des matières complémentaires !|Liste des matières complémentaires obtenue";
        firstTime = "T";
        string[] values4 = { iD, name, posID, dialogue4, firstTime};
        dbManager.Insert("Dialogues", values4);
        
        iD = "5";
        name = "$|.";
        posID = "1,1";
        string dialogue5 = "Ça, j’le connais par cœur, mais bon, avec le trac, j’préfère l’avoir avec moi.|Liste des matières informatiques obtenue";
        firstTime = "T";
        string[] values5 = { iD, name, posID, dialogue5, firstTime };
        dbManager.Insert("Dialogues", values5);

        iD = "6";
        name = "MAKSSOUD|MAKSSOUD|.";
        posID = "1,2,1";
        string dialogue6 = "Ah ! $ tu tombes bien ! J’ai oublié de te dire, ce serait peut être bien de parler des débouchés durant la réunion.|Tiens, je te remets ce document, c’est la liste des métiers exercés par nos anciens élèves, ça devrait t’aider pour répondre aux questions des lycéens.|Liste des débouchés des anciens élèves obtenue";
        firstTime = "T";
        string[] values6 = { iD, name, posID, dialogue6, firstTime };
        dbManager.Insert("Dialogues", values6);

        iD = "7";
        name = "Myke|$|Myke|.";
        posID = "1,1,2,1";
        string dialogue7 = "Salut $, tu viens jouer à Smash Bros ?|Ah, non pas aujourd’hui Myke. Là, il faut vraiment que tu m’aides. T’aurais pas le programme des activités du BDE sous la main ?|Ouais bien sûr, pas de soucis. Et en plus, là on réfléchit à plein de nouveaux trucs.| Document des activités du BDE obtenu";
        firstTime = "T";
        string[] values7 = { iD, name, posID, dialogue7, firstTime };
        dbManager.Insert("Dialogues", values7);

        iD = "8";
        name = "Parrain|$|Parrain|.";
        posID = "1,1,1,1";
        string dialogue8 = "Salut le filleul ! Toi, t’as encore besoin d’un conseil de ton parrain préféré !|Eh ouais, j’voudrais des infos concernant les stages et l’alternance.|Ah oui, la cheffe de dep m’avait dit que tu en aurais besoin ! Tiens, je t’ai préparé ça.| Document stage et alternance obtenu";
        firstTime = "T";
        string[] values8 = { iD, name, posID, dialogue8, firstTime };
        dbManager.Insert("Dialogues", values8);

         iD = "12";
        name = "MAKSSOUD|MAKSSOUD";
        posID = "1|2";
        string dialogue17 = "Rends toi dans toutes les salles du bâtiment qui sont ouvertes.|Tu peux regarder les aides après avoir appuyé sur “ , ”, si tu oublies les commandes.";
        firstTime = "T";
        string[] values17 = { iD, name, posID, dialogue17, firstTime };
        dbManager.Insert("Dialogues", values17);

        iD = "13";
        name = "NEUVOT|NEUVOT";
        posID = "1,1";
        string dialogue18 = "Bah alors ? Fais vite !|Sinon tu vas être en retard pour la réunion.";
        firstTime = "T";
        string[] values18 = { iD, name, posID, dialogue18, firstTime };
        dbManager.Insert("Dialogues", values18);

        iD = "14";
        name = "$|$";
        posID = "1|1";
        string dialogue19 = "Ah ! Mais c'est bizarre ça.|Je n’avais pas déjà récupéré cette liste ?";
        firstTime = "T";
        string[] values19 = { iD, name, posID, dialogue19, firstTime };
        dbManager.Insert("Dialogues", values19);

        iD = "15";
        name = "$|$";
        posID = "1|1";
        string dialogue20 = "Ah ! Mais c'est bizarre ça.|Je n’avais pas déjà récupéré cette liste ?";
        firstTime = "T";
        string[] values20 = { iD, name, posID, dialogue20, firstTime };
        dbManager.Insert("Dialogues", values20);

        iD = "16";
        name = "MAKSSOUD|MAKSSOUD";
        posID = "1|1";
        string dialogue21 = "Je t’ai déjà tout donné, mais il te reste encore des informations à collecter.|À toi de jouer maintenant !";
        firstTime = "T";
        string[] values21 = { iD, name, posID, dialogue21, firstTime };
        dbManager.Insert("Dialogues", values21);

        iD = "17";
        name = "Myke|Myke";
        posID = "1|1";
        string dialogue22 = "Repasse me voir après ta réunion si tu veux.|Ça nous fait toujours plaisir !";
        firstTime = "T";
        string[] values22 = { iD, name, posID, dialogue22, firstTime };
        dbManager.Insert("Dialogues", values22);

        iD = "18";
        name = "Parrain|Parrain";
        posID = "1|1";
        string dialogue23 = "Si t’as besoin d’autres conseils, repasse me voir plus tard.|N'oublie pas, je suis là pour ça !";
        firstTime = "T";
        string[] values23 = { iD, name, posID, dialogue23, firstTime };
        dbManager.Insert("Dialogues", values23);

        iD = "101";
        name = "MAKSSOUD|MAKSSOUD";
        posID = "1,2";
        string dialogue9 = "Bonjour $ ! Je suis ravie que tu te sois portée volontaire pour la JPO. Je compte sur toi pour convaincre un maximum de lycéens de venir dans notre établissement l’année prochaine.|J’ai quelques consignes à te donner mais elles sont sur mon bureau, rejoins moi plus tard, et appuie sur “ E ” pour entrer.";
        firstTime = "T";
        string[] values9 = { iD, name, posID, dialogue9, firstTime};
        dbManager.Insert("Dialogues", values9);

        iD = "102";
        name = "PAPIER|PAPIER|PAPIER";
        posID = "1,1,1";
        string dialogue10 = "La réunion avec les lycéens va bientôt commencer!|Je suis désolé, je n’ai pas réussi à finaliser le livret d’informations à temps, il va falloir que tu t’en charges.|J’ai prévenu tes professeurs.|Rends toi dans toutes les salles du BUT qui sont ouvertes et n’oublie pas, pour voir le plan des salles, appuie sur “ , ”. ";
        firstTime = "T";
        string[] values10 = { iD, name, posID, dialogue10, firstTime };
        dbManager.Insert("Dialogues", values10);

        iD = "103";
        name = "NEUVOT|NEUVOT|NEUVOT|";
        posID = "2,1,2,1";
        string dialogue11 = "Bien le bonjour $, je t’attendais pour te remettre la liste des SAÉs réalisées cette année !|Si tu ne sais pas où tu l’as rangée, il te suffit d’appuyer sur “ echap ” pour la consulter !|Eh ! Je compte sur toi pour leur donner envie aux p’tits jeunes !| Liste des SAÉ obtenue";
        firstTime = "T";
        string[] values11 = { iD, name, posID, dialogue11, firstTime };
        dbManager.Insert("Dialogues", values11);

        iD = "104";
        name = "$|";
        posID = "1,1";
        string dialogue12 = "Ah oui, il ne faut pas que j’oublie de parler des matières complémentaires !|Liste des matières complémentaires obtenue";
        firstTime = "T";
        string[] values12 = { iD, name, posID, dialogue12, firstTime};
        dbManager.Insert("Dialogues", values12);
        
        iD = "105";
        name = "$|";
        posID = "1,1";
        string dialogue13 = "Ça, j’le connais par cœur, mais bon, avec le trac, j’préfère l’avoir avec moi.|Liste des matières informatiques obtenue";
        firstTime = "T";
        string[] values13 = { iD, name, posID, dialogue13, firstTime };
        dbManager.Insert("Dialogues", values13);

        iD = "106";
        name = "MAKSSOUD|MAKSSOUD|";
        posID = "1,2,1";
        string dialogue14 = "Ah ! $ tu tombes bien ! J’ai oublié de te dire, ce serait peut être bien de parler des débouchés durant la réunion.|Tiens, je te remets ce document, c’est la liste des métiers exercés par nos anciens élèves, ça devrait t’aider pour répondre aux questions des lycéens.|Liste des débouchés des anciens élèves obtenue";
        firstTime = "T";
        string[] values14 = { iD, name, posID, dialogue14, firstTime };
        dbManager.Insert("Dialogues", values14);

        iD = "107";
        name = "Myke|$|Myke|";
        posID = "1,1,2,1";
        string dialogue15 = "Salut $, tu viens jouer à Smash Bros ?|Ah, non pas aujourd’hui Myke. Là, il faut vraiment que tu m’aides. T’aurais pas le programme des activités du BDE sous la main ?|Ouais bien sûr, pas de soucis. Et en plus, là on réfléchit à plein de nouveaux trucs.| Document des activités du BDE obtenu";
        firstTime = "T";
        string[] values15 = { iD, name, posID, dialogue15, firstTime };
        dbManager.Insert("Dialogues", values15);

        iD = "108";
        name = "Parrain|$|Parrain|";
        posID = "1,1,1,1";
        string dialogue16 = "Salut la filleule ! Toi, t’as encore besoin d’un conseil de ton parrain préféré !|Et ouais, j’voudrais des infos concernant les stages et l’alternance.|Ah oui, la cheffe de dep m’avait dit que tu en aurais besoin ! Tiens, je t’ai préparé ça.| Document stage et alternance obtenu";
        firstTime = "T";
        string[] values16 = { iD, name, posID, dialogue16, firstTime };
        dbManager.Insert("Dialogues", values16);


        // initialisation des preuves

        iDProof = "1";
        pName = "Liste des SAE";
        description = "Il y a de nombreuses SAE, toutes plus intéressantes les unes que les autres. Voici certains projets que vous pourrez avoir en 2 années de BUT : Programmation d’un PacMan. Production d’un escape game. Conception d’un jeu vidéo en rapport avec le BUT. Création d’un code pour visualiser les catastrophes naturelles en France. Création d’un réseau social inédit. ";
        isEarned = "T";
        string[] Proofvalues = { iDProof, pName, description, isEarned};
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
        string[] Proofvalues3 = { iDProof, pName, description, isEarned};
        dbManager.Insert("Proof", Proofvalues3);

        iDProof = "4";
        pName = "Liste des matières complémentaires";
        description = "Liste des matières complémentaires au BUT : Mathématiques Anglais Droit Gestion Management";
        isEarned = "T";
        string[] Proofvalues4 = { iDProof, pName, description, isEarned};
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
        string[] Proofvalues6 = { iDProof, pName, description, isEarned};
        dbManager.Insert("Proof", Proofvalues6);
    
        //initialisation de la position de Départ

        xPos = "-33.7";
        yPos = "-12";
        string[] pos = { xPos, yPos};
        dbManager.Insert("PlayerPos", pos);

        //Initialisation des questions

        iD = "1";
        name = "Lycéen";
        posID = "1";
        string question = "Est-ce que le BUT c’est pareil que le lycée ? Ou bien, on apprend autre chose que du Python.";
        string[] questionValues = { iD, name, posID, question};
        dbManager.Insert("Questions", questionValues);

        iD = "2";
        name = "Lycéen";
        posID = "1";
        string question2 = "Après le BUT, dans quels secteurs d’activités peut-on travailler ?";
        string[] questionValues2 = { iD, name, posID, question2};
        dbManager.Insert("Questions", questionValues2);

        iD = "3";
        name = "Lycéen";
        posID = "1";
        string question3 = "Je vois qu’il y a beaucoup de possibilités après le BUT mais comment je vais savoir quel domaine je préfère ?";
        string[] questionValues3 = { iD, name, posID, question3};
        dbManager.Insert("Questions", questionValues3);

        iD = "4";
        name = "Lycéen";
        posID = "1";
        string question4 = "Comment se passe l’intégration des “première année” ?";
        string[] questionValues4 = { iD, name, posID, question4};
        dbManager.Insert("Questions", questionValues4);

        iD = "5";
        name = "Lycéen";
        posID = "1";
        string question5 = "Comment se déroulent les évaluations ?";
        string[] questionValues5 = { iD, name, posID, question5};
        dbManager.Insert("Questions", questionValues5);

        iD = "6";
        name = "Lycéen";
        posID = "1";
        string question6 = "J’ai entendu dire que l’anglais était indispensable dans l’informatique. Est-ce qu’on aura encore des cours de cette matière ?";
        string[] questionValues6 = { iD, name, posID, question6};
        dbManager.Insert("Questions", questionValues6);

        //Initialisation des réponses

        iD = "1";
        name = "$|$|$";
        posID = "1,1,1";
        string answer = "Non, vous apprendrez d’autres langages informatiques tels que le C++ et le Java pour la création de jeux et de logiciels|le SQL pour naviguer dans une base de données et le PHP, l’HTML, le CSS et le Javascript qui permettent la programmation de sites webs.|Mais je vous garantis que les bases du Python vous seront utiles durant votre apprentissage.";
        string[] answerValues = { iD, name, posID, answer};
        dbManager.Insert("Answers", answerValues);

        iD = "2";
        name = "$|$";
        posID = "1,1";
        string answer2 = "Les débouchés sont variés. Voici quelques exemples des métiers qu'ont exercés nos anciens élèves :|développeur web, ingénieur en informatique, analyste de données, administrateur de bases de données, concepteur de jeux vidéo, et designer d'interfaces utilisateur.";
        string[] answerValues2 = { iD, name, posID, answer2};
        dbManager.Insert("Answers", answerValues2);

        iD = "3";
        name = "$|$";
        posID = "1,1";
        string answer3 = "Dès la deuxième année, vous aurez le choix entre deux parcours : La cybersécurité ou le développement d’applications.|De plus, pour vous aider à choisir ce domaine, vous pourrez soit faire un stage à la fin de la deuxième année, soit une alternance dès le début.";
        string[] answerValues3 = { iD, name, posID, answer3};
        dbManager.Insert("Answers", answerValues3);

        iD = "4";
        name = "$|$";
        posID = "1,1";
        string answer4 = "Dès le début de votre année, le bureau des étudiants sera là pour vous. Vous aurez accès à une salle où vous pourrez discuter, jouer ou grignoter.|De plus, le BDE organise de nombreuses activités pour favoriser les rencontres entre étudiants.";
        string[] answerValues4 = { iD, name, posID, answer4};
        dbManager.Insert("Answers", answerValues4);

        iD = "5";
        name = "$|$|$";
        posID = "1,1,1";
        string answer5 = "Pour vous évaluer, le BUT met en place un système de contrôle continu ainsi que des SAE|qui consistent en des travaux de groupe sur plusieurs semaines et vous permettront d’approfondir vos connaissances.|Vous pourrez par exemple créer un Pac-Man, développer un réseau social, concevoir un escape game ou encore réaliser un jeu vidéo en lien avec le BUT.";
        string[] answerValues5 = { iD, name, posID, answer5};
        dbManager.Insert("Answers", answerValues5);

        iD = "6";
        name = "$|$|$";
        posID = "1,1,1";
        string answer6 = "L’informatique représente environ 50% des enseignements. Vous aurez également des cours de mathématiques qui vous serviront par exemple en cryptographie|mais également de droit, de gestion et de management, utiles en fonction du métier que vous choisirez.|Et effectivement les cours d’anglais sont dispensés à raison de 2 heures par semaine mais orientés davantage vers l’oral plutôt que l’écrit.";
        string[] answerValues6 = { iD, name, posID, answer6};
        dbManager.Insert("Answers", answerValues6);
    }
    
    void OnDestroy()
    {
        dbManager.CloseConnexion();
    }
}