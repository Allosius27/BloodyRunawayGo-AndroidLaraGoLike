using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestControls : MonoBehaviour
{
    float horizontalMove;
    float verticalMove;

    public float runSpeed;

    public Joystick joystick;

    // Update is called once per frame
    void Update()
    {
        /*if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0f;
            transform.position = touchPosition;
        }*/

        /*if(joystick.Horizontal >= 0.2f)
        {
            horizontalMove = runSpeed;
        } else if(joystick.Horizontal <= -0.2f)
        {
            horizontalMove = -runSpeed;
        } else
        {
            horizontalMove = 0f;
        }*/

        horizontalMove = joystick.Horizontal * runSpeed;
        Debug.Log(horizontalMove);

        verticalMove = joystick.Vertical * runSpeed;
        Debug.Log(verticalMove);

        for (int i = 0; i < Input.touchCount; i++)
        {
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.touches[i].position);
            Debug.DrawLine(Vector3.zero, touchPosition, Color.red);
        }

        transform.position = new Vector3(transform.position.x + horizontalMove,
            transform.position.y + verticalMove, transform.position.z);
    }
}
