using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class PauseMenu : MonoBehaviour
{
    #region Fields

    private GameObject settingsMenu;

    private SelectLevelPanel selectLevelPanel;


    #endregion

    #region Properties

    public static bool gameIsPaused = false;
    public static bool canPause = true;

    #endregion

    #region UnityInspector

    [SerializeField] private GameObject pauseMenuUI;

    [SerializeField] private SceneData mainMenuSceneData;

    #endregion

    #region Behaviour

    // Start is called before the first frame update
    void Start()
    {
        settingsMenu = UICanvasManager.Instance.SettingsMenu.gameObject;
        selectLevelPanel = UICanvasManager.Instance.SelectLevelPanel;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Escape"))
        {
            MenuPause();
        }
    }

    public void MenuPause()
    {
        if (canPause)
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Paused();
            }
        }
    }

    public void Paused()
    {
        //Afficher le menu pause
        pauseMenuUI.SetActive(true);
        // Arrêter le temps
        Time.timeScale = 0;
        // Changer le statut du jeu (l'état : pause ou jeu actif)
        gameIsPaused = true;

    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        settingsMenu.SetActive(false);

        Time.timeScale = 1;

        gameIsPaused = false;

    }

    public void LevelSelectionMenu()
    {
        selectLevelPanel.gameObject.SetActive(true);
    }

    public void LoadSettings()
    {
        Debug.Log("Loading Settings menu");
        settingsMenu.SetActive(true);
    }

    public void LoadMainMenu()
    {
        canPause = false;
        Resume();
        AllosiusDev.AudioManager.StopAllMusics();
        SceneLoader.Instance.ActiveLoadingScreen(mainMenuSceneData, 1.0f);
    }

    #endregion
}
