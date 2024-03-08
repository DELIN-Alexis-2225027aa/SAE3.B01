using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contrôleur du joueur en 2D.
/// </summary>
public class Player2DController : MonoBehaviour
{
    public float horizontal;
    public float vertical;
    private float speed = 8f;

    [SerializeField] private Rigidbody2D rb;

    /// <summary>
    /// Méthode appelée au démarrage.
    /// </summary>
    void Start()
    {
        // Aucune initialisation nécessaire pour le moment.
    }

    /// <summary>
    /// Méthode appelée à chaque frame.
    /// </summary>
    void Update()
    {
        // Récupère les axes horizontaux et verticaux de l'entrée du joueur.
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    /// <summary>
    /// Méthode appelée à chaque intervalle fixe de temps.
    /// </summary>
    private void FixedUpdate()
    {
        // Applique une vélocité au Rigidbody pour déplacer le joueur.
        rb.velocity = new Vector3(horizontal * speed, vertical * speed);
    }
}
