using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionLever : BaseLever
{
    #region Properties

    public bool canUse { get; set; }

    #endregion

    #region Behaviour

    public override void Press()
    {
        if(canUse)
        {
            Debug.Log("ActiveLever");
            ActiveLever();
        }
    }

    #endregion
}
