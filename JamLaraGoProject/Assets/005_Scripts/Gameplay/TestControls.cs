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

        for (int i = 0; i < Input.touchCount; i++)
        {
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.touches[i].position);
            Debug.DrawLine(Vector3.zero, touchPosition, Color.red);
        }

        if(joystick.canMove == false)
        {
            horizontalMove = joystick.Horizontal * runSpeed;
            verticalMove = joystick.Vertical * runSpeed;
        }
        
        if (joystick.canMove)
        {
            joystick.canMove = false;
            

            float _x = 0;
            float _z = 0;
           

            if(horizontalMove > 0)
            {
                _x = 1;
            }
            else if (horizontalMove == 0)
            {
                _x = 0;
            }
            else if (horizontalMove < 0)
            {
                _x = -1;
            }

            if (verticalMove > 0)
            {
                _z = 1;
            }
            else if (verticalMove == 0)
            {
                _z = 0;
            }
            else if (verticalMove < 0)
            {
                _z = -1;
            }

            Debug.Log(_x);
            Debug.Log(_z);
            transform.position = new Vector3(transform.position.x + _x,
            transform.position.y, transform.position.z + _z);
        }

        /*transform.position = new Vector3(transform.position.x + horizontalMove,
            transform.position.y + verticalMove, transform.position.z);*/
    }
}
