using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCore : AllosiusDev.Singleton<GameCore>
{
    #region UnityInspector

    [SerializeField] private SceneData currentLevelData;

    #endregion

    #region Behaviour

    public void GameOver()
    {
        SceneLoader.Instance.ActiveLoadingScreen(currentLevelData, 1.0f);
    }

    #endregion
}
