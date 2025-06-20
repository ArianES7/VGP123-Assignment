using System;
using UnityEngine;

public class PlayerMovement : Singelton<PlayerMovement>
{
   [SerializeField] public float moveSpeed = 5f;
    [SerializeField] public float sprintMultiplier = 1.5f;
    [SerializeField] public float jumpPower = 15f;

    public bool isGrounded = false;
    public bool isSprinting = false;
    private bool canDoubleJump = false;

    public Transform groundCheck;
    public LayerMask groundLayerMask;

    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer sr;

    private float moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       
        
    }

    void Update()
    {
        if(Time.timeScale == 0) return; // Pause check
        
        moveInput = Input.GetAxis("Horizontal");

        // Sprint check (only allowed if moving horizontally)
        isSprinting = Input.GetKey(KeyCode.LeftShift) && Mathf.Abs(moveInput) > 0.1f;

        FlipSprite();
        
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, .2f, groundLayerMask);


        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {

                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
                canDoubleJump = true;

                AudioManager.Instance.PlaySound("Jump");

            }
            else if (canDoubleJump)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
                canDoubleJump = false;

                AudioManager.Instance.PlaySound("Jump");

            }
            else
            {
                Debug.Log("No jumps available");
            }

            AudioManager.Instance.StopSound();
        }

        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetBool("isAttacking", true);
        }

        // Update grounded and animation states
        animator.SetBool("isGrounded", isGrounded);

        // Set speed for Blend Tree (0 = idle, 0.75 = walk, 1 = sprint)
        float animSpeed = isSprinting ? 1f : Mathf.Abs(moveInput) > 0.1f ? 0.75f : 0f;
        animator.SetFloat("Speed", animSpeed);
    }

    void FixedUpdate()
    {
        float currentSpeed = moveSpeed * (isSprinting ? sprintMultiplier : 1f);
        rb.linearVelocity = new Vector2(moveInput * currentSpeed, rb.linearVelocity.y);
    }

    void FlipSprite()
    {
        if (moveInput < 0)
            sr.flipX = true;
        else if (moveInput > 0)
            sr.flipX = false;
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            isGrounded = false;
            animator.SetBool("isJumping", true);
        }
    }
    
    public void EndAttack()
    {
        animator.SetBool("isAttacking", false);
    }
    
}
