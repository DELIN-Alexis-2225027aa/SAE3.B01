using UnityEngine.SceneManagement;
using UnityEngine;

public class GameResume : MonoBehaviour
{

    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MovingPhase");
        } 
    }
}
