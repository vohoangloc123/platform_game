using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 2f;
    public Transform pointA;
    public Transform pointB;
    private bool movingToPointB = true;

    private Quaternion startRotation;
    private Quaternion endRotation;

    void Start()
    {
        startRotation = transform.rotation;
        endRotation = Quaternion.Euler(0f, 180f, 0f); // Xoay thuyền 180 độ theo trục Y
    }

    void Update()
    {
        Vector3 targetPosition = movingToPointB ? pointB.position : pointA.position;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Kiểm tra xem đã đến điểm cuối chưa
        if (transform.position == targetPosition)
        {
            if (movingToPointB)
            {
                movingToPointB = false;
                // Lật thuyền khi đến điểm cuối
                RotateBoat(endRotation);
            }
            else
            {
                movingToPointB = true;
                // Lật thuyền khi đến điểm cuối
                RotateBoat(startRotation);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
            // Reset quay của người chơi thành 0
            collision.transform.rotation = Quaternion.identity;
        }
    }

    // Hàm để xoay thuyền
    private void RotateBoat(Quaternion targetRotation)
    {
        transform.rotation = targetRotation; // Xoay thuyền ngay lập tức đến vị trí mong muốn
    }
}
