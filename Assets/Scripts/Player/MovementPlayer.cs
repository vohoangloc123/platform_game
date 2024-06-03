
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{

    private Animator anim;
    public Rigidbody2D playerRb;
    public float speed;
    public float input;

    public SpriteRenderer spriteRenderer;

    public float jumpForce;

    public LayerMask groundLayer;
    private bool isGrounded;
    public Transform feetPosition;
    public float groundCheckCircle;

    public bool flippedLeft;
    public bool facingRight;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        input = Input.GetAxisRaw("Horizontal");
        anim.SetFloat("run", Mathf.Abs(input));
        if(input <0)
        {
            // spriteRenderer.flipX = true;
            facingRight = false;
            Flip(false);
        }
        else if(input >0)
        {
            facingRight = true;
            Flip(true);
            // spriteRenderer.flipX = false;
        }
        isGrounded = Physics2D.OverlapCircle(feetPosition.position, groundCheckCircle, groundLayer);
        
        if(isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            playerRb.velocity = Vector2.up * jumpForce;
            anim.SetBool("jump", true);
        }
        
    }
     void FixedUpdate()
    {
        playerRb.velocity=new Vector2(input * speed, playerRb.velocity.y);
    }
    void Flip(bool facingRight)
    {
        if(flippedLeft && facingRight)
        {
            transform.Rotate(0, -180, 0);
            flippedLeft = false;
        }
        if(!flippedLeft && !facingRight)
        {
            transform.Rotate(0, -180, 0);
            flippedLeft = true;
        }
    }
}


