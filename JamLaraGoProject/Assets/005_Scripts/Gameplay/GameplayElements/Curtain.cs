using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curtain : GameplayElement
{
    #region Fields


    #endregion

    #region UnityInspector

    [SerializeField] private bool _isOpen;

    [SerializeField] private Animator[] animators;

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
            for (int i = 0; i < animators.Length; i++)
            {
                animators[i].SetTrigger("Open");
            }
            lightObj.gameObject.SetActive(true);
        }
        else
        {
            for (int i = 0; i < animators.Length; i++)
            {
                animators[i].SetTrigger("Close");
            }
            lightObj.DeactiveLight();
        }
    }

   

    #endregion
}
