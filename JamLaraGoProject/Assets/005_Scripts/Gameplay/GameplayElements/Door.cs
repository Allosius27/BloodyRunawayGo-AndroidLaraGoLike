using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

public class Door : GameplayElement
{
    #region Fields

    private bool _isOpen;

    private ModuleBehaviour moduleLockedInFront;
    private ModuleBehaviour moduleLockedBehind;

    private bool upLocked;
    private bool downLocked;
    private bool rightLocked;
    private bool leftLocked;

    #endregion

    #region UnityInspector

    [SerializeField] private GameObject doorVisual;


    [SerializeField] private Transform moduleDetectPointFront;
    [SerializeField] private Transform moduleDetectPointBehind;

    #endregion

    #region Behaviour

    private void Start()
    {
        SetDoorState(false);

        GetModulesLocked();
    }

    public void GetModulesLocked()
    {
        Collider[] hitsFront = Physics.OverlapSphere(moduleDetectPointFront.position, 0.3f);
        for (int i = 0; i < hitsFront.Length; i++)
        {
            Debug.Log(hitsFront[i].name);

            ModuleBehaviour module = hitsFront[i].GetComponent<ModuleBehaviour>();
            if (module != null)
            {
                moduleLockedInFront = module;

                float distanceX = moduleDetectPointFront.position.x - module.transform.position.x;
                if (distanceX >= 0.2f)
                {
                    module.rightDirectionLocked = true;
                    rightLocked = true;
                }

                float distanceZ = moduleDetectPointFront.position.z - module.transform.position.z;
                if (distanceZ >= 0.2f)
                {
                    module.upDirectionLocked = true;
                    upLocked = true;
                }
            }
        }

        Collider[] hitsBehind = Physics.OverlapSphere(moduleDetectPointBehind.position, 0.3f);
        for (int i = 0; i < hitsBehind.Length; i++)
        {
            Debug.Log(hitsBehind[i].name);

            ModuleBehaviour module = hitsBehind[i].GetComponent<ModuleBehaviour>();
            if (module != null)
            {
                moduleLockedBehind = module;

                float distanceX = module.transform.position.x - moduleDetectPointBehind.position.x;
                if (distanceX >= 0.2f)
                {
                    module.leftDirectionLocked = true;
                    leftLocked = true;
                }

                float distanceZ = module.transform.position.z - moduleDetectPointBehind.position.z;
                if (distanceZ >= 0.2f)
                {
                    module.downDirectionLocked = true;
                    downLocked = true;
                }
            }
        }
    }

    public override void Activate()
    {
        SetDoorState(true);
    }

    public override void Deactivate()
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

    private void SetDoorState(bool value)
    {
        _isOpen = value;

        if(rightLocked)
        {
            moduleLockedInFront.rightDirectionLocked = !_isOpen;
        }
        if(upLocked)
        {
            moduleLockedInFront.upDirectionLocked = !_isOpen;
        }

        if (leftLocked)
        {
            moduleLockedBehind.leftDirectionLocked = !_isOpen;
        }
        if (downLocked)
        {
            moduleLockedBehind.downDirectionLocked = !_isOpen;
        }

        if (_isOpen)
        {
            doorVisual.transform.localRotation = Quaternion.Euler(-90.0f, -90.0f, 0f);
        }
        else
        {
            doorVisual.transform.localRotation = Quaternion.Euler(-90.0f, 0, 0);
        }
    }



    #endregion

    #region Gizmos

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(moduleDetectPointFront.position, 0.3f);
        Gizmos.DrawWireSphere(moduleDetectPointBehind.position, 0.3f);
    }

    #endregion
}
