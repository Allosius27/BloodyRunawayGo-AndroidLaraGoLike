using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class UpDownMovementObjectBehaviour : MonoBehaviour
{
    private RectTransform _arrowSprite = null;
    
    private void Awake()
    {
        _arrowSprite = GameObject.Find("GoUpParent").transform.GetComponent<RectTransform>();
    }

    private float movableOffset = 0f;
    private float targetOffset = 10f;
    private void LateUpdate()
    {
        if (Camera.main != null)
            _arrowSprite.position = Camera.main.WorldToScreenPoint(this.gameObject.transform.position) + new Vector3(0,10f + movableOffset,0);

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
}
