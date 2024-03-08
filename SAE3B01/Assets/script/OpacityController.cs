using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Contrôle l'opacité d'une image en la faisant osciller entre deux valeurs.
/// </summary>
public class OpacityController : MonoBehaviour
{
    /// <summary>
    /// Opacité initiale.
    /// </summary>
    public float opacity = 0.5f;

    /// <summary>
    /// Opacité cible.
    /// </summary>
    public float opacityToReach = 0.1f;

    /// <summary>
    /// Référence à l'objet Image.
    /// </summary>
    public Image image;

    /// <summary>
    /// Couleur de l'image.
    /// </summary>
    public Color color;

    /// <summary>
    /// Méthode appelée au démarrage du script.
    /// </summary>
    void Start()
    {
        // Enregistre la couleur initiale de l'image
        Color color = image.color;
    }

    /// <summary>
    /// Méthode appelée à chaque mise à jour fixe.
    /// </summary>
    void FixedUpdate()
    {
        while (true)
        {
            // Vérifie si l'opacité actuelle est égale à l'opacité cible
            if (opacityToReach == color.a)
            {
                // Si l'opacité cible est 0.1, change-la à 1.0, sinon change-la à 0.1
                if (opacityToReach == 0.1f)
                {
                    opacityToReach = 1f;
                }
                else
                {
                    opacityToReach = 0.1f;
                }
            }

            // Si l'opacité cible est inférieure à l'opacité actuelle, diminue l'opacité
            if (opacityToReach < color.a)
            {
                color.a -= 0.1f;
            }
            else // Sinon, augmente l'opacité
            {
                color.a += 0.1f;
            }
        }
    }
}
