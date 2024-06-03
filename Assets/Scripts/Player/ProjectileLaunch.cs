using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLaunch : MonoBehaviour
{
    public GameObject projectilePrefab; //một đối tượng (GameObject) được sử dụng để tạo ra đạn khi bắn
    public Transform launchPoint; //là một đối tượng (Transform) định vị vị trí bắn.

    public float shootTime; //là thời gian giữa mỗi lần bắn
    public float shootCounter; //đếm thời gian còn lại trước khi có thể bắn lần tiếp theo.
    // Start is called before the first frame update
    void Start()
    {
        shootCounter = shootTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && shootCounter <= 0)
        {
            if (projectilePrefab != null) // Kiểm tra đối tượng projectilePrefab có tồn tại hay không
            {
                Instantiate(projectilePrefab, launchPoint.position, Quaternion.identity);
                shootCounter = shootTime;
            }
        }
    shootCounter -= Time.deltaTime;
    }
}

