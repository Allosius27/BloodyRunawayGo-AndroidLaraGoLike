using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatObjectBehaviour : MonoBehaviour
{
    [SerializeField] private int _batAmount = 3;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<BatMovement>(out BatMovement batMovement))
        {
            batMovement.ChangeBatMovementCount(_batAmount);
            Destroy(this.gameObject);
        }
    }
}
