using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvasManager : AllosiusDev.Singleton<UICanvasManager>
{
    #region UnityInspector

    public SettingsMenu SettingsMenu => settingsMenu;
    [SerializeField] private SettingsMenu settingsMenu;

    #endregion
}
