// using UnityEngine;
// using UnityEngine.UI;
// using System.Collections;

// public class BabyDragonSkill : MonoBehaviour
// {
//     public GameObject dragon; // Đối tượng dragon cần hiển thị
//     public float dragonCooldownTime = 10f; // Thời gian cooldown giữa mỗi lần hiển thị dragon
//     private float nextDragonTime = 0f; // Thời gian tiếp theo có thể hiển thị dragon

//     public Text cooldownText; // Text UI để hiển thị cooldown của dragon

//     void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.Alpha2) && Time.time >= nextDragonTime)
//         {
//             ShowDragon(); // Hiển thị dragon
//             nextDragonTime = Time.time + dragonCooldownTime; // Cập nhật thời gian cho lần hiển thị dragon kế tiếp
//             StartCoroutine(DragonCooldown()); // Bắt đầu đếm ngược thời gian cooldown của dragon
//         }

//         UpdateCooldownText(); // Cập nhật text hiển thị cooldown
//     }

//     void ShowDragon()
//     {
//         // Hiển thị dragon (nếu dragon đã được gán và không null)
//         if (dragon != null)
//         {
//             dragon.SetActive(true); // Hiển thị dragon
//             StartCoroutine(HideDragonAfterDelay()); // Gọi hàm để ẩn dragon sau một khoảng thời gian
//         }
//         else
//         {
//             Debug.LogError("Dragon object is not assigned!"); // Báo lỗi nếu dragon chưa được gán
//         }
//     }

//     IEnumerator DragonCooldown()
//     {
//         float remainingCooldown = dragonCooldownTime;
//         while (remainingCooldown > 0)
//         {
//             cooldownText.text = remainingCooldown.ToString("F0"); // Hiển thị thời gian cooldown còn lại
//             yield return new WaitForSeconds(1f); // Đợi 1 giây
//             remainingCooldown -= 1f; // Giảm thời gian cooldown còn lại
//         }
//         cooldownText.text = ""; // Xóa text khi hết thời gian cooldown
//     }

//     void UpdateCooldownText()
//     {
//         if (Time.time < nextDragonTime)
//         {
//             cooldownText.text = (nextDragonTime - Time.time).ToString("F0"); // Cập nhật text hiển thị cooldown
//         }
//         else
//         {
//             cooldownText.text = ""; // Xóa text khi hết thời gian cooldown
//         }
//     }

//     IEnumerator HideDragonAfterDelay()
//     {
//         yield return new WaitForSeconds(5f); // Thời gian tùy ý để dragon hiển thị
//         if (dragon != null)
//         {
//             dragon.SetActive(false); // Ẩn dragon sau khi đã hiển thị
//         }
//     }
// }

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BabyDragonSkill : MonoBehaviour
{
    public GameObject dragonPrefab; // Prefab của đối tượng dragon cần hiển thị
    public GameObject player; // Đối tượng player
    public float dragonCooldownTime = 10f; // Thời gian cooldown giữa mỗi lần hiển thị dragon
    private float nextDragonTime = 0f; // Thời gian tiếp theo có thể hiển thị dragon

    public Text cooldownText; // Text UI để hiển thị cooldown của dragon

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2) && Time.time >= nextDragonTime)
        {
            ShowDragon(); // Hiển thị dragon
            nextDragonTime = Time.time + dragonCooldownTime; // Cập nhật thời gian cho lần hiển thị dragon kế tiếp
            StartCoroutine(DragonCooldown()); // Bắt đầu đếm ngược thời gian cooldown của dragon
        }

        UpdateCooldownText(); // Cập nhật text hiển thị cooldown
    }

    void ShowDragon()
    {
        // Hiển thị dragon (nếu dragonPrefab và player đã được gán và không null)
        if (dragonPrefab != null && player != null)
        {
            Vector3 spawnPosition = player.transform.position + new Vector3(2f, 0f, 2f); // Vị trí spawn gần player
            GameObject spawnedDragon = Instantiate(dragonPrefab, spawnPosition, Quaternion.identity); // Tạo đối tượng dragon mới
            StartCoroutine(DestroyDragonAfterDelay(spawnedDragon)); // Gọi hàm để hủy dragon sau 10 giây
        }
        else
        {
            Debug.LogError("Dragon prefab or player object is not assigned!"); // Báo lỗi nếu dragonPrefab hoặc player chưa được gán
        }
    }

    IEnumerator DragonCooldown()
    {
        float remainingCooldown = dragonCooldownTime;
        while (remainingCooldown > 0)
        {
            cooldownText.text = remainingCooldown.ToString("F0"); // Hiển thị thời gian cooldown còn lại
            yield return new WaitForSeconds(1f); // Đợi 1 giây
            remainingCooldown -= 1f; // Giảm thời gian cooldown còn lại
        }
        cooldownText.text = ""; // Xóa text khi hết thời gian cooldown
    }

    void UpdateCooldownText()
    {
        if (Time.time < nextDragonTime)
        {
            cooldownText.text = (nextDragonTime - Time.time).ToString("F0"); // Cập nhật text hiển thị cooldown
        }
        else
        {
            cooldownText.text = ""; // Xóa text khi hết thời gian cooldown
        }
    }

    IEnumerator DestroyDragonAfterDelay(GameObject spawnedDragon)
    {
        yield return new WaitForSeconds(10f); // Thời gian dragon tồn tại trước khi bị hủy
        if (spawnedDragon != null)
        {
            Destroy(spawnedDragon); // Hủy đối tượng dragon sau khi đã hiển thị
        }
    }
}
