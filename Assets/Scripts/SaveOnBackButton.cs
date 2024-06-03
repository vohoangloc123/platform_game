using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 
public class SaveOnBackButton : MonoBehaviour
{
    public Button backButton;

    private void Start()
    {
        // Gắn hàm lưu vào sự kiện khi nút "Back to Menu" được nhấn
        backButton.onClick.AddListener(SaveAndGoBack);
    }

    private void SaveAndGoBack()
    {
        // Trước tiên, kiểm tra xem DataPersistenceManager đã tồn tại chưa
        DataPersistenceManager dataPersistenceManager = DataPersistenceManager.instance;
        if (dataPersistenceManager != null)
        {
            // Lưu dữ liệu trước khi quay lại menu
            dataPersistenceManager.SaveGame();
        }
        // Sau đó, thực hiện hành động quay lại menu ở đây
        // Ví dụ: Load lại scene chứa menu
        SceneManager.LoadScene("StartGame"); // Đảm bảo bạn đã thêm namespace UnityEngine.SceneManagement
    }
}
