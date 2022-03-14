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
        _batMovementsCountText.text = _currBatMovement.ToString("0");
    }

    public int GetCurrBatMovement()
    {
        return _currBatMovement;
    }
    
    private void ChangeBatMovementCount(int value)
    {
        _currBatMovement += value;
    }
}
