using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class FinishGame : MonoBehaviour
{
    [Header("Menu Buttons")]

    [SerializeField] private Button newGameButton;
     private void Start()
    {
       if(!DataPersistenceManager.instance.HasGameData())
       {
            Debug.Log("No saved game data found.");
       }
    }
    public void OnNewGameClicked()
    {
        DisableMenuButtons();
        Debug.Log("New Game Clicked");
        // Load the game play scene - which will in turn save the game because of
        // OnSceneUnloaded() in the data persistence manager
        DataPersistenceManager.instance.NewGame();
        SceneManager.LoadSceneAsync("Level 1");
    }
    private void DisableMenuButtons()
    {
        this.newGameButton.interactable = false;
    }

}
