using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ModuleBehaviour : GameplayElement
{
    #region Fields

    private float _meshSize = 0f;

    private Vector3? _anchorPos = null;


    #endregion

    #region Properties

    public float DetectionRange => detectionRangeValue;

    public PressurePlate pressurePlateAssociated { get; set; }


    public bool isLighting { get; set; }


    public bool upDirectionLocked { get; set; }
    public bool downDirectionLocked { get; set; }
    public bool rightDirectionLocked { get; set; }
    public bool leftDirectionLocked { get; set; }


    public AllosiusDev.FeedbacksData PlayerLightBurnFeedbackData => playerLightBurnFeedbackData;


    #endregion

    #region UnityInspector

    [SerializeField] private LayerMask modulesLayerMask;

    [HideInInspector] [SerializeField] private bool _topAnchor = true;

    [SerializeField] private bool isLastModule;


    [SerializeField] private float detectionRangeValue = 10.0f;

    [Header("Anchors")] 
    [SerializeField] private GameObject _anchorSprite = null;
    [SerializeField] private GameObject _endLevelAnchorSprite = null;

    public List<ModuleBehaviour> _neighbors = new List<ModuleBehaviour>(6);

    [Space]

    [SerializeField] private AllosiusDev.FeedbacksData playerLightBurnFeedbackData;

    #endregion


    #region Behavior

    public virtual void Awake()
    {
        _meshSize = this.transform.GetChild(0).localScale.x;

        SetAnchorPos();

        RegisterNeighbors();
    }

    public void SetAnchorPos()
    {
        Vector3 pos = transform.position;

        if (_topAnchor) _anchorPos = (pos + new Vector3(0f, _meshSize, 0f));

        //Debug.Log(_anchorPos);

        _anchorSprite.SetActive(_topAnchor);
    }

    public virtual void Start()
    {
        

        //GetIsLastModule();
    }

    [Button(ButtonSizes.Medium)]
    public void SetIsLastModule()
    {
        isLastModule = !isLastModule;
        GetIsLastModule();
    }

    public void GetIsLastModule()
    {
        if (isLastModule)
        {
            _endLevelAnchorSprite.SetActive(true);
        }
        else
        {
            _endLevelAnchorSprite.SetActive(false);
        }
    }

    [Button(ButtonSizes.Medium)]
    public void RegisterNeighbors()
    {
        Vector3[] dir = new Vector3[6];

        _meshSize = this.transform.GetChild(0).localScale.x;

        dir[0] = new Vector3(0f, 0f, 1);
        dir[1] = new Vector3(0f, 0f, -1);
        dir[2] = new Vector3(1, 0f, 0f);
        dir[3] = new Vector3(-1, 0f, 0f);
        dir[4] = new Vector3(0f, 1, 0f);
        dir[5] = new Vector3(0f, -1, 0f);

        RaycastHit hit = new RaycastHit();

        Vector3 pos = transform.position + new Vector3(0f, _meshSize * 0.5f, 0f);

        for (int i = 0; i < dir.Length; i++)
        {
            Ray ray = new Ray(pos, dir[i]);

            if (Physics.Raycast(ray, out hit, 1.2f, modulesLayerMask))
            {
                ModuleBehaviour moduleBehaviour = hit.collider.gameObject.GetComponent<ModuleBehaviour>();
                MovingModuleBehaviour movingModuleBehaviour = hit.collider.gameObject.GetComponent<MovingModuleBehaviour>();
                
                
                if (moduleBehaviour != null && movingModuleBehaviour == null)
                {
                    _neighbors[i] = moduleBehaviour;
                }
                else if (movingModuleBehaviour != null && movingModuleBehaviour.MoveLoop == false && movingModuleBehaviour.IsMoving == false)
                {
                    _neighbors[i] = movingModuleBehaviour;
                }
                else
                {
                    _neighbors[i] = null;
                }
                //hit.collider.gameObject.TryGetComponent<ModuleBehaviour>(out _neighbors[i]);
            }
            else
            {
                _neighbors[i] = null;
            }
        }
    }

    public List<ModuleBehaviour> GetNeighbors()
    {
        return _neighbors;
    }

    public Vector3? GetAnchorPos()
    {
        return _anchorPos;
    }

    public virtual void OnWalked()
    {

    }

    public virtual void CheckCurrentPlayerModule(ModuleBehaviour playerModule)
    {
        if(playerModule != this)
        {
            return;
        }

        if (pressurePlateAssociated != null)
        {
            pressurePlateAssociated.PlayerPress();
        }

        if (isLighting)
        {
            StartCoroutine(playerLightBurnFeedbackData.CoroutineExecute(this.gameObject));
            GameCore.Instance.Player.PlayerDeath();
            //GameCore.Instance.GameOver();
        }

        GameCore.Instance.UpdateEnemiesBehaviour();

        if(isLastModule)
        {
            Debug.Log("Level Completed !");

            GameCore.Instance.LevelCompleted();
        }
    }

    public override void Activate()
    {
        
    }

    public override void Deactivate()
    {
        
    }

    #endregion

    #region Gizmos
    public virtual void OnDrawGizmosSelected()
    {
        if (!_topAnchor) return;

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y + _meshSize, transform.position.z), 0.5f);
    }
    
    [Button(ButtonSizes.Medium)]
    public void SetWalkableUnwakable()
    {
        _topAnchor = !_topAnchor;
        _anchorSprite.SetActive(_topAnchor);
    }

    #endregion
}
