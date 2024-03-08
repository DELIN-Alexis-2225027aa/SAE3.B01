using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Redimensionne un objet en utilisant un tiers de la largeur de l'écran.
/// </summary>
public class ScaleObject : MonoBehaviour
{
    /// <summary>
    /// Référence au RectTransform de l'objet.
    /// </summary>
    [SerializeField] private RectTransform m_RectTransform;

    /// <summary>
    /// Méthode appelée au démarrage du script.
    /// </summary>
    void Start()
    {
        // Récupère la largeur de l'écran
        float largeurEcran = Screen.width;

        // Calcule le tiers de la largeur de l'écran
        float tiersDeLaLargeurEcran = largeurEcran / 1.5f;

        // Définit la nouvelle taille du RectTransform en utilisant le tiers de la largeur de l'écran
        m_RectTransform.sizeDelta = new Vector2(tiersDeLaLargeurEcran, m_RectTransform.sizeDelta.y);
    }
}
