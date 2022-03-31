using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerButton : BaseLever
{
    #region Fields

    private IEnumerator coroutineActivationDuration;

    #endregion

    #region UnityInspector

    [SerializeField] private float activationDuration = 5.0f;

    #endregion

    #region Behaviour

    public override void ActiveLever()
    {
        if (coroutineActivationDuration != null)
            StopCoroutine(coroutineActivationDuration);

        coroutineActivationDuration = CoroutineActivationDuration();
        StartCoroutine(CoroutineActivationDuration());
    }

    private IEnumerator CoroutineActivationDuration()
    {
        Debug.Log("Coroutine Activation Duration");

        base.ActiveLever();

        yield return new WaitForSeconds(activationDuration);

        base.ActiveLever();
    }

    #endregion
}
