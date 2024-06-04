using UnityEngine;

public class PlayerClimbing : MonoBehaviour
{
    public float climbSpeed = 5f;
    private bool isClimbing = false;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Climbable"))
        {
            isClimbing = true;
            rb.gravityScale = 0; // Tắt trọng lực khi leo trèo
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Climbable"))
        {
            isClimbing = false;
            rb.gravityScale = 1; // Bật lại trọng lực khi ngừng leo
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
