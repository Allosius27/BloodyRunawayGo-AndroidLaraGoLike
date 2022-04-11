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

    [Space]

    [SerializeField] private AllosiusDev.AudioData mainMusicFeedbackData;

    [SerializeField] private AllosiusDev.FeedbacksData jingleLoseFeedbackData;
    [SerializeField] private AllosiusDev.FeedbacksData jingleWinFeedbackData;

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

    private void Start()
    {
        AllosiusDev.AudioManager.Play(mainMusicFeedbackData.sound);
    }


    public void GameOver()
    {
        if (gameEnd == false)
        {
            StartCoroutine(jingleLoseFeedbackData.CoroutineExecute(this.gameObject));
            SceneLoader.Instance.ActiveLoadingScreen(currentLevelData, 1.0f);
            gameEnd = true;
        }
    }

    public void LevelCompleted()
    {
        if (gameEnd == false)
        {
            StartCoroutine(jingleWinFeedbackData.CoroutineExecute(this.gameObject));

            for (int i = 0; i < UICanvasManager.Instance.SelectLevelPanel.UnlockLevels.Count; i++)
            {
                if (nextLevelData == UICanvasManager.Instance.SelectLevelPanel.UnlockLevels[i].levelData)
                {
                    UICanvasManager.Instance.SelectLevelPanel.UnlockLevels[i].isUnlocked = true;
                    UICanvasManager.Instance.SelectLevelPanel.SetLevelButtonState(UICanvasManager.Instance.SelectLevelPanel.LevelsButtons[i], UICanvasManager.Instance.SelectLevelPanel.UnlockLevels[i]);
                }
            }

            SceneLoader.Instance.ActiveLoadingScreen(nextLevelData, 1.0f);
            gameEnd = true;
        }
    }

    public void UpdateEnemiesBehaviour()
    {
        if (gameEnd == false)
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
    }

    #endregion
}
