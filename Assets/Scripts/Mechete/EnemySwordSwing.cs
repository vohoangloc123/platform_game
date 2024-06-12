using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwordSwing : MonoBehaviour
{
    public string playerTag = "Player"; // Tag của đối tượng Player
    public Animator animator; // Animator của đối tượng
    public int damage = 1; // Sát thương của từng kẻ địch
    public float attackRange = 2f; // Phạm vi tấn công
    private bool isAttacking = false; 

    private GameObject player; // Lưu trữ đối tượng Player
      public float knockbackForce = 5f; // Lực knockback
    public float cooldown = 1;
    public AudioSource bladeSound;
    void Start()
    {
        // Tìm đối tượng Player một lần khi bắt đầu
        player = GameObject.FindGameObjectWithTag(playerTag);
    }

    void Update()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance <= attackRange)
            {
                // Nếu ở gần địch, thực hiện tấn công
                 StartCoroutine(AttackWithCooldown());
            }
        }
    }
  private IEnumerator AttackWithCooldown()
    {
        isAttacking = true;
        Attack();
        yield return new WaitForSeconds(cooldown);
        isAttacking = false;
    }
    void Attack()
    {
        // Thực hiện hành động tấn công

        // Kích hoạt Animator để thực hiện Animation Attack
        if (animator != null)
        {
            PlaySound();
            animator.SetTrigger("Attack");
        }
        // Gọi hàm tấn công của PlayerHealth
    }
     private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            // Gọi hàm tấn công của PlayerHealth
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            // Thêm hiệu ứng knockback
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;
                playerRb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
            }
        }
    }
     public void PlaySound()
    {
        if(bladeSound != null)
        {
            // Phát âm thanh
            bladeSound.Play();
        }
        else
        {
            Debug.LogError("AudioSource reference is null!");
        }
    }
    
}
