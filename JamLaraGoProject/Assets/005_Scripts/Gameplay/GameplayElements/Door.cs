using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door : GameplayElement
{
    #region Fields

    private bool _isOpen;

    #endregion

    #region UnityInspector

    [SerializeField] private GameObject doorVisual;

    [SerializeField] private ModuleBehaviour moduleLockedInFront;
    [SerializeField] private ModuleBehaviour moduleLockedBehind;

    #endregion

    #region Behaviour

    private void Start()
    {
        SetDoorState(false);
    }

    public override void Activate()
    {
        SetDoorState(!_isOpen);
    }

    public override void Deactivate()
    {
        SetDoorState(!_isOpen);
    }

    public void GetModulesNeighbours(Vector3 _direction)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(_direction), out hit))
        {
            Debug.Log("Did Hit " + hit.collider.name);
        }
    }

    private void SetDoorState(bool value)
    {
        _isOpen = value;

        moduleLockedInFront.isLocked = !value;
        moduleLockedInFront.directionValueLocked = 3;

        moduleLockedBehind.isLocked = !value;
        moduleLockedBehind.directionValueLocked = 2;

        if (_isOpen)
        {
            doorVisual.transform.localRotation = Quaternion.Euler(0, 0, -90.0f);
        }
        else
        {
            doorVisual.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    

    #endregion
}
