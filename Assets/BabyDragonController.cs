
// using UnityEngine;

// public class CharacterMovement : MonoBehaviour
// {
//     private Rigidbody2D rb;
//     private GameObject player;
//     private GameObject[] enemies; // Lưu trữ danh sách kẻ địch
//     public float moveSpeed = 4.5f;
//     public float minDistance = 2.0f;
//     public LayerMask groundLayer; // Layer của ground
//     public LayerMask obstacleLayer; // Layer của chướng ngại vật
//     public float groundDistance = 5f; // Khoảng cách cần giữ với ground
//     public float obstacleDistance = 1f; // Khoảng cách kiểm tra chướng ngại vật
//     public float climbHeight = 2f; // Chiều cao di chuyển lên để vượt qua chướng ngại vật

//     private bool isFacingRight = true;
//     private Vector3 originalScale;

//     void Start()
//     {
//         rb = GetComponent<Rigidbody2D>();
//         originalScale = transform.localScale;
//     }

//     void Update()
//     {
//         FindEnemies(); // Tìm kẻ địch mỗi frame
//         MoveTowardsTarget();
//     }

//     void FindEnemies()
//     {
//         enemies = GameObject.FindGameObjectsWithTag("Enemy");
//     }

//     void MoveTowardsTarget()
//     {
//         if (enemies.Length > 0)
//         {
//             GameObject nearestEnemy = GetNearestEnemy();
//             MoveTowards(nearestEnemy.transform.position);
//         }
//         else
//         {
//             player = GameObject.FindGameObjectWithTag("Player");
//             if (player != null)
//             {
//                 MoveTowards(player.transform.position);
//             }
//         }
//     }

//     GameObject GetNearestEnemy()
//     {
//         GameObject nearestEnemy = null;
//         float closestDistance = Mathf.Infinity;

//         foreach (GameObject enemy in enemies)
//         {
//             float distance = Vector3.Distance(transform.position, enemy.transform.position);
//             if (distance < closestDistance)
//             {
//                 closestDistance = distance;
//                 nearestEnemy = enemy;
//             }
//         }

//         return nearestEnemy;
//     }

//     void MoveTowards(Vector3 targetPosition)
//     {
//         Vector3 direction = targetPosition - transform.position;

//         // Kiểm tra khoảng cách với ground
//         if (IsGroundTooClose())
//         {
//             // Nếu ground quá gần, không di chuyển xuống
//             if (direction.y < 0)
//             {
//                 direction.y = 0;
//             }
//         }

//         // Kiểm tra chướng ngại vật phía trước
//         if (IsObstacleAhead())
//         {
//             // Nếu có chướng ngại vật, di chuyển lên
//             direction.y = climbHeight;
//         }

//         if (direction.magnitude > minDistance)
//         {
//             direction.Normalize();
//             transform.Translate(direction * moveSpeed * Time.deltaTime);
//             Flip(direction.x);
//         }
//     }

//     bool IsGroundTooClose()
//     {
//         RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundDistance, groundLayer);
//         return hit.collider != null;
//     }

//     bool IsObstacleAhead()
//     {
//         Vector2 direction = isFacingRight ? Vector2.right : Vector2.left;
//         RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, obstacleDistance, obstacleLayer);
//         return hit.collider != null;
//     }

//     void Flip(float directionX)
//     {
//         if ((isFacingRight && directionX < 0) || (!isFacingRight && directionX > 0))
//         {
//             isFacingRight = !isFacingRight;
//             Vector3 scale = transform.localScale;
//             scale.x *= -1;
//             transform.localScale = scale;
//         }
//     }
// }

using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject player;
    private GameObject[] enemies; // Lưu trữ danh sách kẻ địch
    public float moveSpeed = 4.5f;
    public float minDistance = 2.0f;
    public float jumpHeight = 2.0f; // Chiều cao của cú bay lên khi chạm vào Ground
    public Transform groundCheck;
    private bool isGrounded = false;
    private bool isFacingRight = true;
    private Vector3 originalScale;
    private Vector2 boxSize = new Vector2(1f, 0.2f);
    public LayerMask groundLayer;
    public float distanceToEnemy = 10f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale;
    }

    void Update()
    {
        FindEnemies(); // Tìm kẻ địch mỗi frame
        MoveTowardsTarget();
        isGrounded = Physics2D.OverlapBox(groundCheck.position, boxSize, 0f, groundLayer);

        if (isGrounded)
        {
            FlyUp();
        }
    }

    void FindEnemies()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }
    // void MoveTowardsTarget()
    // {
    //     if (enemies.Length > 0)
    //     {
    //         GameObject nearestEnemy = GetNearestEnemy();
    //         MoveTowards(nearestEnemy.transform.position);
    //     }
    //     else
    //     {
    //        player = GameObject.FindGameObjectWithTag("Player");
    //         if (player != null)
    //         {
    //             MoveTowards(player.transform.position);
    //         }
    //     }
    // }
    void MoveTowardsTarget()
    {
        GameObject nearestTarget = null;
        float closestDistance = Mathf.Infinity;

        // Check enemies
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance && distance <= distanceToEnemy)
            {
                closestDistance = distance;
                nearestTarget = enemy;
            }
        }

        // Check player if no valid enemy found
        if (nearestTarget == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
                if (distanceToPlayer <= distanceToEnemy)
                {
                    nearestTarget = player;
                }
            }
        }

        // Move towards nearest target
        if (nearestTarget != null)
        {
            MoveTowards(nearestTarget.transform.position);
        }
    }

    private void FlyUp()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y + jumpHeight);
    }

    GameObject GetNearestEnemy()
    {
        GameObject nearestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
    }
    void MoveTowards(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;

        if (direction.magnitude > minDistance)
        {
            direction.Normalize();
            transform.Translate(direction * moveSpeed * Time.deltaTime);
            Flip(direction.x);
        }
    }

    void Flip(float directionX)
    {
        if ((isFacingRight && directionX < 0) || (!isFacingRight && directionX > 0))
        {
            isFacingRight = !isFacingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
}
