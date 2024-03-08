using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// G�re le comportement du curseur de volume de la musique.
/// </summary>
public class SliderMusic : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;  // R�f�rence au composant Slider pour ajuster le volume.

    private void Start()
    {
        // Si la cl� "musicVolume" n'existe pas dans les pr�f�rences du joueur, initialise-la � la valeur par d�faut (1) et charge le volume.
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
        {
            Load();  // Si la cl� existe, charge simplement le volume.
        }
    }

    /// <summary>
    /// Appel�e lorsqu'il y a un changement de valeur sur le curseur de volume.
    /// Ajuste le volume de l'AudioListener et enregistre la nouvelle valeur.
    /// </summary>
    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;  // Ajuste le volume global en fonction de la valeur du curseur.
        Save();  // Enregistre la nouvelle valeur du volume.
    }

    /// <summary>
    /// Charge la valeur du volume � partir des pr�f�rences du joueur.
    /// </summary>
    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");  // Charge la valeur du volume depuis les pr�f�rences.
    }

    /// <summary>
    /// Enregistre la valeur actuelle du volume dans les pr�f�rences du joueur.
    /// </summary>
    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);  // Enregistre la valeur du volume dans les pr�f�rences.
    }
}
