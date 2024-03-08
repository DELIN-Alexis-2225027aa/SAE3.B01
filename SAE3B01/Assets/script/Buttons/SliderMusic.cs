using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Gère le curseur pour ajuster le volume de la musique.
/// </summary>
public class SliderMusic : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;

    /// <summary>
    /// Méthode appelée au démarrage.
    /// Vérifie si la clé "musicVolume" existe dans les préférences du joueur.
    /// Si elle n'existe pas, initialise le volume à 1 et charge les préférences.
    /// Sinon, charge simplement les préférences existantes.
    /// </summary>
    private void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
        {
            Load();
        }
    }

    /// <summary>
    /// Modifie le volume de l'écouteur audio en fonction de la valeur du curseur.
    /// Enregistre également la nouvelle valeur du volume.
    /// </summary>
    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    /// <summary>
    /// Charge la valeur du volume depuis les préférences et l'applique au curseur.
    /// </summary>
    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    /// <summary>
    /// Enregistre la valeur actuelle du curseur dans les préférences.
    /// </summary>
    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
}
