using UnityEngine;

public class PlayerClimbing : MonoBehaviour
{
    public float climbSpeed = 5f;
    private bool isClimbing = false;
    private Rigidbody2D rb;
    private float originalGravityScale;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalGravityScale = rb.gravityScale; // Store the original gravity scale
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Climbable"))
        {
            isClimbing = true;
            rb.gravityScale = 0; // Turn off gravity when climbing
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Climbable"))
        {
            isClimbing = false;
            rb.gravityScale = originalGravityScale; // Restore original gravity when stopping climb
        }
    }

    void Update()
    {
        if (isClimbing)
        {
            float vertical = Input.GetAxis("Vertical");
            rb.velocity = new Vector2(rb.velocity.x, vertical * climbSpeed);
        }
    }
}
