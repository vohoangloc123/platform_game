
using System.Collections;
using UnityEngine;

public class DashMovementPlayer : MonoBehaviour
// , IDataPersistence
{
    private float horizontal;
    public float speed = 5f;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private TrailRenderer tr;
    //animation
    private Animator anim;
    public bool jump = false;
    //new
    public bool flippedLeft;
    public bool facingRight = true;
    public AudioSource audioSource;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (isDashing)
        {
            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal");
        anim.SetFloat("run", Mathf.Abs(horizontal));
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            
        }
        if(!IsGrounded())
        {
            jump = true;
            anim.SetBool("jump", jump);
        }
        else if(IsGrounded())
        {
            jump = false;
            anim.SetBool("jump", jump);
        }
       
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            audioSource.Play();
            StartCoroutine(Dash());
        }
        //new
        if(horizontal <0)
        {
            // spriteRenderer.flipX = true;
            facingRight = false;
            // Flip(false);
        }
        else if(horizontal >0)
        {
            facingRight = true;
        }
        Flip();
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        // Kiểm tra xem nhân vật có tiếp xúc với mặt đất không
        bool grounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        // Kiểm tra xem nhân vật có tiếp xúc với enemy không
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.2f, enemyLayer);

        // Nếu tiếp xúc với enemy, cho phép nhảy
        if (hit.collider != null)
        {
            grounded = true;
        }

        return grounded;
    }
    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
    
}
