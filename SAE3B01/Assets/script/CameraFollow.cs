using UnityEngine;

public class SuiviCamera2D : MonoBehaviour
{
    public Transform objetASuivre;

    [SerializeField] private Camera cam;

    void Update()
    {

        if (Input.GetKey(KeyCode.Plus) || Input.GetKey(KeyCode.KeypadPlus) && cam.orthographicSize > 8)
        {
            cam.orthographicSize -= 3f * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Minus) || Input.GetKey(KeyCode.KeypadMinus) && cam.orthographicSize < 15)
        {
            cam.orthographicSize += 3f * Time.deltaTime;
        }

        // R�cupere la position actuel de la cam�ra
        Vector3 positionActuelle = transform.position;

        // Cr�er la nouvelle position de la cam�ra a partir de la position de l'objet a suivre
        Vector3 newPosition = new Vector3(objetASuivre.position.x, objetASuivre.position.y, positionActuelle.z);

        // D�place la cam�ra vers la nouvelle position
        transform.position = Vector3.Lerp(positionActuelle, newPosition, 1000f);


    }
}

