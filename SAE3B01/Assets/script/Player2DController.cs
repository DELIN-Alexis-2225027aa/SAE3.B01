using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2DController : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    private float speed = 8f;

    [SerializeField] private Rigidbody2D rb;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(horizontal * speed, vertical * speed);
    }
}
