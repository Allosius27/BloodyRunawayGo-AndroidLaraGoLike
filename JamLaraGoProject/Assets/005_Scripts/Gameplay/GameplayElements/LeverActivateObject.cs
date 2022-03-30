using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverActivateObject : MonoBehaviour
{
    #region UnityInspector

    [SerializeField] private ModuleBehaviour moduleAssociated;
    [SerializeField] private ConditionLever leverAssociated;

    #endregion

    #region Behaviour

    public void Use()
    {
        if(moduleAssociated != null && leverAssociated != null && GameCore.Instance.Player.CurrModule == moduleAssociated)
        {
            leverAssociated.canUse = true;

            Destroy(gameObject);
        }
    }

    #endregion
}
