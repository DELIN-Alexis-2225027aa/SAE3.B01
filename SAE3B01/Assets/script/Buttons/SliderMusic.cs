using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Gère le comportement du curseur de volume de la musique.
/// </summary>
public class SliderMusic : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;  // Référence au composant Slider pour ajuster le volume.

    private void Start()
    {
        // Si la clé "musicVolume" n'existe pas dans les préférences du joueur, initialise-la à la valeur par défaut (1) et charge le volume.
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
        {
            Load();  // Si la clé existe, charge simplement le volume.
        }
    }

    /// <summary>
    /// Appelée lorsqu'il y a un changement de valeur sur le curseur de volume.
    /// Ajuste le volume de l'AudioListener et enregistre la nouvelle valeur.
    /// </summary>
    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;  // Ajuste le volume global en fonction de la valeur du curseur.
        Save();  // Enregistre la nouvelle valeur du volume.
    }

    /// <summary>
    /// Charge la valeur du volume à partir des préférences du joueur.
    /// </summary>
    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");  // Charge la valeur du volume depuis les préférences.
    }

    /// <summary>
    /// Enregistre la valeur actuelle du volume dans les préférences du joueur.
    /// </summary>
    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);  // Enregistre la valeur du volume dans les préférences.
    }
}
