using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    #region Fields

    private Color baseColor;

    #endregion

    #region Properties

    public Color BaseColor => baseColor;

    public Color LockedColor => lockedColor;

    public bool isUnlocked { get; set; }

    public SelectLevelPanel selectLevelPanel { get; set; }

    public int levelDataIndex { get; set; }

    public Text TextLabel => textLabel;
    public Image ImgLevel => imgLevel;

    public GameObject ActiveObj => activeObj;

    #endregion

    #region UnityInspector

    [SerializeField] private Text textLabel;
    [SerializeField] private Image imgLevel;

    [SerializeField] private Color lockedColor;

    [SerializeField] private GameObject activeObj;

    #endregion

    #region Behaviour

    private void Awake()
    {
        baseColor = imgLevel.color;
    }

    private void Start()
    {
        activeObj.SetActive(false);

        SetButtonState();
    }

    public void SetButtonState()
    {
        Button button = GetComponent<Button>();
        if (isUnlocked)
        {
            imgLevel.color = baseColor;
            button.interactable = true;
        }
        else
        {
            imgLevel.color = lockedColor;
            button.interactable = false;
        }
    }

    public void SelectLeverButton()
    {
        selectLevelPanel.SetCurrentLevelSelected(this);
    }

    #endregion
}
