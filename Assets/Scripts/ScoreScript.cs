
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using System.IO;
public class ScoreScript : MonoBehaviour, IDataPersistence
{
    public static int scoreValue = 0;
    public Text score;
    
    private bool isFinishGameScene = false; // Thêm biến kiểm tra scene FinishGame

    // Start is called before the first frame update
    [System.Serializable]
    public class ScoreData
    {
        public int STT;
        public int score;
    }

    [System.Serializable]
    public class ScoreList
    {
        public List<ScoreData> scores = new List<ScoreData>();
    }
    void Start()
    {
        score = GetComponent<Text>();
        if (score == null)
        {
            Debug.LogError("TextMeshProUGUI component is missing. Please attach a TextMeshProUGUI component to the GameObject.");
        }

        // Kiểm tra nếu đang ở trong scene FinishGame
        isFinishGameScene = (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "FinishGame");
        if (isFinishGameScene)
        {
            SaveScoreToJson();
            scoreValue = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Kiểm tra nếu đang ở trong scene FinishGame, thì reset điểm số
        if (isFinishGameScene)
        {
            scoreValue = 0;
        }

        score.text = "Score: " + scoreValue;
    }

    public void LoadData(GameData gameData)
    {
        scoreValue = gameData.score;
    }

    public void SaveData(ref GameData gameData)
    {
        gameData.score = scoreValue;
    }
    
    //  public void SaveScoreToJson()
    // {
    //     string path = Path.Combine(Application.persistentDataPath, "scores.json");
    //     ScoreList scoreList;

    //     if (File.Exists(path))
    //     {
    //         string json = File.ReadAllText(path);
    //         scoreList = JsonUtility.FromJson<ScoreList>(json);
    //     }
    //     else
    //     {
    //         scoreList = new ScoreList();
    //     }

    //     // Tạo đối tượng ScoreData mới
    //     ScoreData newScoreData = new ScoreData { score = scoreValue };

    //     // Kiểm tra xem có điểm cao hơn trong danh sách không
    //     bool highScoreReplaced = false;
    //     for (int i = 0; i < scoreList.scores.Count; i++)
    //     {
    //         if (scoreValue > scoreList.scores[i].score)
    //         {
    //             // Thay thế điểm cao nhất và lùi các điểm khác xuống
    //             scoreList.scores.Insert(i, newScoreData);
    //             highScoreReplaced = true;
    //             break;
    //         }
    //     }

    //     if (!highScoreReplaced)
    //     {
    //         scoreList.scores.Add(newScoreData);
    //     }

    //     // Cập nhật STT cho tất cả điểm
    //     for (int i = 0; i < scoreList.scores.Count; i++)
    //     {
    //         scoreList.scores[i].STT = i + 1;
    //     }

    //     // Lưu danh sách điểm vào file JSON
    //     string updatedJson = JsonUtility.ToJson(scoreList, true);
    //     File.WriteAllText(path, updatedJson);

    //     Debug.Log("Score saved to " + path);
    // }
      public void SaveScoreToJson()
    {
        string path = Path.Combine(Application.persistentDataPath, "scores.json");
        ScoreList scoreList;

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            scoreList = JsonUtility.FromJson<ScoreList>(json);
        }
        else
        {
            scoreList = new ScoreList();
        }

        // Tạo đối tượng ScoreData mới
        ScoreData newScoreData = new ScoreData { score = scoreValue };

        // Kiểm tra xem có điểm cao hơn trong danh sách không
        bool highScoreReplaced = false;
        for (int i = 0; i < scoreList.scores.Count; i++)
        {
            if (scoreValue > scoreList.scores[i].score)
            {
                // Thay thế điểm cao nhất và lùi các điểm khác xuống
                scoreList.scores.Insert(i, newScoreData);
                highScoreReplaced = true;
                break;
            }
        }

        if (!highScoreReplaced)
        {
            scoreList.scores.Add(newScoreData);
        }

        // Giới hạn danh sách chỉ chứa top 10 điểm số
        if (scoreList.scores.Count > 10)
        {
            scoreList.scores.RemoveRange(10, scoreList.scores.Count - 10);
        }

        // Cập nhật STT cho tất cả điểm
        for (int i = 0; i < scoreList.scores.Count; i++)
        {
            scoreList.scores[i].STT = i + 1;
        }

        // Lưu danh sách điểm vào file JSON
        string updatedJson = JsonUtility.ToJson(scoreList, true);
        File.WriteAllText(path, updatedJson);

        Debug.Log("Score saved to " + path);
    }
}

