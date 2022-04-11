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

    public bool batActive { get; set; }

    private bool check = false;

    public void ChangeMesh(bool isBat)
    {
        if (check == isBat) return;
        check = isBat;
        
        _playerMesh.SetActive(!isBat);
        _batMesh.SetActive(isBat);

        if (isBat == true)
        {
            batActive = true;
        }

        StartCoroutine(shapeshiftingFeedbacksData.CoroutineExecute(this.gameObject));

        if (_changingFx == null) return;
        _changingFx.Play();
    }
}
