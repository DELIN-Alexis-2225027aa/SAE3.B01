using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

[System.Serializable]
public class Classroom
{
    public string className;
}


    /// <summary>
    /// Gère les déclencheurs des salles de classe pour changer de scène.
    /// </summary>
    public class ClassroomsTriggers : MonoBehaviour
    {
        [SerializeField] private Collider2D myCollider;
        public string colName;
        string filePath;
        public string classroomNumber;


        void Start()
        {
            filePath = Application.dataPath + "/SaveJson/classroom.json";
            myCollider = GetComponent<Collider2D>();
        }

        /// <summary>
        /// Méthode appelée à chaque frame.
        /// </summary>
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.E) && colName != null)
            {
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

            Classroom classroom = new Classroom
            {
                className = classroomNumber
            };

            // Convertir la classe en JSON et écrire dans le fichier
            string updatedJson = JsonUtility.ToJson(classroom);
            File.WriteAllText(filePath, updatedJson);

            SceneManager.LoadScene("Classroom");
        }
    }
