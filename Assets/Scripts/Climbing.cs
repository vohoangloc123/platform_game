
using UnityEngine;

public class PlayerClimbing : MonoBehaviour
{
    public float climbSpeed = 5f;
    private bool isClimbing = false;
    private Rigidbody2D rb;
    private float originalGravityScale;
    private int climbableCounter = 0; // Bộ đếm để theo dõi số lượng đối tượng có thể leo

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalGravityScale = rb.gravityScale; // Lưu trọng lực gốc
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Climbable"))
        {
            climbableCounter++;
            isClimbing = true;
            rb.gravityScale = 0; // Tắt trọng lực khi đang leo
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Climbable"))
        {
            climbableCounter--;
            if (climbableCounter <= 0)
            {
                isClimbing = false;
                rb.gravityScale = originalGravityScale; // Khôi phục trọng lực gốc khi ngừng leo
            }
        }
    }

    void Update()
    {
        if (isClimbing)
        {
            float vertical = Input.GetAxis("Vertical");
            rb.velocity = new Vector2(rb.velocity.x, vertical * climbSpeed);
        }
        else
        {
            // Đảm bảo trọng lực được khôi phục nếu không đang leo (kiểm tra an toàn)
            rb.gravityScale = originalGravityScale;
        }
    }
}
