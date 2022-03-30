using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverActivateObject : GameplayElement
{
    #region UnityInspector

    [SerializeField] private ConditionLever leverAssociated;

    #endregion

    #region Behaviour

    public void Use()
    {
        if(moduleAssociated != null && leverAssociated != null && SetCurrentRangeModule())
        {
            leverAssociated.canUse = true;

            Destroy(gameObject);
        }
    }

    #endregion
}
