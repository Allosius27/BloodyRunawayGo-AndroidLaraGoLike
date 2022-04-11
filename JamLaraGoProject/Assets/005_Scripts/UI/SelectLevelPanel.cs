using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectLevelPanel : MonoBehaviour
{
    #region Class

    [System.Serializable]
    public class UnlockLevel
    {
        public SceneData levelData;
        public bool isUnlocked;
    }

    #endregion

    #region Fields

    private int currentLevelSelectedIndex;
    private LevelButton currentLevelButtonActive;

    private List<LevelButton> levelsButtons = new List<LevelButton>();

    #endregion

    #region Properties

    public MainMenu mainMenu { get; set; }

    public List<UnlockLevel> UnlockLevels => unlockLevels;

    public List<LevelButton> LevelsButtons => levelsButtons;

    #endregion

    #region UnityInspector

    [SerializeField] private List<UnlockLevel> unlockLevels = new List<UnlockLevel>();

    [SerializeField] private LevelButton prefabLevelButton;

    [SerializeField] private Transform levelsButtonsParent;

    #endregion

    #region Behaviour

    public void Init()
    {
        for (int i = 0; i < unlockLevels.Count; i++)
        {
            LevelButton _levelButton = Instantiate(prefabLevelButton, levelsButtonsParent.transform.position, levelsButtonsParent.transform.rotation);
            _levelButton.transform.SetParent(levelsButtonsParent);
            _levelButton.transform.localPosition = Vector3.zero;
            _levelButton.transform.localScale = Vector3.one;

            _levelButton.levelDataIndex = i;

            _levelButton.TextLabel.text = unlockLevels[i].levelData.sceneName;
            _levelButton.ImgLevel.sprite = unlockLevels[i].levelData.sceneImg;

            _levelButton.selectLevelPanel = this;

            SetLevelButtonState(_levelButton, unlockLevels[i]);

            levelsButtons.Add(_levelButton);
        }
    }

    public void SetLevelButtonState(LevelButton levelButton, UnlockLevel unlockLevel)
    {
        levelButton.isUnlocked = unlockLevel.isUnlocked;
        levelButton.SetButtonState();
    }

    public void SetCurrentLevelSelected(LevelButton levelButton)
    {
        if(currentLevelButtonActive != null)
        {
            currentLevelButtonActive.ActiveObj.SetActive(false);
        }

        currentLevelSelectedIndex = levelButton.levelDataIndex;

        currentLevelButtonActive = levelButton;
        currentLevelButtonActive.ActiveObj.SetActive(true);
    }

    public void StartLevel()
    {
        if(currentLevelButtonActive != null)
        {
            if (mainMenu != null)
            {
                SceneLoader.Instance.ActiveLoadingScreen(unlockLevels[currentLevelSelectedIndex].levelData, 1.0f);
                PauseMenu.canPause = true;
                CloseMenu();
            }
            else if(PauseMenu.gameIsPaused)
            {
                UICanvasManager.Instance.pauseMenu.Resume();
                AllosiusDev.AudioManager.StopAllMusics();
                SceneLoader.Instance.ActiveLoadingScreen(unlockLevels[currentLevelSelectedIndex].levelData, 1.0f);
                CloseMenu();
            }
        }
        
    }

    public void CloseMenu()
    {
        gameObject.SetActive(false);
    }

    #endregion
}
