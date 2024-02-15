using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// G�re les d�clencheurs des salles de classe pour changer de sc�ne.
/// </summary>
public class ClassroomsTriggers : MonoBehaviour
{
    public string nomDeLaNouvelleScene;

    private string colName;

    /// <summary>
    /// M�thode appel�e � chaque frame.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && colName != null)
        {
            SceneManager.LoadScene(nomDeLaNouvelleScene);
        }
    }
}

