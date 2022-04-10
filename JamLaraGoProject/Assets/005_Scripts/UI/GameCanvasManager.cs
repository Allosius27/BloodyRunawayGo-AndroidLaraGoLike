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

    public RectTransform UpArrowSprite => _upArrowSprite;

    public RectTransform DownArrowSprite => _downArrowSprite;

    #endregion

    #region UnityInspector

    [SerializeField] private TMP_Text _batMovementsCountText = null;

    [SerializeField] private Button _upMovementButton = null;

    [SerializeField] private RectTransform _upArrowSprite = null;
    [SerializeField] private RectTransform _downArrowSprite = null;

    #endregion

    #region Behaviour

    public void PauseMenu()
    {
        UICanvasManager.Instance.pauseMenu.MenuPause();
    }

    #endregion
}
