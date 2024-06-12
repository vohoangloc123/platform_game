using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    public void startNew()
    {
        
        SceneManager.LoadScene(1);
        DataPersistenceManager.instance.NewGame();
    }
    public void backToMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void finishMenuGame()
    {
        SceneManager.LoadScene(5);
    }
    public void exit()
    {
        Application.Quit();
    }
}
