using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Gère les déclencheurs des salles de classe pour changer de scène.
/// </summary>
public class ClassroomsTriggers : MonoBehaviour
{
    public string nomDeLaNouvelleScene;

    private string colName;

    /// <summary>
    /// Méthode appelée à chaque frame.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && colName != null)
        {
            SceneManager.LoadScene(nomDeLaNouvelleScene);
        }
    }
}

