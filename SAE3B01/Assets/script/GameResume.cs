using UnityEngine.SceneManagement;
using UnityEngine;

/// <summary>
/// Gère la reprise du jeu en chargeant la sc(ne "MovingPhase" lorsque la touche ﾉchap est pressée.
/// </summary>
public class GameResume : MonoBehaviour
{
    /// <summary>
    /// Méthode appelée ・chaque frame.
    /// </summary>
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MovingPhase");
        } 
    }
}
