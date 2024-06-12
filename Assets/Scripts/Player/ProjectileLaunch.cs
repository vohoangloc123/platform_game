using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLaunch : MonoBehaviour
{
    public GameObject projectilePrefab; //một đối tượng (GameObject) được sử dụng để tạo ra đạn khi bắn
    public Transform launchPoint; //là một đối tượng (Transform) định vị vị trí bắn.

    public float shootTime; //là thời gian giữa mỗi lần bắn
    public float shootCounter; //đếm thời gian còn lại trước khi có thể bắn lần tiếp theo.
    public GameObject weaponIdle;
    public GameObject weaponAttack;
    public AudioSource bulletSound;
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
                // weaponIdle.SetActive(false);
                // weaponAttack.SetActive(true);
                PlaySound();
                Instantiate(projectilePrefab, launchPoint.position, Quaternion.identity);
                shootCounter = shootTime;
                StartCoroutine(HideWeapon());
            }
        }
    shootCounter -= Time.deltaTime;
    }
    IEnumerator HideWeapon()
    {
        yield return new WaitForSeconds(0.5f); // Thay đổi thời gian tùy ý
        if (weaponIdle != null&& weaponAttack!=null)
        {
            // weaponIdle.SetActive(true);
            // weaponAttack.SetActive(false);
            Debug.Log("Weapon hidden");
        }
        else
        {
            Debug.LogError("WeaponObject is not assigned!");
        }
    }
    public void PlaySound()
    {
        if(bulletSound != null)
        {
            // Phát âm thanh
            bulletSound.Play();
        }
        else
        {
            Debug.LogError("AudioSource reference is null!");
        }
    }
}

