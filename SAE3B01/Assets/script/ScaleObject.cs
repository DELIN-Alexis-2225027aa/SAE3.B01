using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Redimensionne un objet en utilisant un tiers de la largeur de l'�cran.
/// </summary>
public class ScaleObject : MonoBehaviour
{
    /// <summary>
    /// R�f�rence au RectTransform de l'objet.
    /// </summary>
    [SerializeField] private RectTransform m_RectTransform;

    /// <summary>
    /// M�thode appel�e au d�marrage du script.
    /// </summary>
    void Start()
    {
        // R�cup�re la largeur de l'�cran
        float largeurEcran = Screen.width;

        // Calcule le tiers de la largeur de l'�cran
        float tiersDeLaLargeurEcran = largeurEcran / 1.5f;

        // D�finit la nouvelle taille du RectTransform en utilisant le tiers de la largeur de l'�cran
        m_RectTransform.sizeDelta = new Vector2(tiersDeLaLargeurEcran, m_RectTransform.sizeDelta.y);
    }
}
