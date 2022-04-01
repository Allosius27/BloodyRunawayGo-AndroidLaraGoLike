using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovingModuleBehaviour : ModuleBehaviour
{
    #region Fields

    private int targetIndex;
    private Vector3 currentTarget;

    private Vector3 currentTargetA;
    private Vector3 currentTargetB;

    private bool isMoving;

    #endregion

    #region Properties

    public bool IsMoving => isMoving;

    public bool MoveLoop => moveLoop;

    #endregion

    #region UnityInspector

    [SerializeField] private bool moveLoop;

    [SerializeField] private float moveSpeed;

    [SerializeField] private Vector3Int targetA;
    [SerializeField] private Vector3Int targetB;

    #endregion

    #region Behaviour

    public override void Awake()
    {
        base.Awake();

        currentTargetA = transform.position + targetA;
        currentTargetB = transform.position + targetB;
    }

    public override void Start()
    {
        base.Start();

        currentTarget = currentTargetA;

        if(moveLoop)
        {
            Moving();
        }
        
    }

    public override void Activate()
    {
        SetMove();
    }

    public override void Deactivate()
    {
        SetMove();
    }

    private void SetMove()
    {
        Debug.Log("StopMove");

        transform.DOKill();

        if (isMoving)
        {
            EndMove();
        }

        Moving();
    }

    private void Moving()
    {
        Debug.Log("Moving");

        isMoving = true;
        CheckCurrentNeighbors();

        float duration = Vector3.Distance(currentTarget, transform.position);
        transform.DOLocalMove(currentTarget, duration * (1/moveSpeed)).SetEase(Ease.Linear).OnComplete(EndMove);
    }

    private void EndMove()
    {
        Debug.Log("EndMove");

        isMoving = false;

        if (targetIndex == 0)
        {
            currentTarget = currentTargetB;
            targetIndex = 1;
        }
        else if (targetIndex == 1)
        {
            currentTarget = currentTargetA;
            targetIndex = 0;
        }

        if (moveLoop)
        {
            Moving();
        }
        else
        {
            CheckCurrentNeighbors();

            SetAnchorPos();
        }
    }

    private void CheckCurrentNeighbors()
    {
        RegisterNeighbors();
        for (int i = 0; i < _neighbors.Length; i++)
        {
            if (_neighbors[i] != null)
            {
                _neighbors[i].RegisterNeighbors();
            }
        }
    }

    #endregion

    #region Gizmos

    public override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();

        Gizmos.color = Color.red;

        if (Application.isPlaying)
        {
            Gizmos.DrawSphere(currentTargetA, 0.3f);
            Gizmos.DrawSphere(currentTargetB, 0.3f);
        }
        else
        {
            Gizmos.DrawSphere(transform.position + targetA, 0.3f);
            Gizmos.DrawSphere(transform.position + targetB, 0.3f);
        }
    }

    #endregion
}
