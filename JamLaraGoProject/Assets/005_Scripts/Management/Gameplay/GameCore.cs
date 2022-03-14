using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCore : AllosiusDev.Singleton<GameCore>
{
    #region Properties

    public PlayerMovementController Player => player;

    #endregion

    #region UnityInspector

    [SerializeField] private SceneData currentLevelData;

    [SerializeField] private PlayerMovementController player;

    #endregion

    #region Behaviour

    

    public void GameOver()
    {
        SceneLoader.Instance.ActiveLoadingScreen(currentLevelData, 1.0f);
    }

    #endregion
}
