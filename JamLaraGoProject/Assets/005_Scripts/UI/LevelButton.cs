using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButton : MonoBehaviour
{
    #region Properties

    public SelectLevelPanel selectLevelPanel { get; set; }

    public SceneData levelData { get; set; }

    #endregion

    #region Behaviour

    public void LaunchLevel(float _timeToWait)
    {
        selectLevelPanel.gameObject.SetActive(false);
        PauseMenu.canPause = true;
        SceneLoader.Instance.ActiveLoadingScreen(levelData, _timeToWait);
    }

    #endregion
}
