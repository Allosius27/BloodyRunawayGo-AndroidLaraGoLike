using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ModuleBehaviour moduleBehaviour = other.gameObject.GetComponent<ModuleBehaviour>();
        if(moduleBehaviour != null)
        {
            moduleBehaviour.isLighting = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        ModuleBehaviour moduleBehaviour = other.gameObject.GetComponent<ModuleBehaviour>();
        if (moduleBehaviour != null)
        {
            moduleBehaviour.isLighting = false;
        }
    }
}
