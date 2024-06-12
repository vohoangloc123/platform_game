
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    public float timer;
    private GameObject player;
    public float throwDistance = 10;
    public GameObject weaponObject;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player not found! Make sure the player has the 'Player' tag.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);
            if (distance < throwDistance)
            {
                timer += Time.deltaTime;
                Debug.Log("Timer: " + timer);

                if (timer > 2)
                {
                    timer = 0;
                    shoot();
                    weaponObject.SetActive(false);
                    Debug.Log("Weapon shown");
                    StartCoroutine(HideWeapon());
                }
            }
        }
    }

    void shoot()
    {
        if (bullet != null && bulletPos != null)
        {
            Instantiate(bullet, bulletPos.position, Quaternion.identity);
            Debug.Log("Shot fired");
        }
        else
        {
            Debug.LogError("Bullet or bulletPos is not assigned!");
        }
    }

    IEnumerator HideWeapon()
    {
        yield return new WaitForSeconds(0.5f); // Thay đổi thời gian tùy ý
        if (weaponObject != null)
        {
            weaponObject.SetActive(true);
            Debug.Log("Weapon hidden");
        }
        else
        {
            Debug.LogError("WeaponObject is not assigned!");
        }
    }
}
