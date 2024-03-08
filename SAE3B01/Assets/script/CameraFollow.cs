using UnityEngine;

/// <summary>
/// Gère le suivi 2D d'un objet par la caméra.
/// </summary>
public class SuiviCamera2D : MonoBehaviour
{
    /// <summary>
    /// Transform de l'objet à suivre.
    /// </summary>
    public Transform objetASuivre;

    /// <summary>
    /// Référence à la caméra.
    /// </summary>
    [SerializeField] private Camera cam;

    /// <summary>
    /// Méthode appelée à chaque frame.
    /// </summary>
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

        // Récupère la position actuelle de la caméra
        Vector3 positionActuelle = transform.position;

        // Crée la nouvelle position de la caméra à partir de la position de l'objet à suivre
        Vector3 newPosition = new Vector3(objetASuivre.position.x, objetASuivre.position.y, positionActuelle.z);

        // Déplace la caméra vers la nouvelle position avec l'effet de lissage
        transform.position = Vector3.Lerp(positionActuelle, newPosition, 1000f);
    }
}
