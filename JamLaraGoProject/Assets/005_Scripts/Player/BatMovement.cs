using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class BatMovement : MonoBehaviour
{
    [SerializeField] private TMP_Text _batMovementsCountText = null;

    private int _currBatMovement = 0;

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

    public void ChangeBatMovementCount(int value)
    {
        _currBatMovement += value;
        _batMovementsCountText.text = _currBatMovement.ToString();
    }
}
