using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class BatMovement : MonoBehaviour
{
    #region Fields

    private int _currBatMovement = 0;

    #endregion

    #region Properties

    public AllosiusDev.FeedbacksData GetBloodBottleFeedbackData => getBloodBottleFeedbackData;

    #endregion

    #region UnityInspector

    [SerializeField] private TMP_Text _batMovementsCountText = null;

    [Space]

    [SerializeField] private AllosiusDev.FeedbacksData getBloodBottleFeedbackData;

    #endregion


    private void Awake()
    {
        if (_batMovementsCountText == null)
        {
            _batMovementsCountText = GameCanvasManager.Instance.BatMovementsCountText;
        }

        _batMovementsCountText.text = _currBatMovement.ToString();
    }

    public int GetCurrBatMovement()
    {
        return _currBatMovement;
    }

    public void PlayGetBloodBottleFeedback()
    {
        StartCoroutine(getBloodBottleFeedbackData.CoroutineExecute(this.gameObject));
    }

    public void ChangeBatMovementCount(int value)
    {
        _currBatMovement += value;
        _batMovementsCountText.text = _currBatMovement.ToString();
    }
}
