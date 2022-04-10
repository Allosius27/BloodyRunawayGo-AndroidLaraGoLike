using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseLever : InteractibleObject
{
    #region Fields


    #endregion

    #region Properties

    public bool _isActive { get; protected set; }
    public Animator Animator => animator;

    public UnityEvent OnActivation => onActivation;
    public UnityEvent OnDeactivation => onDeactivation;

    #endregion

    #region UnityInspector

    [SerializeField] private Animator animator;


    [SerializeField] UnityEvent onActivation;
    [SerializeField] UnityEvent onDeactivation;

    [Space]

    [SerializeField] private AllosiusDev.FeedbacksData useLeverFeedbackData;

    #endregion

    #region Behaviour

    public virtual void Press()
    {
        ActiveLever();
    }

    public virtual void ActiveLever()
    {
        if (SetCurrentRangeModule())
        {

            ChangeState();
        }
    }

    public virtual void ChangeState()
    {
        Debug.Log("Active Lever");

        Animator.SetBool("LeverUp", !_isActive);

        if (_isActive == false)
        {
            Debug.Log("On Activation");
            StartCoroutine(useLeverFeedbackData.CoroutineExecute(this.gameObject));
            OnActivation.Invoke();
        }
        else
        {
            Debug.Log("On Deactivation");
            onDeactivation.Invoke();
        }

        _isActive = !_isActive;
    }

    #endregion
}
