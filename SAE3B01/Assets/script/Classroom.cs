using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClassroomsTriggers : MonoBehaviour
{
    public string nomDeLaNouvelleScene;

    private string colName;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && colName != null)
        {
            SceneManager.LoadScene(nomDeLaNouvelleScene);
        }
    }
}

