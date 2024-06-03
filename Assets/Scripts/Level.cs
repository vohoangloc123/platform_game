using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneName : MonoBehaviour, IDataPersistence
{
    public string name;
    // Start is called before the first frame update

    // Update is called once per frame
    public void LoadData(GameData gameData)
    {
    }public void SaveData(ref GameData gameData)
    {
        gameData.sceneName = name;
        Debug.Log("Đã lưu tên màn chơi: " + name);
    }

}
