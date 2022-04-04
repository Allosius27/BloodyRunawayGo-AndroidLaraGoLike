using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region Fields

    [SerializeField] private List<ModuleBehaviour> modulesVisibles = new List<ModuleBehaviour>();

    private Vector3 m_DetectorOffset = Vector3.zero;

    #endregion

    #region Properties

    public List<ModuleBehaviour> ModulesVisibles => modulesVisibles;

    #endregion

    #region UnityInspector

    public List<Entity> listHits = new List<Entity>();

    [SerializeField] private Vector3 m_DetectorSize = Vector3.zero;

    [SerializeField] private Transform rangePoint;

    #endregion

    #region Behaviour

    private void Start()
    {
        CheckForCollisions();
    }

    [Button(ButtonSizes.Medium)]
    public void CheckForCollisions()
    {
        modulesVisibles.Clear();
        listHits.Clear();
        /*RaycastHit hit;
        if (Physics.Raycast(rangePoint.position, rangePoint.TransformDirection(Vector3.forward), out hit, m_DetectorSize.z * 2))
        {
            Debug.DrawRay(rangePoint.position, rangePoint.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did hit " + hit.collider.name);
        }*/

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

            /*Entity entity = hits[i].collider.GetComponent<Entity>();
            if(entity != null && entity.IsObstacle == Entity.isObstacle.IsObstacle)
            {
                Debug.Log("Has Obstacle");
                return;
            }

            ModuleBehaviour module = hits[i].collider.GetComponent<ModuleBehaviour>();
            if (module != null && modulesVisibles.Contains(module) == false)
            {
                modulesVisibles.Add(module);
            }*/
        }

        for (int i = 0; i < listHits.Count; i++)
        {
            float distance = Vector3.Distance(listHits[i].transform.position, rangePoint.position);
            listHits[i].distanceEnemyChecked = distance;
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
