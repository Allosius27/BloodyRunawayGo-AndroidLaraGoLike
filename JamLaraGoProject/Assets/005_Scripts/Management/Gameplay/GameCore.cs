using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCore : AllosiusDev.Singleton<GameCore>
{
    #region Fields

    private PlayerMovementController player;

    private List<Enemy> enemies = new List<Enemy>();

    #endregion

    #region Properties

    public PlayerMovementController Player => player;

    public List<Enemy> Enemies => enemies;

    public bool gameEnd { get; set; }

    #endregion

    #region UnityInspector

    [SerializeField] private SceneData currentLevelData;
    [SerializeField] private SceneData nextLevelData;

    #endregion

    #region Behaviour

    protected override void Awake()
    {
        base.Awake();

        var enemiesFound = FindObjectsOfType<Enemy>();
        for (int i = 0; i < enemiesFound.Length; i++)
        {
            enemies.Add(enemiesFound[i]);
        }

        player = FindObjectOfType<PlayerMovementController>();
    }


    public void GameOver()
    {
        if (gameEnd == false)
        {
            SceneLoader.Instance.ActiveLoadingScreen(currentLevelData, 1.0f);
            gameEnd = true;
        }
    }

    public void LevelCompleted()
    {
        if (gameEnd == false)
        {
            for (int i = 0; i < UICanvasManager.Instance.SelectLevelPanel.UnlockLevels.Count; i++)
            {
                if (nextLevelData == UICanvasManager.Instance.SelectLevelPanel.UnlockLevels[i].levelData)
                {
                    UICanvasManager.Instance.SelectLevelPanel.UnlockLevels[i].isUnlocked = true;
                }
            }

            SceneLoader.Instance.ActiveLoadingScreen(nextLevelData, 1.0f);
            gameEnd = true;
        }
    }

    public void UpdateEnemiesBehaviour()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            bool enemyTarget = enemies[i].UpdateEnemyBehaviour();
            if (enemyTarget == false)
            {
                enemies[i].CheckPlayerCanAttack();
            }
        }
    }

    #endregion
}
