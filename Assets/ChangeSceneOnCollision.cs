// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class ChangeSceneOnCollision : MonoBehaviour
// {
//     public string sceneName; // Tên của scene bạn muốn chuyển đến
//     public SavePosition savePosition; // Tham chiếu đến script SavePosition

//     private void OnCollisionEnter2D(Collision2D collision)
//     {
//         // Kiểm tra xem đối tượng va chạm có tag là "Player" hay không (hoặc bất kỳ điều kiện phù hợp khác)
//         if (collision.gameObject.CompareTag("Player"))
//         {
//             Debug.Log("Collision with player detected."); // Ghi thông tin vào Console để kiểm tra xem va chạm được phát hiện hay không.
            
//             // Lưu vị trí mong muốn
//             savePosition.SetPosition(new Vector2(-9.31f, 11.24f));
            
//             // Chuyển đến scene có tên được chỉ định
//             SceneManager.LoadScene(sceneName);
//             Debug.Log("Scene loaded: " + sceneName); // Ghi thông tin vào Console để kiểm tra xem scene đã được chuyển đến chưa.
//         }
//     }
// }

using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnCollision : MonoBehaviour
{
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

            // Chuyển đến scene có tên được chỉ định
            SceneManager.LoadScene(sceneName);
            Debug.Log("Scene loaded: " + sceneName); // Ghi thông tin vào Console để kiểm tra xem scene đã được chuyển đến chưa.

            // Thiết lập vị trí của người chơi trong scene mới
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                player.transform.position = new Vector3(playerPosX, playerPosY, player.transform.position.z);
            }
            else
            {
                Debug.LogError("Player not found in the scene.");
            }
        }
    }
}
