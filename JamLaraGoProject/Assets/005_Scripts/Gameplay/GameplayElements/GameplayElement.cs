using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameplayElement : MonoBehaviour
{
    #region Properties

    public Entity entity { get; protected set; }

    #endregion

    #region Behaviour

    public abstract void Activate();

    public abstract void Deactivate();

    #endregion
}
