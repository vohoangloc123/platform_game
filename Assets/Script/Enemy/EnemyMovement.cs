
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 3f;
    public float followDistance = 40f;
    private GameObject player;
    private bool isFacingRight = true;
    private float left_right;
    private Animator anim;

    private float distanceToPlayer;


    private bool isGrounded = false;
    private bool isBlocking = false;
    // Kích thước của enemy (dùng để thực hiện Raycast)
    private Vector2 boxSize = new Vector2(1f, 0.2f);

    // LayerMask để chỉ xác định các lớp collision mà enemy phải xem xét
    public LayerMask groundLayer;
    public Transform groundCheck;
    public Transform blockingCheck;
    public float jumpForce = 5f;
    private Rigidbody2D rb;
    // Khoảng cách mà Raycast sẽ kiểm tra
    public float groundCheckDistance = 0.1f;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>(); 
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Kiểm tra xem enemy có đang đứng trên mặt đất hay không
        isGrounded = Physics2D.OverlapBox(groundCheck.position, boxSize, 0f, groundLayer);
        isBlocking = Physics2D.OverlapBox(blockingCheck.position, boxSize, 0f, groundLayer);
        if (player != null)
        {
            Vector3 direction = player.transform.position - transform.position;
            distanceToPlayer = direction.magnitude;

            if (distanceToPlayer <= followDistance)
            {
                transform.Translate(direction.normalized * speed * Time.deltaTime);
                left_right = direction.x;
                Flip();
                anim.SetFloat("run", Mathf.Abs(left_right));

                // Kiểm tra điều kiện để jump
                if (isGrounded&&isBlocking)
                {
                    Jump();
                }
            }
        }
    }

    void Jump()
    {
        // Thực hiện hành động jump ở đây
         rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }


    void Flip()
    {
        if ((isFacingRight && left_right < 0) || (!isFacingRight && left_right > 0))
        {
            isFacingRight = !isFacingRight;
            Vector3 kichThuoc = transform.localScale;
            kichThuoc.x = kichThuoc.x * -1;
            transform.localScale = kichThuoc;
        }
    }
}

