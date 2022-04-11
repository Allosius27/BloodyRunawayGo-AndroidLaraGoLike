using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    #region UnityInspector

    [SerializeField] private PlayerMovementController playerMovementController;

    [Space]

    [SerializeField] private AllosiusDev.FeedbacksData footstepFeedbackData;

    #endregion

    #region Behaviour

    public void Footstep()
    {
        StartCoroutine(footstepFeedbackData.CoroutineExecute(this.gameObject));
    }

    public void EndAttack()
    {
        playerMovementController.SendEnemyDamage();
    }

    public void EndDeath()
    {
        if (GameCore.Instance != null && GameCore.Instance.gameEnd == false)
            GameCore.Instance.GameOver();
    }

    #endregion
}
