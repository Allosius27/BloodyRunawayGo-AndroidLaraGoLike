using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curtain : MonoBehaviour
{
    #region Fields

    private bool _isOpen;

    #endregion

    #region UnityInspector

    [SerializeField] private Animator animator;

    [SerializeField] private GameObject lightObj;

    #endregion

    #region Behaviour

    public void SetCurtainState(bool value)
    {

    }

    #endregion
}
