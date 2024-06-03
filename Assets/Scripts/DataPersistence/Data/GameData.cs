using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GameData
{
    public int score;
    public SerializeableDictionary<string, bool> enemiesKilled;
    public int health;
    public string sceneName;
    public int enemyCount;
    public SerializeableDictionary<string, bool> coinCollected;
    public SerializeableDictionary<string, bool> fruitCollected;
     public float x; // Vị trí x
    public float y; // Vị trí y
    public GameData()
    {
        this.score = 0;
        enemiesKilled = new SerializeableDictionary<string, bool>();
        this.health = 1000;
        this.sceneName = "0";
        this.enemyCount = 0;
        coinCollected = new SerializeableDictionary<string, bool>();
        fruitCollected = new SerializeableDictionary<string, bool>();
        this.x = 0.0f; // Khởi tạo vị trí x
        this.y = 0.0f; // Khởi tạo vị trí y
    }

    public static implicit operator float(GameData v)
    {
        throw new NotImplementedException();
    }
}
