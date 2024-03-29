using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : InteractibleObject
{
    #region Fields

    private List<Entity> listHits = new List<Entity>();
    private List<ModuleBehaviour> modulesVisibles = new List<ModuleBehaviour>();

    private Vector3 m_DetectorOffset = Vector3.zero;

    private BatMovement batMovement;

    #endregion

    #region Properties

    public bool canAttack { get; set; }
    public bool canDied { get; set; }

    public List<ModuleBehaviour> ModulesVisibles => modulesVisibles;

    public ModuleBehaviour ModuleAssociated => moduleAssociated;

    public Transform RangePoint => rangePoint;
    public Transform BulletPoint => bulletPoint;

    public GameObject BulletPrefab => bulletPrefab;


    #endregion

    #region UnityInspector


    [SerializeField] private Vector3 m_DetectorSize = Vector3.zero;

    [SerializeField] private Transform rangePoint;
    [SerializeField] private Transform bulletPoint;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Animator anim;

    [SerializeField] private int _batAmount = 3;

    [Space]

    [SerializeField] private AllosiusDev.FeedbacksData takeDamageFeedbackData;
    [SerializeField] private AllosiusDev.FeedbacksData deathFeedbackData;

    #endregion

    #region Behaviour

    public override void Start()
    {
        canAttack = this;
        canDied = this;

        batMovement = GameCore.Instance.Player.GetComponent<BatMovement>();

        GetCurrentModule();

        UpdateEnemyBehaviour();
    }

    public void GetCurrentModule()
    {
        GetModule();

        MovingModuleBehaviour movingModuleBehaviour = moduleAssociated.GetComponent<MovingModuleBehaviour>();
        if (movingModuleBehaviour != null)
        {
            transform.parent = movingModuleBehaviour.transform;
            movingModuleBehaviour.enemyAssociated = this;
        }
    }

    public override bool SetCurrentRangeModule()
    {
        if (GameCore.Instance.Player.CurrModule == null || moduleAssociated == null)
        {
            return false;
        }

        if (GameCore.Instance.Player.CurrModule == moduleAssociated)
        {
            Debug.Log(GameCore.Instance.Player.CurrModule + " " + moduleAssociated.gameObject.name);
            return true;
        }
        else if (moduleAssociated._neighbors.Contains(GameCore.Instance.Player.CurrModule))
        {
            Debug.Log(GameCore.Instance.Player.CurrModule + " " + moduleAssociated.gameObject.name);
            return true;
        }

        return false;
    }

    [Button(ButtonSizes.Medium)]
    public void CheckForCollisions()
    {
        modulesVisibles.Clear();
        listHits.Clear();

        RaycastHit[] hits;
        hits = Physics.RaycastAll(rangePoint.position, rangePoint.TransformDirection(Vector3.forward), m_DetectorSize.z * 2);
        for (int i = 0; i < hits.Length; i++)
        {
            //Debug.Log("Did hit " + hits[i].collider.name);
            Entity entity = hits[i].collider.GetComponent<Entity>();
            if (entity != null)
            {
                listHits.Add(entity);
                entity.distanceEnemyChecked = 0;
            }
        }

        for (int i = 0; i < listHits.Count; i++)
        {
            float distance = Vector3.Distance(listHits[i].transform.position, rangePoint.position);
            listHits[i].distanceEnemyChecked = distance;
        }

        listHits.Sort(SortByDistance);

        for (int i = 0; i < listHits.Count; i++)
        {
            if(listHits[i].IsObstacle == Entity.isObstacle.IsObstacle)
            {
                //Debug.Log("Has Obstacle");
                return;
            }

            ModuleBehaviour module = listHits[i].GetComponent<ModuleBehaviour>();
            if (module != null && modulesVisibles.Contains(module) == false)
            {
                modulesVisibles.Add(module);
            }
        }

        /*modulesVisibles.Clear();

        Vector3 colliderPos = transform.TransformPoint(m_DetectorOffset);
        Collider[] colliders = Physics.OverlapBox(colliderPos, m_DetectorSize, transform.rotation);

        if(colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    Debug.Log(colliders[i].gameObject);

                    ModuleBehaviour module = colliders[i].GetComponent<ModuleBehaviour>();
                    if(module != null && modulesVisibles.Contains(module) == false)
                    {
                        modulesVisibles.Add(module);
                    }
                }
            }
        }*/
    }

    public bool UpdateEnemyBehaviour()
    {
        CheckForCollisions();

        if(modulesVisibles.Contains(GameCore.Instance.Player.CurrModule) && canAttack)
        {
            Debug.Log("Enemy Detects Player");
            GameCore.Instance.Player.canMove = false;

            anim.SetTrigger("Attack");

            return true;
        }

        return false;
    }

    public void CheckPlayerCanAttack()
    {
        if(SetCurrentRangeModule() && canDied)
        {
            GameCore.Instance.Player.PlayerAttack(this);
        }
    }

    public void Death()
    {
        StartCoroutine(takeDamageFeedbackData.CoroutineExecute(this.gameObject));
        StartCoroutine(deathFeedbackData.CoroutineExecute(this.gameObject));
        anim.SetTrigger("Death");
        GameCore.Instance.Enemies.Remove(this);
        this.enabled = false;
        batMovement.ChangeBatMovementCount(_batAmount);
    }

    private int SortByDistance(Entity a, Entity b)
    {
        float entityA = a.distanceEnemyChecked;
        float entityB = b.distanceEnemyChecked;
        return entityA.CompareTo(entityB);
    }

    #endregion

    #region Gizmos

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.matrix = transform.localToWorldMatrix;
        m_DetectorOffset = new Vector3(0, 0, m_DetectorSize.z);
        Gizmos.DrawWireCube(rangePoint.localPosition + m_DetectorOffset, m_DetectorSize * 2);
    }

    #endregion
}
