using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : BaseLever
{
    #region Behaviour

    public override void Start()
    {
        base.Start();

        moduleAssociated.pressurePlateAssociated = this;
    }

    public override void Press()
    {
        
    }

    public void PlayerPress()
    {
        ActiveLever();
    }

    #endregion
}
