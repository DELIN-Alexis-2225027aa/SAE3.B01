using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Contrôle l'opacité d'une image en continu.
/// </summary>
public class OpacityController : MonoBehaviour
{
    public float opacity = 0.5f;
    public float opacityToReach = 0.1f;
    public Image image;
    public Color color;

    void Start()
    {
        // Initialisation de la couleur
        Color color = image.color;
    }

    void FixedUpdate()
    {
        // Boucle continue (Attention : potentiel boucle infinie)
        while (true)
        {
            // Vérifie si l'opacité à atteindre est égale à l'opacité actuelle
            if (opacityToReach == color.a)
            {
                // Si l'opacité à atteindre est 0.1, change-la à 1, sinon à 0.1
                if (opacityToReach == 0.1f)
                {
                    opacityToReach = 1f;
                }
                else
                {
                    opacityToReach = 0.1f;
                }
            }

            // Ajuste progressivement l'opacité vers l'opacité à atteindre
            if (opacityToReach < color.a)
            {
                color.a -= 0.1f;
            }
            else
            {
                color.a += 0.1f;
            }
        }
    }
}
