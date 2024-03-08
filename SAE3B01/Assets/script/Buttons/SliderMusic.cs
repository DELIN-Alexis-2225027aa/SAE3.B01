using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// G�re le curseur pour ajuster le volume de la musique.
/// </summary>
public class SliderMusic : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;

    /// <summary>
    /// M�thode appel�e au d�marrage.
    /// V�rifie si la cl� "musicVolume" existe dans les pr�f�rences du joueur.
    /// Si elle n'existe pas, initialise le volume � 1 et charge les pr�f�rences.
    /// Sinon, charge simplement les pr�f�rences existantes.
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
    /// Modifie le volume de l'�couteur audio en fonction de la valeur du curseur.
    /// Enregistre �galement la nouvelle valeur du volume.
    /// </summary>
    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    /// <summary>
    /// Charge la valeur du volume depuis les pr�f�rences et l'applique au curseur.
    /// </summary>
    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    /// <summary>
    /// Enregistre la valeur actuelle du curseur dans les pr�f�rences.
    /// </summary>
    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
}
