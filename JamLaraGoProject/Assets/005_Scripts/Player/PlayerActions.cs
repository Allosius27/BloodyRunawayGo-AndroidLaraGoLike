using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    #region Fields

    private PlayerMovementController playerMovementController;

    #endregion

    #region UnityInspector

    public LayerMask raycastMask;

    #endregion

    #region Behaviour

    private void Start()
    {
        playerMovementController = GetComponent<PlayerMovementController>();
    }

    private void Update()
    {
        RaycastHits();
    }

    public void RaycastHits()
    {
#if UNITY_ANDROID
        if ((Input.touchCount > 0))
        {
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {

                if (playerMovementController.MovementDir == PlayerMovementController.MovementDirection.None)
                {
                    Debug.Log("Android Raycast Hits");
                    GenerateRaycast(Input.GetTouch(0).position);
                }
            }
            
        }
#else
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("PC Raycast Hits");
            GenerateRaycast(Input.mousePosition);
        }
#endif
    }

    private void GenerateRaycast(Vector3 pos)
    {
        Ray raycast = Camera.main.ScreenPointToRay(pos);
        RaycastHit raycastHit;
        if (Physics.Raycast(raycast, out raycastHit, Mathf.Infinity, raycastMask))
        {
            if (raycastHit.collider != null)
            {
                ModuleBehaviour module = raycastHit.collider.gameObject.GetComponent<ModuleBehaviour>();
                if(module != null)
                {
                    Debug.Log("Module Touch");
                    return;
                }

                Debug.Log(raycastHit.collider.name);

                BaseLever baseLever = raycastHit.collider.gameObject.GetComponent<BaseLever>();
                if(baseLever != null)
                {
                    Debug.Log("Base Lever Press");
                    baseLever.Press();
                }

                LeverActivateObject leverActivateObject = raycastHit.collider.gameObject.GetComponent<LeverActivateObject>();
                if (leverActivateObject != null)
                {
                    Debug.Log("Lever Activate Press");
                    leverActivateObject.Use();
                }
            }
        }
    }

#endregion
}
