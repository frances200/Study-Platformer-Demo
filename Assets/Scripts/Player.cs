using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float movementSpeed = 3f;
    public float jumpForce = 5f;
    public float groundCheckRadius = 0.2f;
    public Transform groundCheck;
    public LayerMask groundMask;
    
    private bool isGrounded;
    private Rigidbody2D rBody2D;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rBody2D.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * movementSpeed, rBody2D.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) 
        {
            rBody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        isGrounded =  Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundMask);
    }
}
