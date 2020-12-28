using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class will Handle the Pause Functionality, pausing the game 
/// and handling pause Menu button presses
/// </summary>
public class PauseMenu : MonoBehaviour
{
    [Header("Menu Buttons")]
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button retryButton;

    [Header("Needed Components")]
    [SerializeField] private SceneHandler sceneHandler;
    [SerializeField] private GameObject pauseMenuCanvas;

    private bool gamePause = false;

    // Start is called before the first frame update
    void Start()
    {
        mainMenuButton.onClick.AddListener(MainMenuButtonClicked);
        retryButton.onClick.AddListener(RetryButtonClicked);
        pauseMenuCanvas.SetActive(gamePause);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && sceneHandler.CurrentScene != Scenes.Main_Menu)
        {
            gamePause = !gamePause;
            pauseMenuCanvas.SetActive(gamePause);
            
        }
    }

    private void MainMenuButtonClicked()
    {
        sceneHandler.FadeTo(Scenes.Main_Menu);

        gamePause = false;
        pauseMenuCanvas.SetActive(gamePause);
    }

    private void RetryButtonClicked()
    {
        sceneHandler.FadeTo(sceneHandler.CurrentScene);

        gamePause = false;
        pauseMenuCanvas.SetActive(gamePause);
    }
}
