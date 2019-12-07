using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyListener : MonoBehaviour
{
    private bool interactMode;
    private Rigidbody body;

    //Displacement variables used to set velocity of the userRobot
    private float displacementX;
    private float displacementY;

    private float deltaTurn;

    void Start()
    {
        interactMode = true;
        body = GetComponent<Rigidbody>();

        //Constant used to perform calculations for robot movement velocities
        deltaTurn = 0.000366666667f;

        //The initial values used to move the robot
        displacementX = 0;
        displacementY = -0.02f;
    }

    //Fixed Update allows accurate physics as well as consistant rendering
    void FixedUpdate()
    {
        //Specific to the interact state
        if (interactMode) {
            //Used to make sure the robot remains on one plane
            this.transform.position = new Vector3(this.transform.position.x, 0.06f, this.transform.position.z);
            this.transform.eulerAngles = new Vector3(0f, this.transform.eulerAngles.y, 0f);

            //Resetting physics values
            body.velocity = Vector3.zero;
            body.angularVelocity = Vector3.zero;

            if (Input.GetKey(KeyCode.UpArrow))
            {
                updateDisplacements();
                body.velocity = new Vector3(displacementX * 30, 0f, displacementY * 30);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                updateDisplacements();
                body.velocity = new Vector3(displacementX * -30, 0f, displacementY * -30);
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                float turn = Input.GetAxis("Horizontal");
                body.AddTorque(transform.up * 70 * turn);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                float turn = Input.GetAxis("Horizontal");
                body.AddTorque(transform.up * 70 * turn);
            }
        }
    }

    //Calculates displacements to determine velocities necessary to move the robot
    private void updateDisplacements()
    { 
        /*Uses the current robot's angle to calculate the necessary displacements   
            to move in the given direction*/
        float angle = this.transform.eulerAngles.y;
        if (angle >= 0 && angle < 90)
        {
            displacementX = (90 - angle) * deltaTurn;
            displacementY = (0 - angle) * deltaTurn;
        }
        else if (angle >= 90 && angle < 180)
        {
            displacementX = (90 - angle) * deltaTurn;
            displacementY = -(180 - angle) * deltaTurn;
        }
        else if (angle >= 180 && angle < 270)
        {
            displacementX = -(270 - angle) * deltaTurn;
            displacementY = -(180 - angle) * deltaTurn;
        }
        else if (angle >= 270 && angle < 360)
        {
            displacementX = -(270 - angle) * deltaTurn;
            displacementY = (360 - angle) * deltaTurn;
        }
    }
}
