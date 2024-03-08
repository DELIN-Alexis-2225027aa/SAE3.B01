using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Redimensionne un objet RectTransform pour occuper un tiers de la largeur de l'écran.
/// </summary>
public class ScaleObject : MonoBehaviour
{
    [SerializeField] private RectTransform m_RectTransform;

    void Start()
    {
        // Récupère la largeur de l'écran
        float screenWidth = Screen.width;

        // Calcule un tiers de la largeur de l'écran
        float thirdOfScreenWidth = screenWidth / 1.5f;

        // Ajuste la taille de l'objet RectTransform en fonction du tiers de la largeur de l'écran
        m_RectTransform.sizeDelta = new Vector2(thirdOfScreenWidth, m_RectTransform.sizeDelta.y);
    }
}
