using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class handles the button navigation for the Main Menu
/// </summary>
public class MainMenu : MonoBehaviour
{

    SceneHandler sceneHandler;

    [Header("Start Menu")]
    [SerializeField] private Button buttonPlatformerMode;
    [SerializeField] private Button buttonPuzzleMode;
    [SerializeField] private Button buttonOptions;
    [SerializeField] private Button buttonBackFromPlatformerLevels;
    [SerializeField] private Button buttonBackFromPuzzleLevels;

    [Header("Puzzle Level Page")]
    [SerializeField] private GameObject puzzleLevelButtonParent;
    [SerializeField] private LevelButton[] puzzleLevelButtons; //I am creating a list of all levels, then initalizing them in the start 
                                                                   //method(Hopefully this makes it so we just create another button and edit text without extra work)

    [Header("Platformer Level Page")]
    [SerializeField] private GameObject platformerLevelButtonParent;
    [SerializeField] private LevelButton[] platformerLevelButtons;


    
    [SerializeField] private GameObject activePage;
    // Start is called before the first frame update
    void Start()
    {
        sceneHandler = GameObject.FindGameObjectWithTag("Scene_Handler").GetComponent<SceneHandler>();

        buttonPlatformerMode.onClick.AddListener(delegate { ButtonPressed(buttonPlatformerMode.GetComponent<MenuButton>().GoTo); });
        buttonPuzzleMode.onClick.AddListener(delegate { ButtonPressed(buttonPuzzleMode.GetComponent<MenuButton>().GoTo); });
        buttonOptions.onClick.AddListener(delegate { ButtonPressed(buttonOptions.GetComponent<MenuButton>().GoTo); });
        buttonBackFromPlatformerLevels.onClick.AddListener(delegate { ButtonPressed(buttonBackFromPlatformerLevels.GetComponent<MenuButton>().GoTo); });
        buttonBackFromPuzzleLevels.onClick.AddListener(delegate { ButtonPressed(buttonBackFromPuzzleLevels.GetComponent<MenuButton>().GoTo); });

        puzzleLevelButtons = puzzleLevelButtonParent.GetComponentsInChildren<LevelButton>();

        
        for(int i = 0; i < puzzleLevelButtons.Length; i++)
        {
            LevelButton button = puzzleLevelButtons[i];
            button.GoTo = (Scenes)(i + 1);
            button.Button.onClick.AddListener(delegate { LevelButtonPressed(button.GoTo); });
        }

        platformerLevelButtons = platformerLevelButtonParent.GetComponentsInChildren<LevelButton>();

        for(int i = 0; i < platformerLevelButtons.Length; i++)
        {
            LevelButton button = platformerLevelButtons[i];
            button.GoTo = (Scenes)(i + 10001);
            button.Button.onClick.AddListener(delegate { LevelButtonPressed(button.GoTo); });
        }

    }
    
    /// <summary>
    /// This method is called when a menu button is pressed for navigation
    /// </summary>
    /// <param name="gameObject"></param>
    private void ButtonPressed(GameObject gameObject)
    {
        activePage.SetActive(false);
        activePage = gameObject;
        gameObject.SetActive(true);

    }

    /// <summary>
    /// This method is called when a menu button is pressed that goes to a certain level/scene
    /// </summary>
    /// <param name="scene"></param>
    public void LevelButtonPressed(Scenes scene)
    {
        sceneHandler.FadeTo(scene);
    }
}
