using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameplayElement : MonoBehaviour
{
    #region Properties


    #endregion

    #region UnityInspector

    public ModuleBehaviour moduleAssociated;
    public List<ModuleBehaviour> modulesNeighbours;

    #endregion

    #region Behaviour

    public virtual void Start()
    {
        modulesNeighbours = new List<ModuleBehaviour>();

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

        if (moduleAssociated != null)
        {
            for (int i = 0; i < moduleAssociated._neighbors.Length; i++)
            {
                if (moduleAssociated._neighbors[i] != null && modulesNeighbours.Contains(moduleAssociated._neighbors[i]) == false)
                {
                    modulesNeighbours.Add(moduleAssociated._neighbors[i]);
                }
            }
        }
    }

    public bool SetCurrentRangeModule()
    {
        if(GameCore.Instance.Player.CurrModule == moduleAssociated)
        {
            return true;
        }
        else if(modulesNeighbours.Contains(GameCore.Instance.Player.CurrModule))
        {
            return true;
        }

        return false;
    }

    #endregion
}
