using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;

[System.Serializable]
public class ScoreList
{
    public List<ScoreData> scores = new List<ScoreData>();
}
 [System.Serializable]
    public class ScoreData
    {
        public int STT;
        public int score;
    }
public class ScoreDisplay : MonoBehaviour
{
    public Text scoreText; // Gán UI Text từ giao diện người dùng

    void Start()
    {
        LoadScoresFromJSON();
    }

    void LoadScoresFromJSON()
    {
        string path = Path.Combine(Application.persistentDataPath, "scores.json");

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            ScoreList scoreList = JsonUtility.FromJson<ScoreList>(json);

            if (scoreList != null && scoreList.scores.Count > 0)
            {
                string displayText = "Top 10 Scores:\n";
                foreach (ScoreData scoreData in scoreList.scores)
                {
                    displayText +=scoreData.STT + " - Score: " + scoreData.score + "\n";
                }

                scoreText.text = displayText;
            }
            else
            {
                scoreText.text = "No scores available.";
            }
        }
        else
        {
            scoreText.text = "No scores saved yet.";
        }
    }
}

