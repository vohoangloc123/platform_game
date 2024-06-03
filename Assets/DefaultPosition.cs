// using UnityEngine;

// public class PositionManager : MonoBehaviour
// {
//     public static PositionManager instance; // Singleton instance

//     public Vector3 savedPosition; // Vị trí được lưu

//     private void Awake()
//     {
//         if (instance == null)
//         {
//             instance = this;
//             DontDestroyOnLoad(gameObject); // Đảm bảo rằng đối tượng này không bị hủy khi chuyển scene
//         }
//         else
//         {
//             Destroy(gameObject);
//         }
//     }

//     // Lưu vị trí
//     public void SavePosition(Vector3 position)
//     {
//         savedPosition = position;
//     }

//     // Đặt lại vị trí sau khi chuyển scene
//     public void ResetPosition(Transform objectTransform)
//     {
//         objectTransform.position = savedPosition;
//     }
// }

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PositionManager : MonoBehaviour
{
    public static PositionManager instance; // Singleton instance

    private Dictionary<string, Vector3> scenePositions = new Dictionary<string, Vector3>(); // Lưu trữ vị trí của từng scene

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Đảm bảo rằng đối tượng này không bị hủy khi chuyển scene
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Lưu vị trí của scene
    public void SaveScenePosition(string sceneName, Vector3 position)
    {
        if (!scenePositions.ContainsKey(sceneName))
        {
            scenePositions.Add(sceneName, position);
        }
    }

    // Đặt lại vị trí của đối tượng sau khi chuyển scene
    public void ResetObjectPosition(Transform objectTransform)
    {
        string nextSceneName = SceneManager.GetActiveScene().name;
        if (scenePositions.ContainsKey(nextSceneName))
        {
            objectTransform.position = scenePositions[nextSceneName];
        }
    }
}
