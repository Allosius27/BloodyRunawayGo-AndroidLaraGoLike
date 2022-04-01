using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightObject : MonoBehaviour
{
    #region Fields

    private List<ModuleBehaviour> modulesAssociated = new List<ModuleBehaviour>();

    #endregion

    public void DeactiveLight()
    {
        for (int i = 0; i < modulesAssociated.Count; i++)
        {
            modulesAssociated[i].isLighting = false;
        }

        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("light trigger");

        ModuleBehaviour moduleBehaviour = other.gameObject.GetComponent<ModuleBehaviour>();
        if(moduleBehaviour != null)
        {
            if (modulesAssociated.Contains(moduleBehaviour) == false)
            {
                modulesAssociated.Add(moduleBehaviour);
            }
            moduleBehaviour.isLighting = true;
            moduleBehaviour.CheckCurrentPlayerModule(GameCore.Instance.Player.CurrModule);
        }
    }
}
