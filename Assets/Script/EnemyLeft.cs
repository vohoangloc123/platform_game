using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;
public class EnemyLeft : MonoBehaviour
// , IDataPersistence
{
    // Start is called before the first frame update
    [SerializeField]
    public int enemyNumber;
    public TextMeshProUGUI enemyText;
    public String sceneName;
    public bool countingEnemies = true;
    void Start()
    {
        enemyText = GetComponent<TextMeshProUGUI>();
        if (enemyText == null)
        {
            Debug.LogError("TextMeshProUGUI component is missing. Please attach a TextMeshProUGUI component to the GameObject.");
        }
        StartCoroutine(UpdateEnemyCountRoutine());
    }

    // Update is called once per frame
    public void UpdateEnemyNumber(int changeAmount)
    {
        enemyNumber += changeAmount;

        if (enemyNumber <= 0)
        {
            DataPersistenceManager.instance.SaveGame();
            SceneManager.LoadScene(sceneName);
            
        }
    }
    void Update()
    {
        enemyText.text = "Enemy Left: " + enemyNumber;
    }

    IEnumerator UpdateEnemyCountRoutine()
    {
        while (countingEnemies)
        {
            UpdateEnemyNumber1(0); // Cập nhật lại số enemy mỗi giây
            yield return new WaitForSeconds(1f); // Đợi 1 giây trước khi cập nhật tiếp
        }
    }

    public int CountEnemiesWithTag(string tag)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(tag);
        return enemies.Length;
    }

    public void UpdateEnemyNumber1(int changeAmount)
    {
        int enemyCountWithTag = CountEnemiesWithTag("Enemy");
        enemyNumber = Mathf.Max(enemyNumber, enemyCountWithTag);

        if (enemyNumber <= 0)
        {
            DataPersistenceManager.instance.SaveGame();
            // Invoke("LoadSceneAfterDelay", 2f); // Gọi hàm sau 2 giây
            SceneManager.LoadScene(sceneName);
        }

        enemyText.text = "Enemy Left: " + enemyNumber;
    }
    // private void LoadSceneAfterDelay()
    // {
    //     SceneManager.LoadScene(sceneName);
    // }
}


