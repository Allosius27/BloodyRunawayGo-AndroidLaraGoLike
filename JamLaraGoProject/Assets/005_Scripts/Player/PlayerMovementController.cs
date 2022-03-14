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

    #endregion

    #region Properties

    private readonly Vector3?[] _possibleTargets = new Vector3?[4];

    #endregion

    #region UnityInspector

    [SerializeField] private float _movementSpeed = 5f;

    #endregion

    #region Behaviour

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

        GetKeyboardInputs();

        GetMobileInputs();

        if (_movementDirection == MovementDirection.Up)
        {
            SetMovement(0);
        }
        else if (_movementDirection == MovementDirection.Down)
        {
            SetMovement(1);
        }
        else if (_movementDirection == MovementDirection.Right)
        {
            SetMovement(2);
        }
        else if (_movementDirection == MovementDirection.Left)
        {
            SetMovement(3);
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
            transform.DOMove(_targetPos.Value, 0.25f);
            OnMovementEnd();
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPos.Value, _movementSpeed * Time.deltaTime);
        }
    }

    private void SetMovement(int value)
    {
        Debug.Log("SetMovement");
        if (_possibleTargets[value] != null)
        {
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
        _currModule = moduleBehaviour;
        Debug.Log("new module : " + _currModule.name);

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
        if (neighbors[value] != null)
        {
            Vector3? anchorPos = neighbors[value].GetAnchorPos();

            if (anchorPos != null)
            {
                _possibleTargets[value] = anchorPos;
            }
            else
            {
                SetPossibleTargetPos(value, neighbors[value].GetNeighbors());
            }
        }
        else
        {
            _possibleTargets[value] = null;
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


public enum MovementDirection { None, Up, Down, Right, Left }
