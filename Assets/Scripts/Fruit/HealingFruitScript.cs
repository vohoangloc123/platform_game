using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HealingFruitScript : MonoBehaviour, IDataPersistence
{
    public int heal = -50; // Số điểm cộng khi nhặt được coin
    public PlayerHealth playerHealth;
    [SerializeField] private string id;
    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }
    private bool collected=false;
    public AudioClip fruitSound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Player") // Kiểm tra nếu player va chạm với fruit
        {
            // Cộng điểm vào hệ thống điểm của bạn ở đây
            collected=true;
            AudioSource.PlayClipAtPoint(fruitSound, transform.position); // Phát âm thanh tại vị trí hiện tại
            if (playerHealth.health< 900)
            {
                playerHealth.TakeDamage(heal);
            }
            else if (playerHealth.health >= 900 && playerHealth.health < 950)
            {
                playerHealth.TakeDamage(heal / 2);
            }
            else if (playerHealth.health >= 950)
            {
                playerHealth.TakeDamage(0);
            }

            // Biến mất coin
            Destroy(gameObject);
        }
    }
    public void LoadData(GameData gameData)
    {
        gameData.fruitCollected.TryGetValue(id, out collected);
        if(collected)
        {
            gameObject.SetActive(false);
        }
    }

    public void SaveData(ref GameData gameData)
    {
       if(gameData.fruitCollected.ContainsKey(id))
       {
           gameData.fruitCollected.Remove(id);
       }
       gameData.fruitCollected.Add(id, collected);
    }
}