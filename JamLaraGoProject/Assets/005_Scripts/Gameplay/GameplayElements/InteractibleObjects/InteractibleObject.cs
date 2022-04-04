using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractibleObject : MonoBehaviour
{
    #region Properties


    #endregion

    #region UnityInspector

    public ModuleBehaviour moduleAssociated;
    //public List<ModuleBehaviour> modulesNeighbours;

    #endregion

    #region Behaviour

    public virtual void Start()
    {
        GetModule();
    }

    [Button(ButtonSizes.Medium)]
    public void GetModule()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
        {
            Debug.Log("Did Hit " + hit.collider.name);
            ModuleBehaviour module = hit.collider.GetComponent<ModuleBehaviour>();
            if (module != null)
            {
                moduleAssociated = module;
            }
            else
            {
                moduleAssociated = null;
            }
        }

        /*if (moduleAssociated != null)
        {
            for (int i = 0; i < moduleAssociated._neighbors.Count; i++)
            {
                if (moduleAssociated._neighbors[i] != null && modulesNeighbours.Contains(moduleAssociated._neighbors[i]) == false)
                {
                    modulesNeighbours.Add(moduleAssociated._neighbors[i]);
                }
            }
        }*/
    }

    public virtual bool SetCurrentRangeModule()
    {
        if(GameCore.Instance.Player.CurrModule == null || moduleAssociated == null)
        {
            return false;
        }

        if(GameCore.Instance.Player.CurrModule == moduleAssociated)
        {
            Debug.Log(GameCore.Instance.Player.CurrModule + " " + moduleAssociated.gameObject.name);
            return true;
        }

        return false;
    }

    #endregion
}
