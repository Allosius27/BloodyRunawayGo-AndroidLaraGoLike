using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);

        PlayerMovementController player = other.GetComponent<PlayerMovementController>();
        if(player != null)
        {
            GameCore.Instance.GameOver();
        }
    }
}
