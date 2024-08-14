using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject player;
    private GameObject[] enemies; // Lưu trữ danh sách kẻ địch
    public float moveSpeed = 4.5f;
    public float minDistance = 2.0f;
    public float jumpHeight = 2.0f; // Chiều cao của cú bay lên khi chạm vào Ground
    private bool isFacingRight = true;
    public float distanceToEnemy = 10f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
