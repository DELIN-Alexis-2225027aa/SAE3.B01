using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Contr�le l'opacit� d'une image en la faisant osciller entre deux valeurs.
/// </summary>
public class OpacityController : MonoBehaviour
{
    /// <summary>
    /// Opacit� initiale.
    /// </summary>
    public float opacity = 0.5f;

    /// <summary>
    /// Opacit� cible.
    /// </summary>
    public float opacityToReach = 0.1f;

    /// <summary>
    /// R�f�rence � l'objet Image.
    /// </summary>
    public Image image;

    /// <summary>
    /// Couleur de l'image.
    /// </summary>
    public Color color;

    /// <summary>
    /// M�thode appel�e au d�marrage du script.
    /// </summary>
    void Start()
    {
        // Enregistre la couleur initiale de l'image
        Color color = image.color;
    }

    /// <summary>
    /// M�thode appel�e � chaque mise � jour fixe.
    /// </summary>
    void FixedUpdate()
    {
        while (true)
        {
            // V�rifie si l'opacit� actuelle est �gale � l'opacit� cible
            if (opacityToReach == color.a)
            {
                // Si l'opacit� cible est 0.1, change-la � 1.0, sinon change-la � 0.1
                if (opacityToReach == 0.1f)
                {
                    opacityToReach = 1f;
                }
                else
                {
                    opacityToReach = 0.1f;
                }
            }

            // Si l'opacit� cible est inf�rieure � l'opacit� actuelle, diminue l'opacit�
            if (opacityToReach < color.a)
            {
                color.a -= 0.1f;
            }
            else // Sinon, augmente l'opacit�
            {
                color.a += 0.1f;
            }
        }
    }
}
