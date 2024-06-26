
using UnityEngine;

public class PlayerWaterInteraction : MonoBehaviour
{
    private bool isInWater = false;
    private Rigidbody2D playerRigidbody;
    private float originalGravityScale;
    private float swimGravityScale = 0.5f;
    private int waterCounter = 0; // Bộ đếm để theo dõi số lượng vùng nước

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        originalGravityScale = playerRigidbody.gravityScale;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            waterCounter++;
            isInWater = true;
            // Thay đổi gravity scale khi vào nước
            playerRigidbody.gravityScale = swimGravityScale;
            Debug.Log("Player entered water.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            waterCounter--;
            if (waterCounter <= 0)
            {
                isInWater = false;
                // Khôi phục gravity scale khi ra khỏi nước
                playerRigidbody.gravityScale = originalGravityScale;
                Debug.Log("Player exited water.");
            }
        }
    }

    // Cập nhật mỗi frame
    void Update()
    {
        // Kiểm tra nếu đang trong nước và người chơi nhấn nút bơi (ví dụ: Space), thì thực hiện bơi lên
        if (isInWater && Input.GetKey(KeyCode.Space))
        {
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 5f); // Điều chỉnh tốc độ bơi lên
        }
        else
        {
            // Đảm bảo trọng lực được khôi phục nếu không đang trong nước (kiểm tra an toàn)
            playerRigidbody.gravityScale = originalGravityScale;
        }
    }
}
