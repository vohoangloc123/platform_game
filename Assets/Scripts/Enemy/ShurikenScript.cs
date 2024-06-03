
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public float force;
    public float timer;
    public int damage;
    public float rotationSpeed = 100f; // Tốc độ quay của viên đạn
    private bool isRotating = true; // Biến để kiểm tra liệu viên đạn có nên xoay hay không

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector2 randomDirection;

        // Tạo hướng di chuyển ban đầu
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            Vector3 direction = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
            rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
            float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rot + 180);
        }
        else
        {
            // Nếu không tìm thấy đối tượng kẻ địch, di chuyển theo hướng ngẫu nhiên
            float randomAngle = Random.Range(0f, 360f);
            randomDirection = new Vector2(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad));
            rb.velocity = randomDirection.normalized * force;
        }

        // Bắt đầu coroutine để xoay viên đạn liên tục
        StartCoroutine(RotateBullet());
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 2)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
            // Sau khi xử lý va chạm với Player, hủy viên đạn
            Destroy(gameObject);
        }
    }

    IEnumerator RotateBullet()
    {
        while (isRotating)
        {
            // Xoay viên đạn 360 độ liên tục
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
            yield return null;
        }
    }
}

