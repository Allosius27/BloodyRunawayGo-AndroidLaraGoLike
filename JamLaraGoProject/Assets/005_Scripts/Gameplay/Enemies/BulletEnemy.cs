using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    #region Fields

    private Rigidbody rb;

    private Vector3 velocity = Vector3.zero;

    #endregion

    #region Properties

    public Vector3 direction { get; set; }

    public GameObject Graphics => graphics;

    #endregion

    #region UnityInspector

    [SerializeField] private GameObject graphics;

    [SerializeField] private float speed;

    #endregion

    #region Behaviour

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //rb.velocity = Vector3.SmoothDamp(rb.velocity, direction * speed, ref velocity, 0.05f);
        transform.Translate(direction * Time.deltaTime * speed);
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
