using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    #region Behaviour

    private void Update()
    {
        RaycastHits();
    }

    public void RaycastHits()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GenerateRaycast(Input.mousePosition);
        }

        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            GenerateRaycast(Input.GetTouch(0).position);
        }
    }

    private void GenerateRaycast(Vector3 pos)
    {
        Ray raycast = Camera.main.ScreenPointToRay(pos);
        RaycastHit raycastHit;
        if (Physics.Raycast(raycast, out raycastHit))
        {
            if (raycastHit.collider != null)
            {
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
