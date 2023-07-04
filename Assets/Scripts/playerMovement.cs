using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public ParticleSystem dust;
    public AudioSource jumpSound;
    public Animator animator;

    public float speed;
    private float moveInput;
    private bool isGrounded;
    public Transform groundCheck;
    public LayerMask whatisGround;
    public float checkRadius;
    public bool canMove = true;
    public float knockback;
    public float jumpForce;
    private float jumpTimeCounter;
    public float jumpTime;
    public  bool isJumping;
    public static bool jumpAllowed;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpSound = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (jumpAllowed && canMove)
        {

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;

            jumpTimeCounter = jumpTime;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            createDust();
            jumpSound.Play();
        }

        if (Input.GetButton("Jump") && isJumping)
        {
            if(jumpTimeCounter>0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
                
            }
            else
            {
                isJumping = false;
                
            }
        }
        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }
        }

    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatisGround);
        if (canMove)
        {
            moveInput = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
            if (moveInput == 0)
            {
                animator.SetBool("isRunning", false);
            }
            else
            {
                animator.SetBool("isRunning", true);
            }

        }
        
        
    }

    void createDust()
    {
        dust.Play();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Enemy"))
        {
            var movingRight = collision.gameObject.GetComponent<Patrol>();
            canMove = false;
            if (movingRight.movingRight == true)
            {
                rb.AddForce(new Vector2(12,1) * knockback, ForceMode2D.Force);
                canMove = true;

            }
            else
            {

                rb.AddForce(new Vector2(-12, 1) * knockback, ForceMode2D.Force);
                canMove = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Finish")
        {
            SceneManager.LoadScene(2);
        }
    }
}
