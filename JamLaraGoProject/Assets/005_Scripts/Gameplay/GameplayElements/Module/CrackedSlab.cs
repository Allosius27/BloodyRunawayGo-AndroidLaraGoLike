using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackedSlab : ModuleBehaviour
{
    #region Fields

    private int currentDurability;

    #endregion

    #region Properties


    #endregion

    #region UnityInspector

    [SerializeField] private GameObject graphics;
    [SerializeField] private Collider colliderObj;

    [SerializeField] private int maxDurability = 3;

    #endregion

    #region Behaviour

    public override void Start()
    {
        base.Start();

        currentDurability = maxDurability;
    }

    public override void OnWalked()
    {
        if(currentDurability <= 0)
        {
            return;
        }

        currentDurability -= 1;

        if(currentDurability <= 0)
        {
            currentDurability = 0;
            graphics.SetActive(false);
            colliderObj.enabled = false;
            GameCore.Instance.Player.fall = true;
        }
    }

    #endregion
}
