using UnityEngine;

public class MoveImage : MonoBehaviour
{
    public float speed = 1.0f; // Vitesse de déplacement
    public float magnitude = 1.0f; // Amplitude du mouvement
    public float frequency = 1.0f; // Fréquence du mouvement

    private float initialY; // Position initiale en Y

    void Start()
    {
        // Enregistre la position initiale en Y
        initialY = transform.position.y;
    }

    void Update()
    {
        // Calcule le décalage en Y en utilisant la fonction sinus pour créer un mouvement oscillant
        float offsetY = Mathf.Sin(Time.time * frequency) * magnitude;

        // Calcule la position en Y en ajoutant le décalage à la position initiale
        float newY = initialY + offsetY;

        // Déplace l'objet à la nouvelle position en Y
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        // Pour déplacer l'objet selon l'axe X
        // transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
}
