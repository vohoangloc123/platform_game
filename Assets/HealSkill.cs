using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealSkill : MonoBehaviour
{
    public PlayerHealth playerHealth; // Đối tượng PlayerHealth của người chơi
    public int healAmount = 100; // Lượng máu hồi
    public float cooldownTime = 3f; // Thời gian hồi chiêu (giây)
    private float nextHealTime = 0f; // Thời gian có thể sử dụng tiếp theo
    public Text cooldownText; // Text UI để hiển thị thời gian hồi chiêu
    public GameObject healSkill;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && Time.time >= nextHealTime)
        {
            Heal(); // Hồi máu cho người chơi
            nextHealTime = Time.time + cooldownTime; // Cập nhật thời gian hồi chiêu
            StartCoroutine(Cooldown()); // Bắt đầu đếm ngược thời gian hồi chiêu
        }

        UpdateCooldownText(); // Cập nhật text hiển thị thời gian hồi chiêu
    }

    void Heal()
    {
        // Kiểm tra nếu máu của người chơi đã đạt hoặc vượt mức tối đa thì không hồi máu nữa
        if (playerHealth.health >= 1000)
        {
            return; // Không hồi máu nếu đã đạt mức tối đa
        }

        // Cộng thêm máu cho người chơi, nhưng không vượt quá mức tối đa
        playerHealth.health = Mathf.Min(playerHealth.health + healAmount, 1000);

        // Cập nhật thanh máu
        healSkill.SetActive(true);
        playerHealth.healthBar.SetHealth(playerHealth.health);
        playerHealth.healthBar.UpdateHealthBar(playerHealth.health);
        StartCoroutine(HideObject());
    }

    IEnumerator Cooldown()
    {
        float remainingCooldown = cooldownTime;
        while (remainingCooldown > 0)
        {
            cooldownText.text = remainingCooldown.ToString("F0"); // Hiển thị thời gian còn lại
            yield return new WaitForSeconds(1f); // Đợi 1 giây
            remainingCooldown -= 1f; // Giảm thời gian còn lại
        }
        cooldownText.text = ""; // Xóa text khi hết thời gian hồi chiêu
    }

    void UpdateCooldownText()
    {
        if (playerHealth.health >= 1000)
        {
            cooldownText.text = "Full"; // Hiển thị "Full" nếu máu đã đầy
        }
        else if (Time.time < nextHealTime)
        {
            cooldownText.text = (nextHealTime - Time.time).ToString("F0"); // Cập nhật text hiển thị thời gian hồi chiêu
        }
        else
        {
            cooldownText.text = ""; // Xóa text khi hết thời gian hồi chiêu
        }
    }
     IEnumerator HideObject()
    {
        yield return new WaitForSeconds(1f); // Thay đổi thời gian tùy ý
        if (healSkill != null)
        {
            healSkill.SetActive(false);
        }
        else
        {
            Debug.LogError("Heal object is not assigned!");
        }
    }
}

