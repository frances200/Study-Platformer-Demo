using System;
using NUnit.Framework.Constraints;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header(("Movement Settings"))] 
    public float moveSpeed = 8f;
    public float jumpForce = 16f;
    [Range(0, 1)] public float groundDamping = 0.1f;
    
    [Header("Game Feel Settings")]
    public float coyoteTime = 0.2f;
    public float jumpBufferTime = 0.2f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float gameOverWaitTime = 5f;
    
    [Header("Checks")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public LayerMask spikeLayer;

    private Rigidbody2D rb;
    private float horizontalInput;
    private bool isGrounded;
    public bool isGameOver = false;
    private float currentGameOverWaitTime;
    private bool facingRight = true;
    private Vector3 velocity = Vector3.zero;
    
    // Timers
    private float coyoteTimeCounter;
    private float jumpBufferCounter;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentGameOverWaitTime = gameOverWaitTime;
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        bool wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (jumpBufferCounter > 0f && coyoteTimeCounter > 0f)
        {
            PerformJump();
        }

        if (Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 1f);
            coyoteTimeCounter = 0f;
        }

        FlipCharacter();
    }

    private void FixedUpdate()
    {
        if (!isGameOver)
        {
            Move(horizontalInput);
        }
        ApplyBetterGravity();
        CheckDeath();
    }

    void Move(float move)
    {
        Vector3 targetVelocity = new Vector2(move * moveSpeed, rb.linearVelocity.y);
        rb.linearVelocity = Vector3.SmoothDamp(rb.linearVelocity, targetVelocity, ref velocity, groundDamping);
    }

    void PerformJump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        
        jumpBufferCounter = 0f;
    }

    void ApplyBetterGravity()
    {
        if (rb.linearVelocity.y < 0)
        {
            rb.gravityScale = fallMultiplier;
        } else if (rb.linearVelocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.gravityScale = lowJumpMultiplier;
        }
        else
        {
            rb.gravityScale = 1f;
        }
    }

    void FlipCharacter()
    {
        if (horizontalInput > 0 && !facingRight)
        {
            Flip();
        } else if (horizontalInput < 0 && facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void CheckDeath()
    {
        if (Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, spikeLayer))
        {
            isGameOver = true;
            currentGameOverWaitTime -= Time.deltaTime;
            if (currentGameOverWaitTime <= 0f)
            {
                transform.position = new Vector3(0, -2.5f, 1.6f);
                isGameOver = false;
            }
        }
        else
        {
            currentGameOverWaitTime = gameOverWaitTime;
            isGameOver = false;
        }
    }
}
