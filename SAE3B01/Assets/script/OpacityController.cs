using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Contr�le l'opacit� d'une image en continu.
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
            // V�rifie si l'opacit� � atteindre est �gale � l'opacit� actuelle
            if (opacityToReach == color.a)
            {
                // Si l'opacit� � atteindre est 0.1, change-la � 1, sinon � 0.1
                if (opacityToReach == 0.1f)
                {
                    opacityToReach = 1f;
                }
                else
                {
                    opacityToReach = 0.1f;
                }
            }

            // Ajuste progressivement l'opacit� vers l'opacit� � atteindre
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
