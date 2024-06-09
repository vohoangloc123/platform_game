

using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnCollision : MonoBehaviour
{   [SerializeField]
    public string sceneName; // Tên của scene bạn muốn chuyển đến

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Kiểm tra xem đối tượng va chạm có tag là "Player" hay không (hoặc bất kỳ điều kiện phù hợp khác)
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collision with player detected."); // Ghi thông tin vào Console để kiểm tra xem va chạm được phát hiện hay không.

            // Lấy vị trí của người chơi từ PlayerPrefs
            
            float playerPosX = PlayerPrefs.GetFloat("PlayerPosX", 0f);
            float playerPosY = PlayerPrefs.GetFloat("PlayerPosY", 0f);
         
            Debug.Log("Scene loaded: " + sceneName); // Ghi thông tin vào Console để kiểm tra xem scene đã được chuyển đến chưa.

            // Thiết lập vị trí của người chơi trong scene mới
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            // if (player != null)
            // {
            //     player.transform.position = new Vector3(playerPosX, playerPosY, player.transform.position.z);
            //     DataPersistenceManager.instance.SaveGame();
            //     // Chuyển đến scene có tên được chỉ định
            //     SceneManager.LoadScene(sceneName);
            // }
            // else
            // {
            //     Debug.LogError("Player not found in the scene.");
            // }
             if (player != null)
            {
                // Đặt vị trí người chơi thành (0, 0) trước khi lưu
                Vector3 defaultPosition = new Vector3(0f, 0f, player.transform.position.z);
                player.transform.position = defaultPosition;

                DataPersistenceManager.instance.SavePlayerPosition(defaultPosition);
                
                Debug.Log($"Saved player position: {defaultPosition.x}, {defaultPosition.y}");
                SceneManager.LoadScene(sceneName);
            }
            else
            {
                Debug.LogError("Player not found in the scene.");
            }
        }
    }
}
