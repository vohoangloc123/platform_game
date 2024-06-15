using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitFire : MonoBehaviour
{
    public string enemyTag = "Enemy"; // Tag của đối tượng Enemy
    public float detectionRange = 10f; // Khoảng cách để phát hiện
    public GameObject spitFireObject; // Đối tượng cần hiển thị hoặc ẩn

    // Update is called once per frame
    void Update()
    {
        bool foundEnemy = false;

        // Tìm tất cả các đối tượng có tag là "Enemy" trong khoảng cách detectionRange
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, detectionRange);

        foreach (Collider2D enemy in enemies)
        {
            if (enemy.CompareTag(enemyTag))
            {
                foundEnemy = true;
                break;
            }
        }

        // Hiển thị hoặc ẩn đối tượng theo tình trạng tìm thấy Enemy
        if (foundEnemy)
        {
            spitFireObject.SetActive(true); // Hiển thị đối tượng
        }
        else
        {
            spitFireObject.SetActive(false); // Ẩn đối tượng
        }
    }
}
