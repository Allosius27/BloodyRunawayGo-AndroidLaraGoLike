using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour
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

    public void GetModulesNeighbours(Vector3 _direction)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(_direction), out hit))
        {
            Debug.Log("Did Hit " + hit.collider.name);
        }
    }

    public void SetDoorState(bool value)
    {
        _isOpen = value;

        moduleLockedInFront.isLocked = !value;
        moduleLockedInFront.directionValueLocked = 3;

        moduleLockedBehind.isLocked = !value;
        moduleLockedBehind.directionValueLocked = 2;

        if (_isOpen)
        {
            doorVisual.transform.Rotate(0.0f, 0.0f, -90.0f, Space.Self);
        }
        else
        {
            doorVisual.transform.Rotate(0.0f, 0.0f, 0.0f, Space.Self);
        }
    }

    #endregion
}
