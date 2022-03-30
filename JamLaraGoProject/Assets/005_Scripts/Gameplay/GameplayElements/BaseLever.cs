using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseLever : GameplayElement
{
    #region Fields

    private bool _isActive;

    #endregion

    #region UnityInspector

    [SerializeField] private Animator animator;


    [SerializeField] UnityEvent onPress;

    #endregion

    #region Behaviour

    public virtual void Press()
    {
        ActiveLever();
    }

    public void ActiveLever()
    {
        if (_isActive == false && SetCurrentRangeModule())
        {
            Debug.Log("Active Lever");

            _isActive = true;

            animator.SetBool("LeverUp", true);

            onPress.Invoke();
        }
    }

    #endregion
}
