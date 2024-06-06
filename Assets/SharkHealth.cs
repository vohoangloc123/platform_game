using UnityEngine;

public class SharkHealth : MonoBehaviour, IDataPersistence
{
    // save
    [SerializeField] private string id;
    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }
    
    public int maxHealth = 10;
    public int health;
    private bool killed = false;
    private EnemyLeft enemyLeft;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0 && gameObject.CompareTag("Shark"))
        {
            ScoreScript.scoreValue += 50;
            killed = true;
            FindObjectOfType<EnemyLeft>().UpdateEnemyNumber(-1); // Giảm số lượng quái vật đi 1
            Destroy(gameObject);
        }
    }

    public void LoadData(GameData gameData)
    {
        gameData.enemiesKilled.TryGetValue(id, out killed);
        if (killed)
        {
            gameObject.SetActive(false);
        }
    }

    public void SaveData(ref GameData gameData)
    {
        if (gameData.enemiesKilled.ContainsKey(id))
        {
            gameData.enemiesKilled.Remove(id);
        }
        gameData.enemiesKilled.Add(id, killed);
    }
}
