using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackedSlab : ModuleBehaviour
{
    #region Fields

    private int maxDurability;
    private int currentDamage;

    #endregion

    #region Properties


    #endregion

    #region UnityInspector

    [SerializeField] private MeshRenderer graphics;
    [SerializeField] private Collider colliderObj;

    [SerializeField] private CrackedSlabData crackedSlabData;

    [Space]

    [SerializeField] private AllosiusDev.FeedbacksData damagedCrackedSlabFeedbackData;
    [SerializeField] private AllosiusDev.FeedbacksData destroyCrackedSlabFeedbackData;

    #endregion

    #region Behaviour

    public override void Start()
    {
        base.Start();

        maxDurability = crackedSlabData.statesMeshes.Count-1;

        graphics.GetComponent<MeshFilter>().mesh = crackedSlabData.statesMeshes[currentDamage];
    }

    public override void OnWalked()
    {
        if(currentDamage >= maxDurability)
        {
            return;
        }

        currentDamage += 1;

        graphics.GetComponent<MeshFilter>().mesh = crackedSlabData.statesMeshes[currentDamage];

        if(currentDamage >= maxDurability)
        {
            StartCoroutine(destroyCrackedSlabFeedbackData.CoroutineExecute(this.gameObject));
        }
        else
        {
            StartCoroutine(damagedCrackedSlabFeedbackData.CoroutineExecute(this.gameObject));
        }
    }

    public override void CheckCurrentPlayerModule(ModuleBehaviour playerModule)
    {
        base.CheckCurrentPlayerModule(playerModule);

        if (playerModule == this && currentDamage >= maxDurability)
        {
            currentDamage = maxDurability;

            //graphics.gameObject.SetActive(false);
            colliderObj.enabled = false;

            GameCore.Instance.Player.fall = true;
            GameCore.Instance.Player.canMove = false;
        }
    }



    #endregion
}
