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

    private void Start()
    {
        GetModule();
    }

    public virtual void Press()
    {
        ActiveLever();
    }

    private void GetModule()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
        {
            Debug.Log("Did Hit " + hit.collider.name);
            ModuleBehaviour module = hit.collider.GetComponent<ModuleBehaviour>();
            if(module != null)
            {
                moduleAssociated = module;
            }
            else
            {
                moduleAssociated = null;
            }
        }
    }

    public void ActiveLever()
    {
        if (_isActive == false && GameCore.Instance.Player.CurrModule == moduleAssociated)
        {
            Debug.Log("Active Lever");

            _isActive = true;

            animator.SetBool("LeverUp", true);

            onPress.Invoke();
        }
    }

    #endregion
}
