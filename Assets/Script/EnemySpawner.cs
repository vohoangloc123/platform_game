using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Đây là một thuộc tính Unity, cho phép các biến dưới đây (swarmerPrefab, bigSwarmerPrefab, swarmerInterval, bigSwarmerInterval) 
    //hiển thị và có thể chỉnh sửa trong trình chỉnh sửa Inspector của Unity mặc dù chúng là các biến private.
    [SerializeField]
    private GameObject swarmerPrefab;
    [SerializeField]
    private GameObject bigSwarmerPrefab;
     //private GameObject swarmerPrefab;, private GameObject bigSwarmerPrefab;: 
    //Đây là hai biến để lưu trữ prefab của hai loại quái vật: "swarmer" và "bigSwarmer". 
    //Prefab là một tài nguyên trong Unity đại diện cho một kiểu đối tượng có thể được tái sử dụng để tạo nhiều thể hiện khác nhau trong trò chơi.
    [SerializeField]
    private float swarmerInterval = 3.5f;
    [SerializeField]
    private float bigSwarmerInterval =  10f;
    //private float swarmerInterval = 3.5f;, private float bigSwarmerInterval = 10f;: 
    //Đây là hai biến để xác định khoảng thời gian giữa mỗi lần sinh ra quái vật loại "swarmer" và "bigSwarmer". 
    //Ví dụ: mỗi 3.5 giây sẽ tạo một quái vật loại "swarmer", và mỗi 10 giây sẽ tạo một quái vật loại "bigSwarmer".
    [SerializeField]
    private int enemySpawnCount = 0; // Biến đếm số lần sinh ra quái vật
    [SerializeField]
    private int maxEnemySpawns; // Số lần sinh ra tối đa (20 lần)

    //trục vị trí spawn
    public float x1;
    public float x2;
    public float y1;
    public float y2;

    void Start()
    {
        StartCoroutine(spawnEnemy(swarmerInterval, swarmerPrefab));
        StartCoroutine(spawnEnemy(bigSwarmerInterval, bigSwarmerPrefab));

    }
    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {

        if (enemySpawnCount >= maxEnemySpawns)
        {
            // Đã sinh ra đủ số lượng quái vật, không sinh thêm
            yield break;
        }

        yield return new WaitForSeconds(interval);

        // Kiểm tra vị trí sinh ra quái vật
        Vector3 spawnPosition = new Vector3(Random.Range(x1, x2), Random.Range(y1, y2), 0f);
        //Đây là một Coroutine để sinh ra các quái vật loại "swarmer". 
        //Coroutine cho phép thực hiện công việc chạy song song với các công việc khác trong trò chơi. 
        //Khi Coroutine này được gọi, sau mỗi khoảng thời gian swarmerInterval, nó sẽ tạo một quái vật "swarmer" 
        //tại một vị trí ngẫu nhiên trong khoảng từ -5 đến 5 trên trục X và từ -5 đến 5 trên trục Y.

        //Mở code dưới đây nếu bạn muốn nó chỉ xuất hiện trên bề mặt đất(còn lỗi)
        // RaycastHit2D hit = Physics2D.Raycast(spawnPosition, Vector2.down, Mathf.Infinity, LayerMask.GetMask("Ground"));
      
        // if (hit.collider != null)
        // {
        //     // Nếu có va chạm với layer "Ground", thì sinh ra quái vật tại vị trí hit.point
        //     spawnPosition = hit.point;
        //     GameObject newEnemy = Instantiate(enemy, spawnPosition, Quaternion.identity);
        //     // Tăng biến đếm số lượng quái vật đã sinh ra
        //     enemySpawnCount++;
        // }

        GameObject newEnemy = Instantiate(enemy, spawnPosition, Quaternion.identity);
        // Tăng biến đếm số lượng quái vật đã sinh ra
        enemySpawnCount++;
        // Gọi lại chính nó để tiếp tục sinh quái vật
        StartCoroutine(spawnEnemy(interval, enemy));
    }

}
