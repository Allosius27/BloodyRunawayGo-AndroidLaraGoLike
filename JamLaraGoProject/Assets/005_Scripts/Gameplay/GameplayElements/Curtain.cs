using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curtain : GameplayElement
{
    #region Fields


    #endregion

    #region UnityInspector

    [SerializeField] private bool _isOpen;

    [SerializeField] private Animator animator;

    [SerializeField] private LightObject lightObj;

    #endregion

    #region Behaviour

    private void Start()
    {
        SetCurtainState(_isOpen);
    }

    public override void Activate()
    {
        SetCurtainState(!_isOpen);
    }

    public override void Deactivate()
    {
        SetCurtainState(!_isOpen);
    }

    private void SetCurtainState(bool value)
    {
        _isOpen = value;

        if(value)
        {
            animator.SetTrigger("Open");
            lightObj.gameObject.SetActive(true);
        }
        else
        {
            animator.SetTrigger("Close");
            lightObj.DeactiveLight();
        }
    }

   

    #endregion
}
