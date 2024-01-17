using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// G�re le suivi 2D d'un objet par la cam�ra dans la sc�ne de la carte.
/// </summary>
public class MapScript : MonoBehaviour
{
    public Transform objetASuivre;

    [SerializeField] private Camera cam;

    void Update()
    {
        // Gestion du zoom de la cam�ra.
        if (Input.GetKey(KeyCode.Plus) || Input.GetKey(KeyCode.KeypadPlus) && cam.orthographicSize > 2)
        {
            cam.orthographicSize -= 1f * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Minus) || Input.GetKey(KeyCode.KeypadMinus) && cam.orthographicSize < 6)
        {
            cam.orthographicSize += 1f * Time.deltaTime;
        }

        // R�cupere la position actuel de la cam�ra
        Vector3 positionActuelle = transform.position;

        // Cr�er la nouvelle position de la cam�ra a partir de la position de l'objet a suivre
        Vector3 newPosition = new Vector3(objetASuivre.position.x, objetASuivre.position.y, positionActuelle.z);

        // D�place la cam�ra vers la nouvelle position
        transform.position = Vector3.Lerp(positionActuelle, newPosition, 1000f);


    }
}
