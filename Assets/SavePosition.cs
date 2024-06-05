using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePosition : MonoBehaviour, IDataPersistence
{
    private float positionX;
    private float positionY;
    private float defaultX;
    private float defaultY;

    public Vector2 GetPosition()
    {
        return new Vector2(positionX, positionY);
    }

    public void SetPosition(Vector2 newPosition)
    {
        positionX = newPosition.x;
        positionY = newPosition.y;
    }

    void Start()
    {
        // Khởi tạo vị trí ban đầu
            SetPosition(transform.position);
    }

    void Update()
    {
        // Cập nhật vị trí nếu cần
        if (transform.hasChanged)
        {
            SetPosition(transform.position);
            transform.hasChanged = false;
        }
    }

    public void LoadData(GameData gameData)
    {
        positionX = gameData.x;
        positionY = gameData.y;
        transform.position = new Vector3(positionX, positionY, transform.position.z);
    }

    public void SaveData(ref GameData gameData)
    {
        positionX = transform.position.x;
        positionY = transform.position.y;
        gameData.x = positionX;
        gameData.y = positionY;
    }
    private void OnDestroy()
    {
        // Lưu vị trí của người chơi khi thoát scene
        PlayerPrefs.SetFloat("PlayerPosX", transform.position.x);
        PlayerPrefs.SetFloat("PlayerPosY", transform.position.y);
    }
}
