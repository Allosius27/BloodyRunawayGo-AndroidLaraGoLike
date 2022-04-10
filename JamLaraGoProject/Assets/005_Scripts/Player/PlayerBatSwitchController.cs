using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBatSwitchController : MonoBehaviour
{
    [SerializeField] private GameObject _playerMesh;
    [SerializeField] private GameObject _batMesh;

    [Space]

    [SerializeField] private ParticleSystem _changingFx;

    [SerializeField] private AllosiusDev.FeedbacksData shapeshiftingFeedbacksData;


    private bool check = false;

    public void ChangeMesh(bool isBat)
    {
        if (check == isBat) return;
        check = isBat;
        
        _playerMesh.SetActive(!isBat);
        _batMesh.SetActive(isBat);

        StartCoroutine(shapeshiftingFeedbacksData.CoroutineExecute(this.gameObject));

        if (_changingFx == null) return;
        _changingFx.Play();
    }
}
