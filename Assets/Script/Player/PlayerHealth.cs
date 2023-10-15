using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerHealth : MonoBehaviour,  IDataPersistence
{
    public int health;
    public HealthBar healthBar;
    void Start()
    {
         healthBar.SetMaxHealth(1000);
         UpdateHealthBar();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            SceneManager.LoadScene("EndGame");
            Destroy(gameObject);
            healthBar.SetMaxHealth(0);
        }
        else
        {
            healthBar.SetHealth(health);
        }
    }
    public void SaveData(ref GameData gameData)
    {
        gameData.health = health;
        Debug.Log("Đã save health: "+gameData.health);
    }
    public void LoadData(GameData gameData)
    {
        health = gameData.health;
        healthBar.SetHealth(health);
        Debug.Log("Đã tải dữ liệu health"+ gameData.health);
    }
     private void UpdateHealthBar()
    {
        healthBar.UpdateHealthBar(health);
    }
}
