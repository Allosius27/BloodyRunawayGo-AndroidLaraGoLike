using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBatSwitchController : MonoBehaviour
{
    #region Fields

    private PlayerMovementController playerMovementController;

    private bool check = false;

    #endregion

    #region Properties
    public bool batActive { get; set; }

    public float CharacterMoveSpeed => characterMoveSpeed;

    public float BatMoveSpeed => batMoveSpeed;

    #endregion

    #region UnityInspector

    [SerializeField] private GameObject _playerMesh;
    [SerializeField] private GameObject _batMesh;

    [SerializeField] private float characterMoveSpeed;
    [SerializeField] private float batMoveSpeed;

    [Space]

    [SerializeField] private ParticleSystem _changingFx;

    [SerializeField] private AllosiusDev.FeedbacksData shapeshiftingFeedbacksData;

    #endregion

    #region Behaviour

    private void Awake()
    {
        playerMovementController = GetComponent<PlayerMovementController>();
    }

    public void ChangeMesh(bool isBat)
    {
        if (check == isBat) return;
        check = isBat;
        
        _playerMesh.SetActive(!isBat);
        _batMesh.SetActive(isBat);

        if (isBat == true)
        {
            batActive = true;

            playerMovementController._movementSpeed = batMoveSpeed;
        }
        else
        {
            playerMovementController._movementSpeed = characterMoveSpeed;
        }

        StartCoroutine(shapeshiftingFeedbacksData.CoroutineExecute(this.gameObject));

        if (_changingFx == null) return;
        _changingFx.Play();
    }

    #endregion
}
