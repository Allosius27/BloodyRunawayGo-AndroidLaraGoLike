using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectLevelPanel : MonoBehaviour
{
    #region UnityInspector

    [SerializeField] private List<SceneData> levelsDatas;

    [SerializeField] private LevelButton prefabLevelButton;

    [SerializeField] private Transform levelsButtonsParent;

    #endregion

    #region Behaviour

    private void Start()
    {
        for (int i = 0; i < levelsDatas.Count; i++)
        {
            LevelButton _levelButton = Instantiate(prefabLevelButton);
            _levelButton.transform.SetParent(levelsButtonsParent);
            _levelButton.transform.localPosition = Vector3.zero;

            _levelButton.levelData = levelsDatas[i];
        }
    }

    #endregion
}
