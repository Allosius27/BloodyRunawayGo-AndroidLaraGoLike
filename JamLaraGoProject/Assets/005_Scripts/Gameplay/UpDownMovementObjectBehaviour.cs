using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class UpDownMovementObjectBehaviour : MonoBehaviour
{
    [SerializeField] private int _upMovementsCost = 1;
    
    [SerializeField] private GameObject _upBlock;
    [SerializeField] private GameObject _downBlock;

    [SerializeField] private RectTransform _upArrowSprite = null;
    [SerializeField] private RectTransform _downArrowSprite = null;
    
    private TMP_Text _upMovementCostText = null;
    
    private void Awake()
    {
        _upMovementCostText = _upArrowSprite.GetComponentInChildren<TMP_Text>();
        _upMovementCostText.text = _upMovementsCost.ToString();
    }

    private float movableOffset = 0f;
    private float targetOffset = 10f;
    private void LateUpdate()
    {
        if (Camera.main == null) return;
        _upArrowSprite.position = Camera.main.WorldToScreenPoint(this._upBlock.transform.position) + new Vector3(0,10f + movableOffset,0);
        _downArrowSprite.position = Camera.main.WorldToScreenPoint(this._downBlock.transform.position) + new Vector3(0,10f + movableOffset,0);
        
        movableOffset = math.lerp(movableOffset, targetOffset, Time.deltaTime * 5f);

        if (movableOffset > targetOffset - 0.2f && targetOffset == 10f)
        {
            targetOffset = -10f;
        }
        else if(movableOffset < targetOffset + 0.2f && targetOffset == -10f)
        {
            targetOffset = 10f;
        }
    }

    public int GetMovementsCost()
    {
        return _upMovementsCost;
    }
}