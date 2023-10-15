using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CoinScript : MonoBehaviour, IDataPersistence
{
    public int scoreValue = 50; // Số điểm cộng khi nhặt được coin
    
    [SerializeField] private string id;
    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }
    private bool collected=false;
    public AudioClip coinSound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Player") // Kiểm tra nếu player va chạm với coin
        {
            // Cộng điểm vào hệ thống điểm của bạn ở đây
            collected=true;
            AudioSource.PlayClipAtPoint(coinSound, transform.position); // Phát âm thanh tại vị trí hiện tại
            ScoreScript.scoreValue += scoreValue;
            // Biến mất coin
            Destroy(gameObject);
        }
    }
    public void LoadData(GameData gameData)
    {
        gameData.coinCollected.TryGetValue(id, out collected);
        if(collected)
        {
            gameObject.SetActive(false);
        }
    }

    public void SaveData(ref GameData gameData)
    {
       if(gameData.coinCollected.ContainsKey(id))
       {
           gameData.coinCollected.Remove(id);
       }
       gameData.coinCollected.Add(id, collected);
    }
}