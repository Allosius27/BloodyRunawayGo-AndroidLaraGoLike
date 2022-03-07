using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 5f;

    private ModuleBehaviour _currModule = null;

    private MovementState _movementState = MovementState.walking;

    public Vector3?[] _possibleTargets = new Vector3?[4];

    private Vector3? _targetPos = null;

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

        if (Input.GetKeyDown(KeyCode.Z))
        {
            SetMovement(0);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {

            SetMovement(3);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            SetMovement(1);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            SetMovement(2);
        }
    }

    private void UpdateMovements()
    {
        if (_targetPos == null) return;

        transform.position = Vector3.MoveTowards(transform.position, _targetPos.Value, _movementSpeed * Time.deltaTime);

        if((_targetPos.Value - transform.position).sqrMagnitude < 0.01f) { OnMovementEnd(); }
    }

    private void SetMovement(int value)
    {
        if(_possibleTargets[value] != null)
        {
            SetTargetPos(_possibleTargets[value].Value);
        }
    }

    private void OnMovementEnd()
    {
        _targetPos = null;
        CheckForCurrModule();
    }

    public void SetCurrModule(ModuleBehaviour moduleBehaviour)
    {
        _currModule = moduleBehaviour;

        SetPossibleTargetsPos();
    }

    public void SetTargetPos(Vector3 pos)
    {
        _targetPos = pos;
    }

    public void SetPossibleTargetsPos()
    {
        if (_currModule == null) { Debug.LogWarning("NoCurrentModule"); return; }

        if(_movementState == MovementState.walking)
        {
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
    }

    private void SetPossibleTargetPos(int value, ModuleBehaviour[] neighbors)
    {
        if(_movementState == MovementState.walking)
        {
            if (neighbors[value] != null)
            {
                Vector3?[] anchors = neighbors[value].GetAnchorsPos();

                if (anchors[0] != null)
                {
                    _possibleTargets[value] = anchors[0];
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
    }



    private void CheckForCurrModule()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1f))
        {
            if(hit.collider.TryGetComponent<ModuleBehaviour>(out ModuleBehaviour module))
            {
                SetCurrModule(module);
            }
        }
    }
}
public enum MovementState { walking, climbing }
