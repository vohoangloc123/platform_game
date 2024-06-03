using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GameData
{
    public int score;
    public Vector3 playerPosition;
    public SerializeableDictionary<string, bool> enemiesKilled;
    public int health;
    public string sceneName;
    public int enemyCount;
    public SerializeableDictionary<string, bool> coinCollected;
    public SerializeableDictionary<string, bool> fruitCollected;
    public GameData()
    {
        this.score = 0;
        playerPosition=Vector3.zero;
        enemiesKilled = new SerializeableDictionary<string, bool>();
        this.health = 1000;
        this.sceneName = "0";
        this.enemyCount = 0;
        coinCollected = new SerializeableDictionary<string, bool>();
        fruitCollected = new SerializeableDictionary<string, bool>();
    }
}
