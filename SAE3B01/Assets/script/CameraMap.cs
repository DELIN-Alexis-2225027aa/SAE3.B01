using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScript : MonoBehaviour
{
    public Transform objetASuivre;

    [SerializeField] private Camera cam;

    void Update()
    {

        if (Input.GetKey(KeyCode.Plus) || Input.GetKey(KeyCode.KeypadPlus) && cam.orthographicSize > 2)
        {
            cam.orthographicSize -= 1f * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Minus) || Input.GetKey(KeyCode.KeypadMinus) && cam.orthographicSize < 6)
        {
            cam.orthographicSize += 1f * Time.deltaTime;
        }

        // Récupere la position actuel de la caméra
        Vector3 positionActuelle = transform.position;

        // Créer la nouvelle position de la caméra a partir de la position de l'objet a suivre
        Vector3 newPosition = new Vector3(objetASuivre.position.x, objetASuivre.position.y, positionActuelle.z);

        // Déplace la caméra vers la nouvelle position
        transform.position = Vector3.Lerp(positionActuelle, newPosition, 1000f);


    }
}
