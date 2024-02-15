using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    // Définir la vitesse de déplacement de l'image
    public float speed = 5f;
    // Facteur d'échelle pour augmenter la vitesse de l'image par rapport à la souris
    public float scale = 1.5f;

    // Stocker la différence entre les coordonnées de la souris avant et après le lancement du programme
    private Vector3 mouseOffset;

    void Start()
    {
        // Obtenir la position initiale de la souris par rapport à l'image
        mouseOffset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void Update()
    {
        // Obtenir la position actuelle de la souris
        Vector3 mousePos = Input.mousePosition;
        // Convertir la position de la souris de l'écran en position dans le monde
        Vector3 targetPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f)) + mouseOffset;

        // Calculer la différence de position verticale entre l'image et la souris
        float verticalOffset = targetPos.y - transform.position.y;
        // Multiplier la différence par le facteur d'échelle
        float moveOffset = verticalOffset * scale;

        // Déplacer l'image avec une vitesse basée sur le déplacement vertical de la souris multiplié par le facteur d'échelle
        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y + moveOffset, transform.position.z), Time.deltaTime * speed);
    }
}