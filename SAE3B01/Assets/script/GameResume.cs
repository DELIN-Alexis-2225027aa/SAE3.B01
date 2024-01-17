using UnityEngine.SceneManagement;
using UnityEngine;

/// <summary>
/// G�re la reprise du jeu en chargeant la sc�ne "MovingPhase" lorsque la touche �chap est press�e.
/// </summary>
public class GameResume : MonoBehaviour
{
    /// <summary>
    /// M�thode appel�e � chaque frame.
    /// </summary>
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MovingPhase");
        } 
    }
}
