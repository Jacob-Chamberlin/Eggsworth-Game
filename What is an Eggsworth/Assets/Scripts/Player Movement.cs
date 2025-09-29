using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement :MonoBehaviour
{
    //horizontal movement
    public Rigidbody2D rb;
    public float moveSpeed = 5f;
    public bool isMoving;
    //public float moveSpeedCheck;
    float hMovement;
    bool facingR;
    //sprint
    public bool isSprinting;
    public float sprintSpeed = 10f;

    //jumping
    public float jumpPower = 10f;
    public float airJumpPower = 7f;
    public int maxJumps = 3;
    int jumpsRemaining;
    public bool canDJump;
    public bool isDJumping;
    public bool isJumping;

    //glide
    private bool canGlide = false;
    public float MaxGlideFallSpeed = 1f;
    public bool isGliding;

    //groundcheck
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.5f);
    public LayerMask groundLayer;
    public bool onGround;

    //gravity
    public float baseGravity = 2f;
    public float maxFallSpeed = 18f;
    public float maxFallSpeedCheck = 1f;
    public float fallSpeedMultiplier = 2f;

    //animation
    Animator animator;
    private SpriteRenderer sr;

    private void Start()
    {
        //checks 
        maxFallSpeedCheck = maxFallSpeed;
        //animator = GetComponent<Animator>();
        //sr = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (isSprinting == false)
        {
            rb.linearVelocity = new Vector2(hMovement * moveSpeed, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(hMovement * sprintSpeed, rb.linearVelocity.y);
        }
        if (hMovement == 1)
        {
            sr.flipX = true;
            isMoving = true;
            //animator.SetBool("isMoving", isMoving);
        }
        if (hMovement == -1)
        {
            sr.flipX = false;
            isMoving = true;
            //animator.SetBool("isMoving", isMoving);
        }
        if (hMovement == 0)
        {
            isMoving = false;
            animator.SetBool("isMoving", isMoving);
        }
        if (rb.linearVelocity.y <= 0)
        {
            isDJumping = false;
            //animator.SetBool("isDJumping", isDJumping);
        }

        GroundCheck();
        Gravity();
        //animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
        //animator.SetFloat("yVelocity", rb.velocity.y);
        //animator.SetBool("isGrounded", onGround);
    }

    public void Gravity()
    {
        if (rb.linearVelocity.y < 0)
        {
            rb.gravityScale = baseGravity * fallSpeedMultiplier; //fall faster over time
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Max(rb.linearVelocity.y, -maxFallSpeed));
        }
        else
        {
            rb.gravityScale = baseGravity;
        }
    }
    public void Move(InputAction.CallbackContext context)
    {
        hMovement = context.ReadValue<Vector2>().x;

    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (jumpsRemaining > 0)
        {
            isJumping = true;
            //animator.SetBool("isJumping", isJumping);
            if (context.performed && onGround == true)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
                jumpsRemaining--;

            }
            if (context.canceled && rb.linearVelocity.y > 0 && canDJump == false)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, y: 3);
            }
            if (context.performed && canDJump == true)
            {
                isDJumping = true;
                //animator.SetBool("isDJumping", isDJumping);
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, airJumpPower);
                jumpsRemaining--;
            }
            //if (context.canceled && canDJump == true)
            {

            }
        }
    }

    public void Glide(InputAction.CallbackContext context)
    {
        if (canGlide == true)
        {
            if (context.performed)
            {
                maxFallSpeed = MaxGlideFallSpeed;
                isGliding = true;
                //animator.SetBool("isGliding", isGliding);
            }
            if (context.canceled)
            {
                maxFallSpeed = maxFallSpeedCheck;
                isGliding = false;
                //animator.SetBool("isGliding", isGliding);

            }
        }
    }

    /*public void Sprint(InputAction.CallbackContext context)
    {
        if (context.performed && hMovement != 0)
        {
            isSprinting = true;
            animator.SetBool("isSprinting", isSprinting);
        }
        if (context.canceled)
        {
            isSprinting = false;
            animator.SetBool("isSprinting", isSprinting);
        }
    }
    */
    private void GroundCheck()
    {
        if (Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer))
        {
            jumpsRemaining = maxJumps;
            canGlide = false;
            if (maxFallSpeed != maxFallSpeedCheck)
            {
                maxFallSpeed = maxFallSpeedCheck;
            }
            onGround = true;
            canDJump = false;

        }
        else
        {
            if (jumpsRemaining == maxJumps)
            {
                jumpsRemaining--;
                //animator.SetBool("isJumping", isJumping);
            }
            onGround = false;
            canGlide = true;
            canDJump = true;
            isJumping = false;
            //animator.SetBool("isJumping", isJumping);
            //animator.SetBool("isGrounded", onGround);
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
    }
}
