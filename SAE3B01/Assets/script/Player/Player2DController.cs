using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contreur du joueur.
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

    }



    /// <summary>
    /// Méthode appelée ・chaque frame.
    /// </summary>
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    /// <summary>
    /// Méthode appelée ・chaque intervalle fixe de temps.
    /// </summary>
    private void FixedUpdate()
    {
        // Applique une vélocit・au Rigidbody pour déplacer le joueur
        rb.velocity = new Vector3(horizontal * speed, vertical * speed);
    }
}
