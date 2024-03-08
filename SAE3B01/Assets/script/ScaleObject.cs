using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Redimensionne un objet RectTransform pour occuper un tiers de la largeur de l'�cran.
/// </summary>
public class ScaleObject : MonoBehaviour
{
    [SerializeField] private RectTransform m_RectTransform;

    void Start()
    {
        // R�cup�re la largeur de l'�cran
        float screenWidth = Screen.width;

        // Calcule un tiers de la largeur de l'�cran
        float thirdOfScreenWidth = screenWidth / 1.5f;

        // Ajuste la taille de l'objet RectTransform en fonction du tiers de la largeur de l'�cran
        m_RectTransform.sizeDelta = new Vector2(thirdOfScreenWidth, m_RectTransform.sizeDelta.y);
    }
}
