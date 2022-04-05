using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvasManager : AllosiusDev.Singleton<UICanvasManager>
{
    #region Properties
    public SettingsMenu SettingsMenu => settingsMenu;

    public SelectLevelPanel SelectLevelPanel => selectLevelPanel;

    public PauseMenu pauseMenu { get; protected set; }

    #endregion

    #region UnityInspector

    [SerializeField] private SettingsMenu settingsMenu;

    [SerializeField] private SelectLevelPanel selectLevelPanel;

    #endregion

    #region Behaviour

    private void Start()
    {
        pauseMenu = GetComponent<PauseMenu>();
    }

    #endregion
}
