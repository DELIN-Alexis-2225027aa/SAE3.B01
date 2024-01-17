using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleObject : MonoBehaviour
{
    [SerializeField] private RectTransform m_RectTransform;

    void Start()
    {
        float screenWidth = Screen.width;
        float thirdOfScreenWidth = screenWidth / 1.5f;
        m_RectTransform.sizeDelta = new Vector2(thirdOfScreenWidth, m_RectTransform.sizeDelta.y);
    }
}
