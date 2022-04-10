using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PlayerMovementController : MonoBehaviour
{
    #region Fields  

    private ModuleBehaviour _currModule = null;

    private Vector3 _beganPosition = Vector3.zero;
    private Vector3 _endedPosition = Vector3.zero;

    private Vector3? _targetPos = null;

    private BatMovement _batMovement = null;

    private int[] _batMovementsCosts = new int[4];

    private readonly Vector3?[] _possibleTargets = new Vector3?[4];

    private MovementDirection _movementDir = MovementDirection.None;

    private bool _isInUpMovement = false;
    private bool _isInDownMovement = false;

    #endregion

    #region Properties
    public enum MovementDirection { None, Up, Down, Right, Left }

    public MovementDirection MovementDir => _movementDir;

    public bool canMove { get; set; }

    public bool fall { get; set; }

    public ModuleBehaviour CurrModule => _currModule;

    public Animator Animator => animator;

    #endregion

    #region UnityInspector

    [SerializeField] private float _movementSpeed = 5f;

    [SerializeField] private Button _upMovementButton = null;

    [SerializeField] private Animator animator;

    [SerializeField] private PlayerBatSwitchController _playerBatSwitch;

    [Space]

    [SerializeField] private AllosiusDev.FeedbacksData playerTakeDamageFeedbackData;
    [SerializeField] private AllosiusDev.FeedbacksData playerDeathFeedbackData;

    #endregion

    #region Behaviour

    private void Awake()
    {
        if (_upMovementButton == null)
        {
            _upMovementButton = GameCanvasManager.Instance.UpMovementButton;
        }

        _batMovement = this.gameObject.GetComponent<BatMovement>();
    }

    private void Start()
    {
        canMove = true;

        _upMovementButton.onClick.AddListener(DoUpMovement);
        _upMovementButton.gameObject.SetActive(false);
        CheckForCurrModule();
    }

    private void Update()
    {
        if (canMove)
        {
            UpdateInput();
            UpdateMovements();
        }

        if (fall)
        {
            transform.Translate(Vector3.down * Time.deltaTime * _movementSpeed);
        }
    }
    private void UpdateInput()
    {
        if (_targetPos != null) return;

        int currBatMovement = _batMovement.GetCurrBatMovement();

#if UNITY_ANDROID
        GetMobileInputs();
#endif

#if UNITY_EDITOR
        GetKeyboardInputs();
#endif

        if (_movementDir == MovementDirection.Up && _currModule.upDirectionLocked == false)
        {
            if (_batMovementsCosts[0] <= currBatMovement)
            {
                SetMovement(0);
            }

        }
        else if (_movementDir == MovementDirection.Down && _currModule.downDirectionLocked == false)
        {
            if (_batMovementsCosts[1] <= currBatMovement)
            {
                SetMovement(1);
            }
        }
        else if (_movementDir == MovementDirection.Right && _currModule.rightDirectionLocked == false)
        {
            if (_batMovementsCosts[2] <= currBatMovement)
            {
                SetMovement(2);
            }
        }
        else if (_movementDir == MovementDirection.Left && _currModule.leftDirectionLocked == false)
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
            _movementDir = MovementDirection.Up;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            _movementDir = MovementDirection.Down;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            _movementDir = MovementDirection.Right;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            _movementDir = MovementDirection.Left;
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

                Debug.Log(direction);
                //Debug.Log(Mathf.Abs(_currModule.GetAnchorPos().Value.x + _currModule.transform.position.x) + " " + Mathf.Abs(_currModule.GetAnchorPos().Value.z + +_currModule.transform.position.z));

                if (Mathf.Abs(direction.x) < _currModule.DetectionRange && Mathf.Abs(direction.y) < _currModule.DetectionRange)
                {
                    _movementDir = MovementDirection.None;
                    return;
                }

                if (direction.x > 0 && direction.y > 0)
                {
                    Debug.Log("RIGHT");
                    _movementDir = MovementDirection.Right;
                }

                if (direction.x > 0 && direction.y < 0)
                {
                    Debug.Log("DOWN");
                    _movementDir = MovementDirection.Down;
                }

                if (direction.x < 0 && direction.y < 0)
                {
                    Debug.Log("LEFT");
                    _movementDir = MovementDirection.Left;
                }

                if (direction.x < 0 && direction.y > 0)
                {
                    Debug.Log("UP");
                    _movementDir = MovementDirection.Up;
                }
            }
        }
    }

    private void UpdateMovements()
    {
        if (_targetPos == null) return;

        Vector3 direction = _targetPos.Value - transform.position;
        float moveStep = _movementSpeed * Time.deltaTime;

        if (moveStep > direction.sqrMagnitude)
        {
            transform.DOMove(_targetPos.Value, 0.125f);
            OnMovementEnd();
        }
        else
        {
            animator.SetBool("Move", true);
            transform.position = Vector3.MoveTowards(transform.position, _targetPos.Value, _movementSpeed * Time.deltaTime);
        }
    }

    private void SetMovement(int value)
    {
        if (_possibleTargets[value] != null)
        {
            _batMovement.ChangeBatMovementCount(-_batMovementsCosts[value]);

            if (_batMovementsCosts[value] > 0)
            {
                _playerBatSwitch.ChangeMesh(true);
            }
            
            _batMovementsCosts = new int[4];

            if (value == 0)
            {
                transform.DORotate(new Vector3(0, 0, 0), 0.2f);
            }
            else if (value == 1)
            {
                transform.DORotate(new Vector3(0, 180), 0.2f);
            }
            else if (value == 2)
            {
                transform.DORotate(new Vector3(0, 90, 0), 0.2f);
            }
            else if (value == 3)
            {
                transform.DORotate(new Vector3(0, 270, 0), 0.2f);
            }

            SetTargetPos(_possibleTargets[value].Value);
        }
    }

    private void OnMovementEnd()
    {
        animator.SetBool("Move", false);

        _targetPos = null;
        _movementDir = MovementDirection.None;

        _playerBatSwitch.ChangeMesh(false);
        
        if (_isInUpMovement && upDownBlock != null)
        {
            _targetPos = upDownBlock.transform.position;
            _isInUpMovement = false;
            upDownBlock = null;

            _playerBatSwitch.ChangeMesh(true);
        }

        if (_isInDownMovement && upDownBlock != null)
        {
            _targetPos = upDownBlock.transform.position;
            _isInDownMovement = false;
            upDownBlock = null;
            _playerBatSwitch.ChangeMesh(true);
        }

        CheckForCurrModule();
    }

    private void SetCurrModule(ModuleBehaviour moduleBehaviour)
    {
        if (_currModule != null)
        {
            _currModule.CheckCurrentPlayerModule(_currModule);
            _currModule.OnWalked();
        }

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

        List<ModuleBehaviour> neighbors = _currModule.GetNeighbors();

        //Forward
        SetPossibleTargetPos(0, neighbors);
        //BackWard
        SetPossibleTargetPos(1, neighbors);
        //Right
        SetPossibleTargetPos(2, neighbors);
        //Left
        SetPossibleTargetPos(3, neighbors);
    }

    private void SetPossibleTargetPos(int value, List<ModuleBehaviour> neighbors)
    {
        if (neighbors[value] == null)
        {
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

    GameObject upDownBlock = null;
    public void DoUpMovement()
    {
        if (upDownBlock == null) return;

        _playerBatSwitch.ChangeMesh(true);
        
        Vector3 target = Vector3.zero; ;

        if (upDownBlock == upDownMovement.UpBlock)
        {
            _isInUpMovement = true;
            target = new Vector3(transform.position.x, upDownBlock.transform.position.y, transform.position.z);
            _batMovement.ChangeBatMovementCount(-upDownMovement.GetMovementsCost());
        }
        else if (upDownBlock == upDownMovement.DownBlock)
        {
            _isInDownMovement = true;
            target = new Vector3(upDownBlock.transform.position.x, transform.position.y, upDownBlock.transform.position.z);
        }
        _targetPos = target;

        _upMovementButton.gameObject.SetActive(false);
    }

    private UpDownMovementObjectBehaviour upDownMovement = null;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponentInParent<UpDownMovementObjectBehaviour>())
        {
            upDownMovement = other.gameObject.GetComponentInParent<UpDownMovementObjectBehaviour>();

            if (other.gameObject == upDownMovement.UpBlock)
            {
                upDownBlock = upDownMovement.DownBlock;
                _upMovementButton.transform.eulerAngles = new Vector3(0, 0, 180f);
                _upMovementButton.gameObject.SetActive(true);
            }
            else if (other.gameObject == upDownMovement.DownBlock)
            {
                upDownBlock = upDownMovement.UpBlock;
                _upMovementButton.transform.eulerAngles = new Vector3(0, 0, 0f);
            }

            if (upDownMovement.GetMovementsCost() <= _batMovement.GetCurrBatMovement())
            {
                _upMovementButton.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponentInParent<UpDownMovementObjectBehaviour>())
        {
            _upMovementButton.gameObject.SetActive(false);
        }
    }

    public void PlayerAttack(Enemy enemy)
    {
        animator.SetTrigger("Attack");

        StartCoroutine(CoroutinePlayerAttack(enemy));
    }

    IEnumerator CoroutinePlayerAttack(Enemy enemy)
    {
        canMove = false;

        //Fetch the current Animation clip information for the base layer
        AnimatorClipInfo[] m_CurrentClipInfo = animator.GetCurrentAnimatorClipInfo(0);
        //Access the current length of the clip
        float m_CurrentClipLength = m_CurrentClipInfo[0].clip.length;

        yield return new WaitForSeconds(m_CurrentClipLength);

        canMove = true;

        enemy.Death();
    }

    public void PlayerDamage()
    {
        StartCoroutine(playerTakeDamageFeedbackData.CoroutineExecute(this.gameObject));
        StartCoroutine(playerDeathFeedbackData.CoroutineExecute(this.gameObject));

        animator.SetTrigger("Death");

        StartCoroutine(CoroutinePlayerDamage());
    }

    IEnumerator CoroutinePlayerDamage()
    {
        canMove = false;

        //Fetch the current Animation clip information for the base layer
        AnimatorClipInfo[] m_CurrentClipInfo = animator.GetCurrentAnimatorClipInfo(0);
        //Access the current length of the clip
        float m_CurrentClipLength = m_CurrentClipInfo[0].clip.length;

        yield return new WaitForSeconds(m_CurrentClipLength);

        GameCore.Instance.GameOver();
    }
}
#endregion