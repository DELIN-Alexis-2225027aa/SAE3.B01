using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;

/// <summary>
/// Gère les déclencheurs des salles de classe pour changer de scène.
/// </summary>
public class ClassroomsTriggers : MonoBehaviour
{
    [SerializeField] Transform EventStartSecondPhaseTrigger;
    [SerializeField] RectTransform eventStartSecondPhase;
    [SerializeField] private Collider2D myCollider;
    public string colName;
    public string classroomNumber;
    [SerializeField] private PosSaver posSaver;

    [SerializeField] public InventoryManager inventoryManager;
    [SerializeField] public ValluesConvertor valluesConvertor; 
    [SerializeField] public DBManager dbManager;

    private Vector3 savedPosEventStartSecondPhaseTrigger; 
    private Vector3 savedPosEventStartSecondPhase; 
    private Vector3 outPos; 

    private string[] proofsIsCollected;

    void Start()
{
    dbManager = new DBManager();
    inventoryManager = UnityEngine.Object.FindObjectOfType<InventoryManager>();
    valluesConvertor = new ValluesConvertor();

    savedPosEventStartSecondPhaseTrigger = EventStartSecondPhaseTrigger.localPosition;
    savedPosEventStartSecondPhase = eventStartSecondPhase.localPosition;
    outPos = new Vector3(1000f, 1000f, 0f);
    eventStartSecondPhase.localPosition = outPos;
    EventStartSecondPhaseTrigger.localPosition = outPos;
    myCollider = GetComponent<Collider2D>();

    proofsIsCollected = new string[6]; // Initialize the array here

    GetProofByID();
    checkIfPlayerHaveAllItem();
}

    /// <summary>
    /// Méthode appel à chaque frame.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && colName != null)
        {
            if (areAllCharactersDigits())
            {
                posSaver.SavePlayerPosition();
                SaveClassroomOnDB();
                TPClassroom();
            }else
            {
                if(classroomNumber.Equals("BDE") || classroomNumber.Equals("Mak"))
                {
                    posSaver.SavePlayerPosition();
                    SaveClassroomOnDB();
                    TPClassroom();
                }
                if(classroomNumber.Equals("Sta"))
                {
                    SceneManager.LoadScene("2ndPhaseDialogue");
                }
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        colName = collision.gameObject.name;
        classroomNumber = colName.Substring(0, 3);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        colName = null;
        classroomNumber = null;
    }

    void TPClassroom()
    {
        SceneManager.LoadScene("Classroom");
    }

    void SaveClassroomOnDB()
    {
        dbManager = new DBManager();
        dbManager.DeleteEverythingFromTable("Classroom");
        dbManager.InsertOneValue("Classroom", classroomNumber);
    }

    bool areAllCharactersDigits()
    {
        foreach (char character in classroomNumber)
        {
            if (!char.IsDigit(character))
            {
                // Si le caractère n'est pas un chiffre, retourne false
                return false;
            }
        }
        // Si tous les caractères sont des chiffres, retourne true
        return true;
    }

    void checkIfPlayerHaveAllItem()
    {
        bool isReadyToChangePhase = true;
        for (int i = 0; i < 5; ++i)
        {
            if (proofsIsCollected[i].Equals("F"))
            {
                isReadyToChangePhase = false;
            }
        }
        if (isReadyToChangePhase)
        {
            setupForTheSecondPhase();
        }
    }

    void setupForTheSecondPhase()
    {
        eventStartSecondPhase.localPosition = savedPosEventStartSecondPhase;
        EventStartSecondPhaseTrigger.localPosition = savedPosEventStartSecondPhaseTrigger;
    }



    public void GetProofByID()
    {
        List<List<object>> resultat = dbManager.Select("Proof", "isCollected", "1");

        for (int i = 0; i < 6; ++i)
        {
            List<object> row = resultat[i];
            proofsIsCollected[i]= valluesConvertor.convertRowToString(row);
        }
    }

}
