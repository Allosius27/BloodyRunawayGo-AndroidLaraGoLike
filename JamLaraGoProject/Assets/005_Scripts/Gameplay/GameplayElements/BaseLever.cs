using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseLever : MonoBehaviour
{
    #region Fields

    private bool _isActive;

    #endregion

    #region UnityInspector

    [SerializeField] private Animator animator;

    [SerializeField] private ModuleBehaviour moduleAssociated;

    [SerializeField] UnityEvent onPress;

    #endregion

    #region Behaviour

    public void Press()
    {
        if (_isActive == false && GameCore.Instance.Player.CurrModule == moduleAssociated)
        {
            _isActive = true;

            animator.SetBool("LeverUp", true);

            onPress.Invoke();
        }
    }

    #endregion
}
