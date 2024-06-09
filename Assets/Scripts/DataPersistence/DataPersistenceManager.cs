
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;  // Đã sửa lỗi tên namespace
//main menu
using UnityEngine.SceneManagement;
public class DataPersistenceManager : MonoBehaviour
{
    [Header("Debugging")]
    [SerializeField] private bool initializeDataIfNull = false;
    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption = false;
    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;
    private string savedSceneName;
    public static DataPersistenceManager instance
    {
        get;
        private set;
    }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Data Persistence Manager in the scene. Delete the newest one");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, this.fileName, this.useEncryption);
    }
    //main menu
    public void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }
    public void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Debug.Log("OnSceneLoaded called");
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
      
        LoadGame();
    }
    public void OnSceneUnloaded(Scene scene)
    {
        // Debug.Log("OnSceneUnloaded called");
        SaveGame();
    }

    public void NewGame()
    {
        // Giả sử GameData là một class hoặc struct hợp lệ
        this.gameData = new GameData();
    }
    public void LoadGame()
    {
        // TODO: Tải dữ liệu đã lưu từ tệp bằng cách sử dụng trình xử lý dữ liệu
        this.gameData = this.dataHandler.Load();
        //start a new game if the data is null and we're configured data for debugging
        if(this.gameData == null && this.initializeDataIfNull)
        {
            Debug.Log("No saved game found. Initializing new data");
            NewGame();
        }
       //if no data can be loaded, dont continue
        if (this.gameData == null)
        {
            Debug.Log("No data was found. A New Game needs to be started before data can be loaded");
            return;
        }
       
        // TODO - Đẩy dữ liệu đã tải đến tất cả các kịch bản khác cần dùng
        foreach (IDataPersistence dataPersistenceObject in this.dataPersistenceObjects)
        {
            dataPersistenceObject.LoadData(this.gameData);
        }
        Debug.Log("Đã tải x = " + gameData.x+ " từ dữ liệu đã lưu");  // Đã sửa dấu ngoặc kép
        Debug.Log("Đã tải y = " + gameData.y+ " từ dữ liệu đã lưu");  // Đã sửa dấu ngoặc kép
        //new
        // Khôi phục vị trí của người chơi
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = new Vector3(this.gameData.x, this.gameData.y, player.transform.position.z);
            Debug.Log($"Player position set to: {this.gameData.x}, {this.gameData.y}");
        }
        else
        {
            Debug.LogWarning("Player object not found to set position.");
        }
    }
    public void SavePlayerPosition(Vector3 position)
    {
        if (this.gameData == null)
        {
            this.gameData = new GameData();
        }

        this.gameData.x = position.x;
        this.gameData.y = position.y;
        SaveGame();
    }
    public void SaveGame()
    {
        //if we dont have any data to save, log a warning here
        if(this.gameData ==null)
        {
            Debug.LogWarning("No data was found. A new game needs to be started before data can be saved");
            return;
        }
        // TODO - Truyền dữ liệu cho các kịch bản khác để họ có thể cập nhật nó
        foreach (IDataPersistence dataPersistenceObject in this.dataPersistenceObjects)
        {
            dataPersistenceObject.SaveData(ref this.gameData);
        }
        // Debug.Log("Đã lưu điểm số = " + gameData.score + " vào dữ liệu đã lưu");  // Đã sửa dấu ngoặc kép
        // TODO - Lưu dữ liệu vào tệp bằng cách sử dụng trình xử lý dữ liệu
           Debug.Log("Đã lưu X = " + gameData.x+ " vào dữ liệu đã lưu"); 
            Debug.Log("Đã lưu Y = " + gameData.y + " vào dữ liệu đã lưu"); 
        dataHandler.Save(this.gameData);
        string saveFilePath = System.IO.Path.Combine(Application.persistentDataPath, fileName);
        Debug.Log("Đã lưu dữ liệu vào tệp: " + saveFilePath);
    }
    public void SaveDefaultPosition()
    {
        if (gameData == null)
        {
            Debug.LogWarning("No game data found. Initializing new data.");
            NewGame();
        }
        else
        {
            // Gọi hàm xóa vị trí
            DeletePosition();
            // Lưu gameData
            SaveGame();
        }
    }
    public void DeletePosition()
    {
        if (gameData != null)
        {
            // Xóa giá trị của x và y trong gameData
            gameData.x = 0;
            gameData.y = 0;
            Debug.Log("Deleted gameData position.");
        }
        else
        {
            Debug.LogWarning("No game data found. Cannot delete position.");
        }
    }
    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }
    public bool HasGameData()
    {
        return gameData != null;
    }

    public void SetSavedSceneName(string sceneName)
    {
        savedSceneName = sceneName;
        Debug.Log(sceneName);
    }
    public string ReadSceneNameFromData()
    {
    GameData loadedData = this.dataHandler.Load();
    if (loadedData != null)
    {
        return loadedData.sceneName;
    }
    else
    {
        Debug.LogWarning("No saved data found.");
        return null;
    }
    }

}
