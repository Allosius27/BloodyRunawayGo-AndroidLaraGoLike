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
        {
            Debug.Log("Stop Coroutine");
            StopCoroutine(coroutineActivationDuration);
        }

        if (_isActive == false)
        {
            Debug.Log("Coroutine Activation Duration");

            coroutineActivationDuration = CoroutineActivationDuration();
            StartCoroutine(coroutineActivationDuration);
        }
        else
        {
            Debug.Log("Active Lever");

            base.ActiveLever();
        }
        
    }

    private IEnumerator CoroutineActivationDuration()
    {
        Debug.Log("Activation Duration");

        base.ActiveLever();

        yield return new WaitForSeconds(activationDuration);

        base.ActiveLever();
    }

    #endregion
}
