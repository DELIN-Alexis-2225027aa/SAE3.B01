using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// G�re le t�l�porteur du joueur en fonction des collisions avec les escaliers.
/// </summary>
public class StairsTP : MonoBehaviour
{
    public float x;
    public float y;
    public string colName;

    /// <summary>
    /// M�thode appel�e lorsque le t�l�porteur entre en collision avec un objet.
    /// </summary>
    /// <param /name = //collision> Collider de l'objet en collision.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // D�termine la destination du t�l�porteur en fonction du nom de l'objet en collision
        colName = collision.gameObject.name;

        if (colName.Equals("LeftStairs1stFloor"))
        {
            x = -41.7f;
            y = -104.7f;
            TPPlayer();
        }
        if (colName.Equals("RightStairs1stFloor"))
        {
            x = 41.7f;
            y = -95.5f;
            TPPlayer();
        }
        if (colName.Equals("LeftStairs2ndFloorDown"))
        {
            x = -41.7f;
            y = -5f;
            TPPlayer();
        }
        if (colName.Equals("RightStairs2ndFloorDown"))
        {
            x = 42f;
            y = 4.6f;
            TPPlayer();
        }
        if (colName.Equals("LeftStairs2ndFloorUp"))
        {
            x = -41.5f;
            y = -203f;
            TPPlayer();
        }
        if (colName.Equals("RightStairs2ndFloorUp"))
        {
            x = 41.5f;
            y = -196f;
            TPPlayer();
        }
        if (colName.Equals("LeftStairs3rdFloorDown"))
        {
            x = -41.7f;
            y = -104.7f;
            TPPlayer();
        }
        if (colName.Equals("RightStairs3rdFloorDown"))
        {
            x = 41.7f;
            y = -95.5f;
            TPPlayer();
        }
    }

    /// <summary>
    /// T�l�porte le joueur � la position sp�cifi�e.
    /// </summary>
    private void TPPlayer()
    {
        Vector3 loadedPlayerPos = new Vector3(x, y, 0f);

        transform.position = loadedPlayerPos;
    }
}
