using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameCanvasManager : AllosiusDev.Singleton<GameCanvasManager>
{
    #region Properties

    public TMP_Text BatMovementsCountText => _batMovementsCountText;

    public Button UpMovementButton => _upMovementButton;

    #endregion

    #region UnityInspector

    [SerializeField] private TMP_Text _batMovementsCountText = null;

    [SerializeField] private Button _upMovementButton = null;

    #endregion

    #region Behaviour

    public void PauseMenu()
    {
        UICanvasManager.Instance.pauseMenu.MenuPause();
    }

    #endregion
}
