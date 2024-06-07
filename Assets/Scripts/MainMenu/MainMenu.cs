using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    [Header("Menu Buttons")]

    [SerializeField] private Button newGameButton;

    [SerializeField] private Button continueGameButton;
     private void Start()
    {
       if(!DataPersistenceManager.instance.HasGameData())
       {
            continueGameButton.interactable = false;
       }
    }
    public void OnNewGameClicked()
    {
        DisableMenuButtons();
        // Debug.Log("New Game Clicked");
        //Load the game play scene - which will in turn save the game because of
        //OnSceneUnloaded() in the data persistence manager
        DataPersistenceManager.instance.NewGame();
        SceneManager.LoadSceneAsync("Level 1");
    }

    // Update is called once per frame
    public void OnContinueGameClicked()
    {
        if (DataPersistenceManager.instance.HasGameData())
        {
            string savedSceneName = DataPersistenceManager.instance.ReadSceneNameFromData();
            if (!string.IsNullOrEmpty(savedSceneName))
            {
                DisableMenuButtons();
                SceneManager.LoadSceneAsync(savedSceneName);
            }
            else
            {
                Debug.LogWarning("No saved scene name found in data.");
            }
        }
        else
        {
            Debug.LogWarning("No saved game data found.");
        }
    }
     public void OnTopScoreClicked()
    {
        DisableMenuButtons();
        SceneManager.LoadSceneAsync("TopScore");
    }

    private void DisableMenuButtons()
    {
        this.newGameButton.interactable = false;
        this.continueGameButton.interactable = false;
    }
}
