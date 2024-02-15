using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    // D�finir la vitesse de d�placement de l'image
    public float speed = 5f;
    // Facteur d'�chelle pour augmenter la vitesse de l'image par rapport � la souris
    public float scale = 1.5f;

    // Stocker la diff�rence entre les coordonn�es de la souris avant et apr�s le lancement du programme
    private Vector3 mouseOffset;

    void Start()
    {
        // Obtenir la position initiale de la souris par rapport � l'image
        mouseOffset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void Update()
    {
        // Obtenir la position actuelle de la souris
        Vector3 mousePos = Input.mousePosition;
        // Convertir la position de la souris de l'�cran en position dans le monde
        Vector3 targetPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f)) + mouseOffset;

        // Calculer la diff�rence de position verticale entre l'image et la souris
        float verticalOffset = targetPos.y - transform.position.y;
        // Multiplier la diff�rence par le facteur d'�chelle
        float moveOffset = verticalOffset * scale;

        // D�placer l'image avec une vitesse bas�e sur le d�placement vertical de la souris multipli� par le facteur d'�chelle
        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y + moveOffset, transform.position.z), Time.deltaTime * speed);
    }
}