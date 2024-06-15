// using UnityEngine;

// public class BabyDragonController : MonoBehaviour
// {
//     // public Transform player; // Tham chiếu đến GameObject của người chơi
//     // public float followDistance = 2.0f; // Khoảng cách mà pet muốn giữ với người chơi

//     // private Rigidbody2D rb;
//     // private Vector2 moveDirection;
//     // public float moveSpeed = 5f;

//     // void Start()
//     // {
//     //     rb = GetComponent<Rigidbody2D>();
//     //     moveDirection = Vector2.zero;
//     // }

//     // void Update()
//     // {
//     //     if (player != null)
//     //     {
//     //         // Tính toán vị trí mà pet muốn di chuyển đến
//     //         Vector2 targetPosition = player.position + (Vector3.Normalize(transform.position - player.position) * followDistance);

//     //         // Di chuyển pet đến vị trí targetPosition
//     //         moveDirection = (targetPosition - (Vector2)transform.position).normalized;
//     //         rb.velocity = moveDirection * moveSpeed;
//     //     }
//     // }
//     ////////////////////////////////////////////////////////////////////////
//     //   private Animator anim;
//     // private Rigidbody2D rb;
//     // private GameObject player;
//     // public float moveSpeed = 4.5f;
//     // public float minDistance = 2.0f;

//     // private bool isFacingRight = true;
//     // private Vector3 originalScale;
//     // private bool isMoving = false; // Thêm biến để theo dõi trạng thái di chuyển

//     // void Start()
//     // {
//     //     player = GameObject.FindGameObjectWithTag("Player");
//     //     anim = GetComponent<Animator>();
//     //     rb = GetComponent<Rigidbody2D>();
//     //     originalScale = transform.localScale;
//     // }

//     // void Update()
//     // {
//     //     if (player != null)
//     //     {
//     //         Vector3 direction = player.transform.position - transform.position;

//     //         if (direction.magnitude > minDistance)
//     //         {
//     //             direction.Normalize();
//     //             transform.Translate(direction * moveSpeed * Time.deltaTime);
//     //             Flip(direction.x);
//     //         }
//     //     }
//     // }

//     // void Flip(float directionX)
//     // {
//     //     if ((isFacingRight && directionX < 0) || (!isFacingRight && directionX > 0))
//     //     {
//     //         isFacingRight = !isFacingRight;
//     //         Vector3 scale = transform.localScale;
//     //         scale.x *= -1;
//     //         transform.localScale = scale;
//     //     }
//     // }
//     //////////////////////////////////////////////////////////////////////////////////////
//     private Rigidbody2D rb;
//     private GameObject player;
//     private GameObject[] enemies; // Lưu trữ danh sách kẻ địch
//     public float moveSpeed = 4.5f;
//     public float minDistance = 2.0f;

//     private bool isFacingRight = true;
//     private Vector3 originalScale;
//     private bool isMoving = false;

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

//         if (direction.magnitude > minDistance)
//         {
//             direction.Normalize();
//             transform.Translate(direction * moveSpeed * Time.deltaTime);
//             Flip(direction.x);
//         }
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


// // using UnityEngine;

// // public class BabyDragonController : MonoBehaviour
// // {
// //     public Transform player; // Tham chiếu đến GameObject của người chơi
// //     public float followDistance = 2.0f; // Khoảng cách mà pet muốn giữ với người chơi
// //     public string enemyTag = "Enemy"; // Tag của các quái vật
// //     public float enemyCheckDistance = 10f; // Khoảng cách để kiểm tra sự hiện diện của quái vật

// //     private Rigidbody2D rb;
// //     private Vector2 moveDirection;
// //     public float moveSpeed = 5f;
// //     private bool facingRight = true; // Biến để xác định hướng mặt của pet

// //     void Start()
// //     {
// //         rb = GetComponent<Rigidbody2D>();
// //         moveDirection = Vector2.zero;
// //     }

// //     void Update()
// //     {
// //         if (player != null)
// //         {
// //             // Tính toán vị trí mà pet muốn di chuyển đến
// //             Vector2 targetPosition = player.position + (Vector3.Normalize(transform.position - player.position) * followDistance);

// //             // Kiểm tra xem có quái vật trong khoảng cách kiểm tra không
// //             bool enemyNearby = Physics2D.OverlapCircle(transform.position, enemyCheckDistance, LayerMask.GetMask(enemyTag));

// //             if (enemyNearby)
// //             {
// //                 // Tìm đối tượng quái gần nhất trong khoảng cách
// //                 Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, enemyCheckDistance, LayerMask.GetMask(enemyTag));
// //                 Transform closestEnemy = null;
// //                 float closestDistance = Mathf.Infinity;

// //                 foreach (Collider2D collider in colliders)
// //                 {
// //                     float distance = Vector2.Distance(transform.position, collider.transform.position);
// //                     if (distance < closestDistance)
// //                     {
// //                         closestDistance = distance;
// //                         closestEnemy = collider.transform;
// //                     }
// //                 }

// //                 // Flip hướng pet để hướng về quái vật
// //                 if (closestEnemy != null)
// //                 {
// //                     if ((closestEnemy.position.x < transform.position.x && facingRight) || (closestEnemy.position.x > transform.position.x && !facingRight))
// //                     {
// //                         Flip();
// //                     }
// //                 }
// //             }
// //             else
// //             {
// //                 // Nếu không có quái vật gần, flip theo người chơi
// //                 float direction = player.position.x - transform.position.x;
// //                 if ((direction < 0 && facingRight) || (direction > 0 && !facingRight))
// //                 {
// //                     Flip();
// //                 }
// //             }

// //             // Di chuyển pet đến vị trí targetPosition
// //             moveDirection = (targetPosition - (Vector2)transform.position).normalized;
// //             rb.velocity = moveDirection * moveSpeed;
// //         }
// //     }

// //     void Flip()
// //     {
// //         // Đảo chiều của pet
// //         facingRight = !facingRight;
// //         Vector3 scale = transform.localScale;
// //         scale.x *= -1;
// //         transform.localScale = scale;
// //     }
// // }

// using UnityEngine;

// public class CharacterMovement : MonoBehaviour
// {
//     private Rigidbody2D rb;
//     private GameObject player;
//     private GameObject[] enemies; // Lưu trữ danh sách kẻ địch
//     public float moveSpeed = 4.5f;
//     public float minDistance = 2.0f;
//     public LayerMask groundLayer; // Layer của ground
//     public float groundDistance = 5f; // Khoảng cách cần giữ với ground

//     private bool isFacingRight = true;
//     private Vector3 originalScale;
//     private bool isMoving = false;

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
    public LayerMask groundLayer; // Layer của ground
    public LayerMask obstacleLayer; // Layer của chướng ngại vật
    public float groundDistance = 5f; // Khoảng cách cần giữ với ground
    public float obstacleDistance = 1f; // Khoảng cách kiểm tra chướng ngại vật
    public float climbHeight = 2f; // Chiều cao di chuyển lên để vượt qua chướng ngại vật

    private bool isFacingRight = true;
    private Vector3 originalScale;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale;
    }

    void Update()
    {
        FindEnemies(); // Tìm kẻ địch mỗi frame
        MoveTowardsTarget();
    }

    void FindEnemies()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    void MoveTowardsTarget()
    {
        if (enemies.Length > 0)
        {
            GameObject nearestEnemy = GetNearestEnemy();
            MoveTowards(nearestEnemy.transform.position);
        }
        else
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                MoveTowards(player.transform.position);
            }
        }
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

        // Kiểm tra khoảng cách với ground
        if (IsGroundTooClose())
        {
            // Nếu ground quá gần, không di chuyển xuống
            if (direction.y < 0)
            {
                direction.y = 0;
            }
        }

        // Kiểm tra chướng ngại vật phía trước
        if (IsObstacleAhead())
        {
            // Nếu có chướng ngại vật, di chuyển lên
            direction.y = climbHeight;
        }

        if (direction.magnitude > minDistance)
        {
            direction.Normalize();
            transform.Translate(direction * moveSpeed * Time.deltaTime);
            Flip(direction.x);
        }
    }

    bool IsGroundTooClose()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundDistance, groundLayer);
        return hit.collider != null;
    }

    bool IsObstacleAhead()
    {
        Vector2 direction = isFacingRight ? Vector2.right : Vector2.left;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, obstacleDistance, obstacleLayer);
        return hit.collider != null;
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
