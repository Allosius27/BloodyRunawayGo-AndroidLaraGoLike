using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{

    #region UnityInspector

    [SerializeField] private float speed;

    #endregion

    #region Behaviour

    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);

        PlayerMovementController player = other.GetComponent<PlayerMovementController>();
        if (player != null)
        {
            GameCore.Instance.GameOver();

            Destroy(gameObject);
        }
    }

    #endregion
}
