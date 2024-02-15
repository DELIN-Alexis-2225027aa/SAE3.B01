using UnityEngine;

/// <summary>
/// Gère le suivi 2D d'un objet par la caméra.
/// </summary>
public class SuiviCamera2D : MonoBehaviour
{
    public Transform objetASuivre;

    [SerializeField] private Camera cam;

    void Update()
    {
        // Gestion du zoom de la caméra
        if (Input.GetKey(KeyCode.Plus) || Input.GetKey(KeyCode.KeypadPlus) && cam.orthographicSize > 8)
        {
            cam.orthographicSize -= 3f * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Minus) || Input.GetKey(KeyCode.KeypadMinus) && cam.orthographicSize < 15)
        {
            cam.orthographicSize += 3f * Time.deltaTime;
        }

        // Récupere la position actuel de la caméra
        Vector3 positionActuelle = transform.position;

        // Créer la nouvelle position de la caméra a partir de la position de l'objet a suivre
        Vector3 newPosition = new Vector3(objetASuivre.position.x, objetASuivre.position.y, positionActuelle.z);

        // Déplace la caméra vers la nouvelle position
        transform.position = Vector3.Lerp(positionActuelle, newPosition, 1000f);


    }
}

