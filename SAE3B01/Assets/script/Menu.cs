using UnityEngine;

/// <summary>
/// Déplace le GameObject selon un motif oscillant sinusoïdal.
/// </summary>
public class MoveImage : MonoBehaviour
{
    /// <summary>
    /// Vitesse du mouvement.
    /// </summary>
    public float vitesse = 1.0f;

    /// <summary>
    /// Amplitude du mouvement.
    /// </summary>
    public float amplitude = 1.0f;

    /// <summary>
    /// Fréquence du mouvement.
    /// </summary>
    public float frequence = 1.0f;

    private float positionInitialeY; // Position initiale sur l'axe Y

    void Start()
    {
        // Enregistre la position initiale sur l'axe Y
        positionInitialeY = transform.position.y;
    }

    void Update()
    {
        // Calcule le décalage en Y en utilisant la fonction sinus pour créer un mouvement oscillant
        float décalageY = Mathf.Sin(Time.time * frequence) * amplitude;

        // Calcule la nouvelle position en Y en ajoutant le décalage à la position initiale
        float nouvellePositionY = positionInitialeY + décalageY;

        // Déplace l'objet à la nouvelle position en Y
        transform.position = new Vector3(transform.position.x, nouvellePositionY, transform.position.z);

        // Pour déplacer l'objet le long de l'axe X
        // transform.position = new Vector3(nouvellePositionX, transform.position.y, transform.position.z);
    }
}
