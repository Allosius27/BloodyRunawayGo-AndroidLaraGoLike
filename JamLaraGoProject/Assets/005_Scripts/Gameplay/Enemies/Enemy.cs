using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : InteractibleObject
{
    #region Fields

    private List<Entity> listHits = new List<Entity>();
    private List<ModuleBehaviour> modulesVisibles = new List<ModuleBehaviour>();

    private Vector3 m_DetectorOffset = Vector3.zero;

    #endregion

    #region Properties

    public bool canAttack { get; set; }
    public bool canDied { get; set; }

    public List<ModuleBehaviour> ModulesVisibles => modulesVisibles;

    public ModuleBehaviour ModuleAssociated => moduleAssociated;

    #endregion

    #region UnityInspector


    [SerializeField] private Vector3 m_DetectorSize = Vector3.zero;

    [SerializeField] private Transform rangePoint;
    [SerializeField] private Transform bulletPoint;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Animator anim;

    #endregion

    #region Behaviour

    public override void Start()
    {
        canAttack = this;
        canDied = this;

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
            Debug.Log("Did hit " + hits[i].collider.name);
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
                Debug.Log("Has Obstacle");
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

            GameObject bullet = Instantiate(bulletPrefab);
            bullet.transform.SetParent(bulletPoint);
            bullet.transform.localPosition = Vector3.zero;
            bullet.transform.rotation = Quaternion.identity;

            bullet.GetComponent<BulletEnemy>().Graphics.transform.localEulerAngles = transform.localEulerAngles;

            Vector3 direction = rangePoint.TransformDirection(Vector3.forward);
            bullet.GetComponent<BulletEnemy>().direction = direction;

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
        anim.SetTrigger("Death");
        GameCore.Instance.Enemies.Remove(this);
        this.enabled = false;
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
