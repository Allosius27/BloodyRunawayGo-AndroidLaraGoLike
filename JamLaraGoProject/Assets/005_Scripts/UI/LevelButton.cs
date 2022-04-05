using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    #region Properties

    public SelectLevelPanel selectLevelPanel { get; set; }

    public int levelDataIndex { get; set; }

    public Text TextLabel => textLabel;
    public Image ImgLevel => imgLevel;

    public GameObject ActiveObj => activeObj;

    #endregion

    #region UnityInspector

    [SerializeField] private Text textLabel;
    [SerializeField] private Image imgLevel;

    [SerializeField] private GameObject activeObj;

    #endregion

    #region Behaviour

    private void Start()
    {
        activeObj.SetActive(false);
    }

    public void SelectLeverButton()
    {
        selectLevelPanel.SetCurrentLevelSelected(this);
    }

    #endregion
}
