using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCore : AllosiusDev.Singleton<GameCore>
{
    #region Properties

    public PlayerMovementController Player => player;

    public List<Enemy> Enemies => enemies;

    #endregion

    #region UnityInspector

    [SerializeField] private SceneData currentLevelData;

    [SerializeField] private PlayerMovementController player;

    [SerializeField] private List<Enemy> enemies = new List<Enemy>();

    #endregion

    #region Behaviour

    

    public void GameOver()
    {
        SceneLoader.Instance.ActiveLoadingScreen(currentLevelData, 1.0f);
    }

    public void UpdateEnemiesBehaviour()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].UpdateEnemyTargets();
            enemies[i].CheckPlayerCanAttack();
        }
    }

    #endregion
}
