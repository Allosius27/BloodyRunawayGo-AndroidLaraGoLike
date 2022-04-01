using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject _target = null;

    private Vector3 _initialOffset;

    private void Start()
    {
        _initialOffset = transform.position - _target.transform.position;
    }

    private void Update()
    {
        transform.position = _target.transform.position + _initialOffset;
    }
}
