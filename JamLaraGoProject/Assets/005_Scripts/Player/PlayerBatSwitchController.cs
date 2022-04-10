using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBatSwitchController : MonoBehaviour
{
    [SerializeField] private GameObject _playerMesh;
    [SerializeField] private GameObject _batMesh;
    [SerializeField] private ParticleSystem _changingFx;


    private bool check = true;
    private void Start()
    {
        ChangeMesh(false);
    }

    public void ChangeMesh(bool isBat)
    {
        if (check == isBat) return;
        check = isBat;
        
        _playerMesh.SetActive(!isBat);
        _batMesh.SetActive(isBat);

        if (_changingFx == null) return;
        _changingFx.Play();
    }
}
