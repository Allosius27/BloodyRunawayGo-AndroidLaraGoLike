using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestControls : MonoBehaviour
{
    float horizontalMove;
    float verticalMove;

    Vector3 beganPosition;
    Vector3 endedPosition;

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

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                //Debug.Log("Began " + touch.position);
                beganPosition = touch.position;
            }
            if (touch.phase == TouchPhase.Moved)
            {
                //Debug.Log("Moved " + touch.position);
            }
            if (touch.phase == TouchPhase.Ended)
            {
                //Debug.Log("Ended " + touch.position);
                endedPosition = touch.position;

                Vector3 direction = endedPosition - beganPosition;
                //var posDirection = Camera.main.ScreenToWorldPoint(direction);
                Debug.Log("direction : " + direction);

                if (direction.x > 0 && direction.y > 0)
                {
                    Debug.Log("RIGHT");
                }

                if (direction.x > 0 && direction.y < 0)
                {
                    Debug.Log("DOWN");
                }

                if (direction.x < 0 && direction.y < 0)
                {
                    Debug.Log("LEFT");
                }

                if (direction.x < 0 && direction.y > 0)
                {
                    Debug.Log("UP");
                }

                //var posEndedPosition = Camera.main.ScreenToWorldPoint(endedPosition);
                //Debug.Log("posEndedPosition : " + posEndedPosition);
                //var posBeganPosition = Camera.main.ScreenToWorldPoint(beganPosition);
                //Debug.Log("posBeganPosition : " + posBeganPosition);
                float distance = Vector3.Distance(endedPosition, beganPosition);
                //Debug.Log("distance : " + distance);
            }
        }



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

        /*for (int i = 0; i < Input.touchCount; i++)
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
        }*/

        /*transform.position = new Vector3(transform.position.x + horizontalMove,
            transform.position.y + verticalMove, transform.position.z);*/
    }
}
