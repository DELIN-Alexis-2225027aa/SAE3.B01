using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contr�leur du joueur.
/// </summary>
public class Player2DController : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    private float speed = 8f;

    [SerializeField] private Rigidbody2D rb;

    /// <summary>
    /// M�thode appel�e au d�marrage.
    /// </summary>
    void Start()
    {
        
    }

    /// <summary>
    /// M�thode appel�e � chaque frame.
    /// </summary>
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    /// <summary>
    /// M�thode appel�e � chaque intervalle fixe de temps.
    /// </summary>
    private void FixedUpdate()
    {
        // Applique une v�locit� au Rigidbody pour d�placer le joueur
        rb.velocity = new Vector3(horizontal * speed, vertical * speed);
    }
}
