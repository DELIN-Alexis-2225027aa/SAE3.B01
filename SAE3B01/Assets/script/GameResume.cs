using UnityEngine.SceneManagement;
using UnityEngine;

/// <summary>
/// Gère la reprise du jeu en chargeant la scène "MovingPhase" lorsque la touche Échap est pressée.
/// </summary>
public class GameResume : MonoBehaviour
{
    /// <summary>
    /// Méthode appelée à chaque frame.
    /// </summary>
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MovingPhase");
        } 
    }
}
