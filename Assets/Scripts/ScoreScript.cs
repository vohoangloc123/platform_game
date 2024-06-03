
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
public class ScoreScript : MonoBehaviour, IDataPersistence
{
    public static int scoreValue = 0;
    public Text score;

    private bool isFinishGameScene = false; // Thêm biến kiểm tra scene FinishGame

    // Start is called before the first frame update
    void Start()
    {
        score = GetComponent<Text>();
        if (score == null)
        {
            Debug.LogError("TextMeshProUGUI component is missing. Please attach a TextMeshProUGUI component to the GameObject.");
        }

        // Kiểm tra nếu đang ở trong scene FinishGame
        isFinishGameScene = (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "FinishGame");
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
}

