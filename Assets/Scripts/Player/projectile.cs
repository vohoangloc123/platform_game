// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class projectile : MonoBehaviour
// {
//     public Rigidbody2D projectileRb;
//     public float speed;

//     public float projectileLife;
//     public float projectileCount;

//     private DashMovementPlayer movementPlayer;
//     public int damageAmount;
//     public bool facingRight;
//     public AudioClip shotSound;
//     // Start is called before the first frame update
//     void Start()
//     {
//         projectileCount = projectileLife;
//         movementPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<DashMovementPlayer>();
//         facingRight = movementPlayer.facingRight;
//         if(!facingRight)
//         {
//             transform.rotation = Quaternion.Euler(0, 180, 0);
//         }
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         projectileCount -= Time.deltaTime;
//         if (projectileCount <= 0)
//         {
//             Destroy(gameObject);
//         }//sau khi bắn ra trong 1 khoảng thời gian thì fire ball biến mất
//     }

//     private void FixedUpdate()
//     {
//         if(facingRight)
//         {
//              projectileRb.velocity = new Vector2(speed, projectileRb.velocity.y);
//         }
//         else
//         {
//             projectileRb.velocity = new Vector2(-speed, projectileRb.velocity.y);
//         }
       

//     }

//     private void OnCollisionEnter2D(Collision2D collision)
//     {
        
//         if (collision.gameObject.CompareTag("Enemy"))
//         {
//             EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
//             if(enemyHealth !=null)
//             {
//                 AudioSource.PlayClipAtPoint(shotSound, transform.position); // Phát âm thanh tại vị trí hiện tại
//                 enemyHealth.TakeDamage(damageAmount);
//             }
//         }
//         else if(collision.gameObject.CompareTag("Boss"))
//         {
//             EnemyHealth bossHealth = collision.gameObject.GetComponent<EnemyHealth>();
//             if (bossHealth != null)
//             {
//                 bossHealth.TakeDamage(damageAmount);
//             }
//         }
//         else if(collision.gameObject.CompareTag("FinalBoss"))
//         {
//             EnemyHealth bossHealth = collision.gameObject.GetComponent<EnemyHealth>();
//             if (bossHealth != null)
//             {
//                 bossHealth.TakeDamage(damageAmount);
//             }
//         }
//         Destroy(gameObject);
//         //kiểm tra đối tượng va chạm và xóa bỏ đối tượng
//     }
// }
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public Rigidbody2D projectileRb;
    public float speed;

    public float projectileLife;
    public float projectileCount;

    private DashMovementPlayer movementPlayer;
    public int damageAmount;
    public bool facingRight;
    public AudioClip shotSound;
    // Start is called before the first frame update
    void Start()
    {
        projectileCount = projectileLife;
        movementPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<DashMovementPlayer>();
        facingRight = movementPlayer.facingRight;
        if (!facingRight)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        projectileCount -= Time.deltaTime;
        if (projectileCount <= 0)
        {
            Destroy(gameObject);
        }//sau khi bắn ra trong 1 khoảng thời gian thì fire ball biến mất
    }

    private void FixedUpdate()
    {
        if (facingRight)
        {
            projectileRb.velocity = new Vector2(speed, projectileRb.velocity.y);
        }
        else
        {
            projectileRb.velocity = new Vector2(-speed, projectileRb.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Boss") || collision.gameObject.CompareTag("FinalBoss"))
        {
             Debug.Log("Chạm vào enemy nên destroy");
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                AudioSource.PlayClipAtPoint(shotSound, transform.position); // Phát âm thanh tại vị trí hiện tại
                enemyHealth.TakeDamage(damageAmount);
            }
            Destroy(gameObject); // Xóa đối tượng projectile sau khi gây damage
        }
    }
     private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") ) // Nếu viên đạn chưa va chạm với một đối tượng "Enemy"
        {
            Debug.Log("Không chạm vào enemy nên destroy");
            Destroy(gameObject); // Tiêu hủy đối tượng projectile khi va chạm với bất kỳ đối tượng nào khác
        }else if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Boss") || collision.gameObject.CompareTag("FinalBoss"))
        {
             Debug.Log("Chạm vào enemy nên destroy");
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                AudioSource.PlayClipAtPoint(shotSound, transform.position); // Phát âm thanh tại vị trí hiện tại
                enemyHealth.TakeDamage(damageAmount);
            }
            Destroy(gameObject); // Xóa đối tượng projectile sau khi gây damage
        }
    }
}
