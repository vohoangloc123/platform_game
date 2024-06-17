// using UnityEngine;

// public class FireDamage : MonoBehaviour
// {
//     public int damagePerSecond = 10; // Sát thương mỗi giây
//     public float durationInSeconds = 3f; // Thời gian sống khi trong vùng trigger
//     private EnemyHealth currentEnemyHealth; // Reference đến EnemyHealth hiện tại
//     private bool isDamaging = false; // Biến kiểm tra xem có gây sát thương hay không
//     private float elapsedTime = 0f; // Thời gian đã trôi qua

//     private void OnTriggerEnter2D(Collider2D collision)
//     {
//         // Kiểm tra xem đối tượng va chạm có phải là Enemy không
//         currentEnemyHealth = collision.gameObject.GetComponent<EnemyHealth>();

//         // Nếu đúng là Enemy và có component EnemyHealth
//         if (currentEnemyHealth != null)
//         {
//             // Bắt đầu gây sát thương
//             isDamaging = true;
//             elapsedTime = 0f; // Reset thời gian đã trôi qua
//             InvokeRepeating(nameof(DealDamage), 1f, 1f); // Gọi hàm DealDamage mỗi giây
//         }
//     }

//     private void OnTriggerExit2D(Collider2D collision)
//     {
//         // Kiểm tra xem đối tượng va chạm có phải là Enemy không
//         EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();

//         // Nếu đúng là Enemy và có component EnemyHealth
//         if (enemyHealth != null)
//         {
//             // Dừng gây sát thương
//             isDamaging = false;
//             CancelInvoke(nameof(DealDamage)); // Hủy gọi hàm DealDamage
//         }
//     }

//     private void DealDamage()
//     {
//         // Kiểm tra xem Enemy còn sống
//         if (currentEnemyHealth != null)
//         {
//             // Trừ sát thương mỗi giây
//             currentEnemyHealth.TakeDamage(damagePerSecond);

//             // Cập nhật thời gian đã trôi qua
//             elapsedTime += 1f;

//             // Kiểm tra nếu đã đủ thời gian sống
//             if (elapsedTime >= durationInSeconds)
//             {
//                 // Hủy gây sát thương và giết quái vật
//                 isDamaging = false;
//                 CancelInvoke(nameof(DealDamage));
//             }
//         }
//     }
// }

// using UnityEngine;

// public class FireDamage : MonoBehaviour
// {
//     public int damagePerSecond = 10; // Sát thương mỗi giây
//     private EnemyHealth currentEnemyHealth; // Reference đến EnemyHealth hiện tại
//     private bool isDamaging = false; // Biến kiểm tra xem có gây sát thương hay không
//     private float damageInterval = 1f; // Khoảng thời gian giữa các lần gây sát thương
//     private float damageTimer = 0f; // Bộ đếm thời gian để gây sát thương

//     private void OnTriggerEnter2D(Collider2D collision)
//     {
//         // Kiểm tra xem đối tượng va chạm có phải là Enemy không
//         currentEnemyHealth = collision.gameObject.GetComponent<EnemyHealth>();

//         // Nếu đúng là Enemy và có component EnemyHealth
//         if (currentEnemyHealth != null)
//         {
//             // Bắt đầu gây sát thương
//             isDamaging = true;
//         }
//     }

//     private void OnTriggerExit2D(Collider2D collision)
//     {
//         // Kiểm tra xem đối tượng va chạm có phải là Enemy không
//         EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();

//         // Nếu đúng là Enemy và có component EnemyHealth
//         if (enemyHealth != null && enemyHealth == currentEnemyHealth)
//         {
//             // Dừng gây sát thương
//             isDamaging = false;
//             currentEnemyHealth = null;
//         }
//     }

//     private void Update()
//     {
//         // Nếu đang gây sát thương và có Enemy trong vùng trigger
//         if (isDamaging && currentEnemyHealth != null)
//         {
//             damageTimer += Time.deltaTime;
//             if (damageTimer >= damageInterval)
//             {
//                 Debug.Log("Did dmg");
//                 currentEnemyHealth.TakeDamage(damagePerSecond);
//                 damageTimer = 0f; // Reset bộ đếm thời gian
//             }
//         }
//     }
// }

using UnityEngine;

public class FireDamage : MonoBehaviour
{
    public int damagePerSecond = 10; // Sát thương mỗi giây
    private EnemyHealth currentEnemyHealth; // Reference đến EnemyHealth hiện tại
    private bool isDamaging = false; // Biến kiểm tra xem có gây sát thương hay không
    private float damageInterval = 1f; // Khoảng thời gian giữa các lần gây sát thương
    private float damageTimer = 0f; // Bộ đếm thời gian để gây sát thương

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra xem đối tượng va chạm có phải là Enemy không
        currentEnemyHealth = collision.gameObject.GetComponent<EnemyHealth>();

        // Nếu đúng là Enemy và có component EnemyHealth
        if (currentEnemyHealth != null)
        {
            // Bắt đầu gây sát thương
            isDamaging = true;
            // Gây sát thương ngay lập tức
            currentEnemyHealth.TakeDamage(damagePerSecond);
            damageTimer = 0f; // Reset bộ đếm thời gian
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Kiểm tra xem đối tượng va chạm có phải là Enemy không
        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();

        // Nếu đúng là Enemy và có component EnemyHealth
        if (enemyHealth != null && enemyHealth == currentEnemyHealth)
        {
            // Dừng gây sát thương
            isDamaging = false;
            currentEnemyHealth = null;
        }
    }

    private void Update()
    {
        // Nếu đang gây sát thương và có Enemy trong vùng trigger
        if (isDamaging && currentEnemyHealth != null)
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= damageInterval)
            {
                currentEnemyHealth.TakeDamage(damagePerSecond);
                damageTimer = 0f; // Reset bộ đếm thời gian
            }
        }
    }
}
