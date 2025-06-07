using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float climbSpeed = 3f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Animator animator;
    private bool isGrounded;
    private bool isClimbing = false;
    private Rigidbody2D rb;
    private GameManager gameManager;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (gameManager.IsGameOver())
        {
            rb.velocity = Vector2.zero; // Stop player movement when game is over
            return;
        }
        Movement();
        Jump();
        Attack();
        UpdateAnimation();
        ClimbLadder();
    }

    private void Movement()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        if (moveInput > 0) transform.localScale = new Vector3(1, 1, 1);
        else if (moveInput < 0) transform.localScale = new Vector3(-1, 1, 1);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }

    private void UpdateAnimation()
    {
        bool IsRunning = Mathf.Abs(rb.velocity.x) > 0.1f;
        bool IsJumping = !isGrounded;
        animator.SetBool("IsRunning", IsRunning);
        animator.SetBool("IsJumping", IsJumping);
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            animator.SetTrigger("IsAttacking");
            // Add attack logic here, e.g., damage enemies
        }
    }

    private void ClimbLadder()
    {
        if (isClimbing)
        {
            float vertical = Input.GetAxis("Vertical");
            rb.velocity = new Vector2(rb.velocity.x, vertical * climbSpeed);
            rb.gravityScale = 0f;
            animator.SetBool("IsClimbing", Mathf.Abs(vertical) > 0.1f);
        }
        else
        {
            rb.gravityScale = 1f;
            animator.SetBool("IsClimbing", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isClimbing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isClimbing = false;
        }
    }
}
