﻿using System.Collections;
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
    [SerializeField] private Collider2D myCollider;
    public string colName;
    string filePath;
    public string classroomNumber;
    [SerializeField] private PosSaver posSaver;



    void Start()
    {
        filePath = Application.dataPath + "/SaveJson/classroom.json";
        myCollider = GetComponent<Collider2D>();
    }

    /// <summary>
    /// Méthode appel à chaque frame.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && colName != null)
        {
            posSaver.SavePlayerPosition();
            SaveClassroomOnJSON();
            TPClassroom();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        colName = collision.gameObject.name;
        classroomNumber = colName.Substring(0, 3);
    }

    void TPClassroom()
    {
        SceneManager.LoadScene("Classroom");
    }

    void SaveClassroomOnJSON()
    {
        Classroom classroom = new Classroom
        {
            classroomName = classroomNumber
        };

        // Convertir la classe en JSON et écrire dans le fichier
        string updatedJson = JsonUtility.ToJson(classroom);
        File.WriteAllText(filePath, updatedJson);

        Debug.Log("Classroom saved to file.");
    }
}
