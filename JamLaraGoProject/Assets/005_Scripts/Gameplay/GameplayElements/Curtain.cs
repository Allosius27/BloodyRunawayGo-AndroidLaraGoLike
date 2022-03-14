using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curtain : MonoBehaviour
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

    public void SetCurtainState(bool value)
    {
        _isOpen = value;

        if(value)
        {
            animator.SetTrigger("Open");
        }
        else
        {
            animator.SetTrigger("Close");
            lightObj.DesactiveLight();
        }
    }

    #endregion
}
