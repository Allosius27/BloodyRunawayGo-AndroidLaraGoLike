using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMovementController : MonoBehaviour
{
    #region Fields  

    private ModuleBehaviour _currModule = null;
    
    private MovementDirection _movementDirection = MovementDirection.None;

    private Vector3 _beganPosition = Vector3.zero;
    private Vector3 _endedPosition = Vector3.zero;

    private Vector3? _targetPos = null;

    private BatMovement _batMovement = null;

    private int[] _batMovementsCosts = new int[4];
    
    #endregion

    #region Properties

    private enum MovementDirection { None, Up, Down, Right, Left }

    private readonly Vector3?[] _possibleTargets = new Vector3?[4];

    public ModuleBehaviour CurrModule => _currModule;

    #endregion

    #region UnityInspector

    [SerializeField] private float _movementSpeed = 5f;

    #endregion

    #region Behaviour

    private void Awake()
    {
        _batMovement = this.gameObject.GetComponent<BatMovement>();
    }

    private void Start()
    {
        CheckForCurrModule();
    }

    private void Update()
    {
        UpdateInput();
        UpdateMovements();
    }
    private void UpdateInput()
    {
        if (_targetPos != null) return;

        int currBatMovement = _batMovement.GetCurrBatMovement();
        
        GetKeyboardInputs();

        GetMobileInputs();

        if (_movementDirection == MovementDirection.Up)
        {
            if (_batMovementsCosts[0] <= currBatMovement)
            {
                SetMovement(0);
            }

        }
        else if (_movementDirection == MovementDirection.Down)
        {
            if (_batMovementsCosts[1] <= currBatMovement)
            {
                SetMovement(1);
            }
        }
        else if (_movementDirection == MovementDirection.Right)
        {
            if (_batMovementsCosts[2] <= currBatMovement)
            {
                SetMovement(2);
            }
        }
        else if (_movementDirection == MovementDirection.Left)
        {
            if (_batMovementsCosts[3] <= currBatMovement)
            {
                SetMovement(3);
            }
        }
    }

    private void GetKeyboardInputs()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            _movementDirection = MovementDirection.Up;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            _movementDirection = MovementDirection.Down;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            _movementDirection = MovementDirection.Right;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {

            _movementDirection = MovementDirection.Left;
        }
    }

    private void GetMobileInputs()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                //Debug.Log("Began " + touch.position);
                _beganPosition = touch.position;
            }
            if (touch.phase == TouchPhase.Moved)
            {
                //Debug.Log("Moved " + touch.position);
            }
            if (touch.phase == TouchPhase.Ended)
            {
                //Debug.Log("Ended " + touch.position);
                _endedPosition = touch.position;

                Vector3 direction = _endedPosition - _beganPosition;
                //Debug.Log("direction : " + direction);

                if (direction.x > 0 && direction.y > 0)
                {
                    Debug.Log("RIGHT");
                    _movementDirection = MovementDirection.Right;
                }

                if (direction.x > 0 && direction.y < 0)
                {
                    Debug.Log("DOWN");
                    _movementDirection = MovementDirection.Down;
                }

                if (direction.x < 0 && direction.y < 0)
                {
                    Debug.Log("LEFT");
                    _movementDirection = MovementDirection.Left;
                }

                if (direction.x < 0 && direction.y > 0)
                {
                    Debug.Log("UP");
                    _movementDirection = MovementDirection.Up;
                }
            }
        }
    }

    private void UpdateMovements()
    {
        if (_targetPos == null) return;

        Vector3 direction = _targetPos.Value - transform.position;
        float moveStep = _movementSpeed * Time.deltaTime;

        if(moveStep > direction.sqrMagnitude)
        {
            transform.DOMove(_targetPos.Value, 0.125f);
            OnMovementEnd();
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPos.Value, _movementSpeed * Time.deltaTime);
        }
    }

    private void SetMovement(int value)
    {
        //Debug.Log("SetMovement");
        if (_possibleTargets[value] != null)
        {
            _batMovement.ChangeBatMovementCount(-_batMovementsCosts[value]);
            _batMovementsCosts = new int[4];
            
            SetTargetPos(_possibleTargets[value].Value);
        }
    }

    private void OnMovementEnd()
    {
        Debug.Log("OnMovementEnd");
        _targetPos = null;
        _movementDirection = MovementDirection.None;
        CheckForCurrModule();
    }

    private void SetCurrModule(ModuleBehaviour moduleBehaviour)
    {
        _currModule.CheckCurrentPlayerModule(_currModule);

        _currModule = moduleBehaviour;
        Debug.Log("new module : " + _currModule.name);

        _currModule.CheckCurrentPlayerModule(_currModule);

        SetPossibleTargetsPos();
    }

    private void SetTargetPos(Vector3 pos)
    {
        _targetPos = pos;
    }

    private void SetPossibleTargetsPos()
    {
        if (_currModule == null) { Debug.LogWarning("NoCurrentModule"); return; }

        ModuleBehaviour[] neighbors = _currModule.GetNeighbors();

        //Forward
        SetPossibleTargetPos(0, neighbors);
        //BackWard
        SetPossibleTargetPos(1, neighbors);
        //Right
        SetPossibleTargetPos(2, neighbors);
        //Left
        SetPossibleTargetPos(3, neighbors);
    }

    private void SetPossibleTargetPos(int value, ModuleBehaviour[] neighbors)
    {
        if (neighbors[value] == null || (neighbors[value].isLocked && neighbors[value].directionValueLocked == value))
        {
            Debug.Log(value);
            _possibleTargets[value] = null;
        }
        else
        {
            Vector3? anchorPos = neighbors[value].GetAnchorPos();

            if (anchorPos != null)
            {
                _possibleTargets[value] = anchorPos;
            }
            else
            {
                _batMovementsCosts[value]++;
                SetPossibleTargetPos(value, neighbors[value].GetNeighbors());
            }
        }
    }

    private void CheckForCurrModule()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1f))
        {
            if (hit.collider.TryGetComponent<ModuleBehaviour>(out ModuleBehaviour module))
            {
                SetCurrModule(module);
            }
        }
    }
}

#endregion
